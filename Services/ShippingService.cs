using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.Interfaces;

namespace ECommerceSystem.Services
{
    public class ShippingService
    {
        private const decimal BaseShippingFee = 5.00m;
        private const decimal WeightFeePerKg = 2.50m;

        public decimal CalculateShippingCost(List<IShippable> shippableItems)
        {
            if (shippableItems == null || !shippableItems.Any())
                return 0;

            var totalWeight = shippableItems.Sum(item => item.GetWeight());
            return BaseShippingFee + (decimal)totalWeight * WeightFeePerKg;
        }
    }
}
