using ECommerceSystem.Models;

namespace ECommerceSystem.Products
{
    public class MobileScratchCard : Product
    {
        public MobileScratchCard(string name, decimal price, int quantity)
            : base(name, price, quantity)
        {
        }

        public override bool RequiresShipping => false;
    }
}
