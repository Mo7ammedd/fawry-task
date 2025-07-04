using System;
using ECommerceSystem.Models;
using ECommerceSystem.Products;
using ECommerceSystem.Services;

namespace ECommerceSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DemonstrateECommerceSystem();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DemonstrateECommerceSystem()
        {
            var customer = new Customer("Mohammed Mostafa", 150.00m);
            var cart = new ShoppingCart();
            var shippingService = new ShippingService();
            var checkoutService = new CheckoutService(shippingService);

            var cheese = new Cheese("Aged Cheddar", 15.99m, 10, DateTime.Now.AddDays(7), 0.5);
            var biscuits = new Biscuits("Chocolate Chip", 4.99m, 20, DateTime.Now.AddDays(30));
            var tv = new Television("Samsung 55-inch", 899.99m, 3, 25.0);
            var scratchCard = new MobileScratchCard("$10 Mobile Credit", 10.00m, 50);

            Console.WriteLine("=== E-COMMERCE SYSTEM ===\n");

            Console.WriteLine("Adding products to cart...");
            cart.AddProduct(cheese, 2);
            cart.AddProduct(biscuits, 3);
            cart.AddProduct(scratchCard, 1);

            Console.WriteLine($"Cart contains {cart.Items.Count} different items");

            var result = checkoutService.ProcessCheckout(customer, cart);
            checkoutService.PrintOrderSummary(result);

            Console.WriteLine("\n=== TESTING ERROR SCENARIOS ===\n");

            TestEmptyCart(customer, checkoutService);
            TestInsufficientBalance(customer, tv, checkoutService);
            TestExpiredProduct(customer, checkoutService);
            TestInsufficientStock(customer, tv, checkoutService);
        }

        static void TestEmptyCart(Customer customer, CheckoutService checkoutService)
        {
            try
            {
                var emptyCart = new ShoppingCart();
                checkoutService.ProcessCheckout(customer, emptyCart);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Empty cart test passed: {ex.Message}");
            }
        }

        static void TestInsufficientBalance(Customer customer, Product expensiveProduct, CheckoutService checkoutService)
        {
            try
            {
                var cart = new ShoppingCart();
                cart.AddProduct(expensiveProduct, 1);
                checkoutService.ProcessCheckout(customer, cart);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Insufficient balance test passed: {ex.Message}");
            }
        }

        static void TestExpiredProduct(Customer customer, CheckoutService checkoutService)
        {
            try
            {
                var expiredCheese = new Cheese("Expired Cheese", 5.99m, 5, DateTime.Now.AddDays(-1), 0.3);
                var cart = new ShoppingCart();
                cart.AddProduct(expiredCheese, 1);
                checkoutService.ProcessCheckout(customer, cart);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Expired product test passed: {ex.Message}");
            }
        }

        static void TestInsufficientStock(Customer customer, Product product, CheckoutService checkoutService)
        {
            try
            {
                var cart = new ShoppingCart();
                cart.AddProduct(product, product.Quantity + 1);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Insufficient stock test passed: {ex.Message}");
            }
        }
    }
}
