namespace ECommerceSystem.Models
{
    public class CheckoutResult
    {
        public decimal Subtotal { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RemainingBalance { get; set; }
    }
}
