using System;
using ECommerceSystem.Models;
using ECommerceSystem.Interfaces;

namespace ECommerceSystem.Products
{
    public class Television : Product, IShippable
    {
        public double Weight { get; private set; }

        public Television(string name, decimal price, int quantity, double weight)
            : base(name, price, quantity)
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
