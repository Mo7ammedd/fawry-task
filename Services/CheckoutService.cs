using System;
using System.Linq;
using ECommerceSystem.Models;
using ECommerceSystem.Interfaces;

namespace ECommerceSystem.Services
{
    public class CheckoutService
    {
        private readonly ShippingService shippingService;

        public CheckoutService(ShippingService shippingService)
        {
            this.shippingService = shippingService ?? throw new ArgumentNullException(nameof(shippingService));
        }

        public CheckoutResult ProcessCheckout(Customer customer, ShoppingCart cart)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));

            if (cart.IsEmpty)
                throw new InvalidOperationException("Cannot checkout with an empty cart");

            ValidateProductAvailability(cart);

            var subtotal = cart.CalculateSubtotal();
            var shippingFee = CalculateShippingFee(cart);
            var totalAmount = subtotal + shippingFee;

            if (!customer.CanAfford(totalAmount))
                throw new InvalidOperationException($"Insufficient balance. Required: {totalAmount:C}, Available: {customer.Balance:C}");

            customer.DeductAmount(totalAmount);
            UpdateProductInventory(cart);

            return new CheckoutResult
            {
                Subtotal = subtotal,
                ShippingFee = shippingFee,
                TotalAmount = totalAmount,
                RemainingBalance = customer.Balance
            };
        }

        private void ValidateProductAvailability(ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                if (!item.Product.IsAvailable)
                {
                    var reason = item.Product.Quantity <= 0 ? "out of stock" : "expired";
                    throw new InvalidOperationException($"{item.Product.Name} is {reason}");
                }

                if (item.RequestedQuantity > item.Product.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for {item.Product.Name}. Available: {item.Product.Quantity}");
            }
        }

        private decimal CalculateShippingFee(ShoppingCart cart)
        {
            var shippableItems = cart.GetShippableItems();
            return shippingService.CalculateShippingCost(shippableItems);
        }

        private void UpdateProductInventory(ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                item.Product.Quantity -= item.RequestedQuantity;
            }
        }

        public void PrintOrderSummary(CheckoutResult result)
        {
            Console.WriteLine("=== ORDER SUMMARY ===");
            Console.WriteLine($"Subtotal: {result.Subtotal:C}");
            Console.WriteLine($"Shipping Fee: {result.ShippingFee:C}");
            Console.WriteLine($"Total Amount: {result.TotalAmount:C}");
            Console.WriteLine($"Remaining Balance: {result.RemainingBalance:C}");
            Console.WriteLine("=====================");
        }
    }
}
