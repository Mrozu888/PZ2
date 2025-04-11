using System;
using System.Globalization;


namespace Lab04;

class Program
{
    static void Main()
    {
        var loader = new Loader<object>();

        // 1
        Console.WriteLine("\n--Zadanie 1--\n");
        var regions = new Loader<Region>().LoadList("import-northwind-dataset/regions.csv", x => new Region(x[0], x[1]));
        var territories = new Loader<Territory>().LoadList("import-northwind-dataset/territories.csv", x => new Territory(x[0], x[1], x[2]));
        var empTerr = new Loader<EmployeeTerritory>().LoadList("import-northwind-dataset/employee_territories.csv", x => new EmployeeTerritory(x[0], x[1]));
        var employees = new Loader<Employee>().LoadList("import-northwind-dataset/employees.csv", x => new Employee(x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], x[9], x[10], x[11], x[12], x[13], x[14], x[15], x[16], x[17]));

        // 2
        Console.WriteLine("\n--Zadanie 2--\n");
        var q1 = from e in employees
                 select new {Name = e.LastName};
        foreach (var row in q1)
            Console.WriteLine(row.Name);

        // 3
        Console.WriteLine("\n--Zadanie 3--\n");
        var q2 = from e in employees
                 join et in empTerr on e.EmployeeID equals et.EmployeeID
                 join t in territories on et.TerritoryID equals t.TerritoryID
                 join r in regions on t.RegionID equals r.RegionID
                 select new { e.LastName, t.TerritoryDescription, r.RegionDescription };

        foreach (var row in q2)
            Console.WriteLine($"{row.LastName} - {row.RegionDescription} – {row.TerritoryDescription}");

        // 4
        Console.WriteLine("\n--Zadanie 4--\n");
        var q3 = from r in regions
                 join t in territories on r.RegionID equals t.RegionID
                 join et in empTerr on t.TerritoryID equals et.TerritoryID
                 join e in employees on et.EmployeeID equals e.EmployeeID
                 group e by r.RegionDescription into g
                 select new { Region = g.Key, Employees = g.Select(x => x.LastName).Distinct() };

        foreach (var group in q3)
        {
            Console.WriteLine($"Region: {group.Region}");
            foreach (var emp in group.Employees)
                Console.WriteLine("\t\t" + emp);
        }

        // 5
        Console.WriteLine("\n--Zadanie 5--\n");
        var q4 = from r in regions
                 join t in territories on r.RegionID equals t.RegionID
                 join et in empTerr on t.TerritoryID equals et.TerritoryID
                 select new { r.RegionDescription, et.EmployeeID } into x
                 group x by x.RegionDescription into g
                 select new { Region = g.Key, Count = g.Select(x => x.EmployeeID).Distinct().Count() };

        foreach (var row in q4)
            Console.WriteLine($"{row.Region} : {row.Count} pracownikow");


        // 6
        Console.WriteLine("\n--Zadanie 6--\n");
        var orders = new Loader<Order>().LoadList("import-northwind-dataset/orders.csv", x => new Order(x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], x[9], x[10], x[11], x[12], x[13]));
        var orderDetails = new Loader<OrderDetail>().LoadList("import-northwind-dataset/orders_details.csv", x => new OrderDetail(x[0], x[1], x[2], x[3], x[4]));

        var q5 = from e in employees
            join order in orders on e.EmployeeID equals order.EmployeeID
            group order by e into x
            select new
            {
                Employee = x.Key,
                NumOfOrders = x.Count(),
                MeanVal = x.Average(zam =>
                    orderDetails
                        .Where(od => od.OrderID == zam.OrderID)
                        .Sum(od =>
                            double.Parse(od.UnitPrice, CultureInfo.InvariantCulture) *
                            int.Parse(od.Quantity) *
                            (1 - double.Parse(od.Discount, CultureInfo.InvariantCulture)))
                ),
                MaxVal = x.Max(zam =>
                    orderDetails
                        .Where(od => od.OrderID == zam.OrderID)
                        .Sum(od =>
                            double.Parse(od.UnitPrice, CultureInfo.InvariantCulture) *
                            int.Parse(od.Quantity) *
                            (1 - double.Parse(od.Discount, CultureInfo.InvariantCulture)))
                )
            };

        Console.WriteLine("\nStatystyki zamówień pracowników:");
        foreach (var row in q5)
            Console.WriteLine($@"
            Pracownik: {row.Employee.FirstName} {row.Employee.LastName}
            Liczba zamowien: {row.NumOfOrders}
            Srednia wartosc: {row.MeanVal}
            Max wartosc: {row.MaxVal}");
    
    }
}
