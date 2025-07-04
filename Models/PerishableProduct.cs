using System;

namespace ECommerceSystem.Models
{
    public abstract class PerishableProduct : Product
    {
        public DateTime ExpirationDate { get; private set; }

        protected PerishableProduct(string name, decimal price, int quantity, DateTime expirationDate)
            : base(name, price, quantity)
        {
            ExpirationDate = expirationDate;
        }

        public override bool IsAvailable => base.IsAvailable && !IsExpired;
        public bool IsExpired => DateTime.Now > ExpirationDate;
    }
}
