public class Order
{
    public string OrderID;
    public string CustomerID;
    public string EmployeeID;
    public string OrderDate;
    public string RequiredDate;
    public string ShippedDate;
    public string ShipVia;
    public string Freight;
    public string ShipName;
    public string ShipAddress;
    public string ShipCity;
    public string ShipRegion;
    public string ShipPostalCode;
    public string ShipCountry;

    public Order(string orderID, string customerID, string employeeID, string orderDate, string requiredDate,
                 string shippedDate, string shipVia, string freight, string shipName, string shipAddress,
                 string shipCity, string shipRegion, string shipPostalCode, string shipCountry)
    {
        OrderID = orderID;
        CustomerID = customerID;
        EmployeeID = employeeID;
        OrderDate = orderDate;
        RequiredDate = requiredDate;
        ShippedDate = shippedDate;
        ShipVia = shipVia;
        Freight = freight;
        ShipName = shipName;
        ShipAddress = shipAddress;
        ShipCity = shipCity;
        ShipRegion = shipRegion;
        ShipPostalCode = shipPostalCode;
        ShipCountry = shipCountry;
    }
}
