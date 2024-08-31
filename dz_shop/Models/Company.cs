﻿namespace dz_shop.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Item> Items { get; set; } = new();
    }
}
