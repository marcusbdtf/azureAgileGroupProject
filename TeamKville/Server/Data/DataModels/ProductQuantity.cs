﻿namespace TeamKville.Server.Data.DataModels;

public class ProductQuantity
{
	public int Id { get; set; }
	public Product Product { get; set; }
	public int Quantity { get; set; }
}