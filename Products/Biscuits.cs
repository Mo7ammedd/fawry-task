using System;
using ECommerceSystem.Models;

namespace ECommerceSystem.Products
{
    public class Biscuits : PerishableProduct
    {
        public Biscuits(string name, decimal price, int quantity, DateTime expirationDate)
            : base(name, price, quantity, expirationDate)
        {
        }

        public override bool RequiresShipping => false;
    }
}
