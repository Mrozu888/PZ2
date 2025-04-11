public class OrderDetail
{
    public string OrderID;
    public string ProductID;
    public string UnitPrice;
    public string Quantity;
    public string Discount;

    public OrderDetail(string orderID, string productID, string unitPrice, string quantity, string discount)
    {
        OrderID = orderID;
        ProductID = productID;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Discount = discount;
    }

}
