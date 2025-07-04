using System;
using ECommerceSystem.Models;
using ECommerceSystem.Interfaces;

namespace ECommerceSystem.Products
{
    public class Cheese : PerishableProduct, IShippable
    {
        public double Weight { get; private set; }

        public Cheese(string name, decimal price, int quantity, DateTime expirationDate, double weight)
            : base(name, price, quantity, expirationDate)
        {
            if (weight <= 0)
                throw new ArgumentException("Weight must be positive");
            Weight = weight;
        }

        public override bool RequiresShipping => true;

        public string GetName() => Name;
        public double GetWeight() => Weight;
    }
}
