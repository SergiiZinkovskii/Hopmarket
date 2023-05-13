using System;
using Market.Domain.Enum;

namespace Market.Domain.Entity
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Model { get; set; }

        public double Power { get; set; }

        public decimal Price { get; set; }

        public DateTime DateCreate { get; set; }

        public TypeProduct TypeProduct { get; set; }

        public byte[]? Avatar { get; set; }
    }
}