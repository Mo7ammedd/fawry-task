namespace ECommerceSystem.Models
{
    public class CartItem
    {
        public Product Product { get; }
        public int RequestedQuantity { get; }

        public CartItem(Product product, int requestedQuantity)
        {
            Product = product;
            RequestedQuantity = requestedQuantity;
        }

        public decimal Subtotal => Product.Price * RequestedQuantity;
    }
}
