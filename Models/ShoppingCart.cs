using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.Interfaces;

namespace ECommerceSystem.Models
{
    public class ShoppingCart
    {
        private readonly List<CartItem> items = new List<CartItem>();

        public IReadOnlyList<CartItem> Items => items.AsReadOnly();
        public bool IsEmpty => !items.Any();

        public void AddProduct(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");
            if (!product.IsAvailable)
                throw new InvalidOperationException($"{product.Name} is not available");
            if (quantity > product.Quantity)
                throw new InvalidOperationException($"Insufficient stock for {product.Name}. Available: {product.Quantity}");

            var existingItem = items.FirstOrDefault(item => item.Product == product);
            if (existingItem != null)
            {
                var totalRequested = existingItem.RequestedQuantity + quantity;
                if (totalRequested > product.Quantity)
                    throw new InvalidOperationException($"Total requested quantity exceeds available stock for {product.Name}");
                
                items.Remove(existingItem);
                items.Add(new CartItem(product, totalRequested));
            }
            else
            {
                items.Add(new CartItem(product, quantity));
            }
        }

        public decimal CalculateSubtotal()
        {
            return items.Sum(item => item.Subtotal);
        }

        public List<IShippable> GetShippableItems()
        {
            var shippableItems = new List<IShippable>();
            
            foreach (var item in items.Where(i => i.Product.RequiresShipping))
            {
                if (item.Product is IShippable shippable)
                {
                    for (int i = 0; i < item.RequestedQuantity; i++)
                    {
                        shippableItems.Add(shippable);
                    }
                }
            }
            
            return shippableItems;
        }

        public void Clear()
        {
            items.Clear();
        }
    }
}
