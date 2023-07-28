﻿namespace XspecT.Test.Subjects.PurchaseOrder;

public class Basket
{
    public int Id { get; set; }
    public BasketItem[] Items { get; internal set; }
    public int CustomerId { get; set; }
    public int CompanyId { get; set; }
}