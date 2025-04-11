public class Employee
{
    public string EmployeeID;
    public string LastName;
    public string FirstName;
    public string Title;
    public string TitleOfCourtesy;
    public string BirthDate;
    public string HireDate;
    public string Address;
    public string City;
    public string Region;
    public string PostalCode;
    public string Country;
    public string HomePhone;
    public string Extension;
    public string Photo;
    public string Notes;
    public string ReportsTo;
    public string PhotoPath;

    public Employee(string employeeID, string lastName, string firstName, string title, string titleOfCourtesy,
                    string birthDate, string hireDate, string address, string city, string region,
                    string postalCode, string country, string homePhone, string extension, string photo,
                    string notes, string reportsTo, string photoPath)
    {
        EmployeeID = employeeID;
        LastName = lastName;
        FirstName = firstName;
        Title = title;
        TitleOfCourtesy = titleOfCourtesy;
        BirthDate = birthDate;
        HireDate = hireDate;
        Address = address;
        City = city;
        Region = region;
        PostalCode = postalCode;
        Country = country;
        HomePhone = homePhone;
        Extension = extension;
        Photo = photo;
        Notes = notes;
        ReportsTo = reportsTo;
        PhotoPath = photoPath;
    }
}
