using System;

namespace ECommerceSystem.Models
{
    public abstract class Product
    {
        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public int Quantity { get; set; }

        protected Product(string name, decimal price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty");
            if (price < 0)
                throw new ArgumentException("Price cannot be negative");
            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative");

            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public virtual bool IsAvailable => Quantity > 0;
        public abstract bool RequiresShipping { get; }
    }
}
