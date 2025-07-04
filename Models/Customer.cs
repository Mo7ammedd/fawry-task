using System;

namespace ECommerceSystem.Models
{
    public class Customer
    {
        public string Name { get; }
        public decimal Balance { get; private set; }

        public Customer(string name, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Customer name cannot be empty");
            if (initialBalance < 0)
                throw new ArgumentException("Initial balance cannot be negative");

            Name = name;
            Balance = initialBalance;
        }

        public bool CanAfford(decimal amount) => Balance >= amount;

        public void DeductAmount(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount to deduct cannot be negative");
            if (amount > Balance)
                throw new InvalidOperationException("Insufficient balance");

            Balance -= amount;
        }

        public void AddFunds(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount to add cannot be negative");

            Balance += amount;
        }
    }
}
