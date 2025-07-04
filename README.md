# E-Commerce System



## Project Structure

```
ECommerceSystem/
├── Interfaces/
│   └── IShippable.cs           # Interface for products requiring shipping
├── Models/
│   ├── Product.cs              # Abstract base product class
│   ├── PerishableProduct.cs    # Base class for expiring products
│   ├── Customer.cs             # Customer with balance management
│   ├── CartItem.cs             # Individual cart item
│   ├── ShoppingCart.cs         # Shopping cart functionality
│   └── CheckoutResult.cs       # Checkout result data
├── Products/
│   ├── Cheese.cs               # Perishable, shippable product
│   ├── Biscuits.cs             # Perishable, non-shippable product
│   ├── Television.cs           # Non-perishable, shippable product
│   └── MobileScratchCard.cs    # Non-perishable, non-shippable product
├── Services/
│   ├── ShippingService.cs      # Shipping cost calculations
│   └── CheckoutService.cs      # Order processing and validation
└── Program.cs                  # Demo application
```

## Architecture

### Core Classes

- **Product**: Abstract base class for all products
- **PerishableProduct**: Extends Product with expiration date functionality
- **IShippable**: Interface for products that require shipping
- **Customer**: Manages customer information and balance
- **ShoppingCart**: Handles product selection and quantity management
- **CheckoutService**: Processes orders and coordinates payment
- **ShippingService**: Calculates shipping costs for shippable items

### Product Types

1. **Cheese**: Perishable product that requires shipping
2. **Biscuits**: Perishable product that doesn't require shipping
3. **Television**: Non-perishable product that requires shipping
4. **MobileScratchCard**: Non-perishable product that doesn't require shipping

## Business Rules

- Products can be perishable (with expiration dates) or non-perishable
- Some products require shipping and have weight for shipping calculations
- Customers cannot purchase more items than available in stock
- Expired products cannot be purchased
- Customers must have sufficient balance to complete purchases
- Shipping costs are calculated based on base fee plus weight-based charges

## Error Handling

The system handles the following error scenarios:
- Empty cart checkout attempts
- Insufficient customer balance
- Out of stock products
- Expired products
- Invalid quantities

## Running the Application

```bash
dotnet run
```

The application will demonstrate:
1. Adding products to cart
2. Successful checkout process
3. Error scenario testing

