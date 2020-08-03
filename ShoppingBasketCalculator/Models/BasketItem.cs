using System;

namespace ShoppingBasketCalculator.Models
{
    public class BasketItem : IEquatable<BasketItem>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int NumberOfItems { get; set; }
        public Offer OfferType { get; set; }
        public decimal PricePerItem { get; set; }

        public bool Equals(BasketItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ProductId == other.ProductId && ProductName == other.ProductName && NumberOfItems == other.NumberOfItems && Equals(OfferType, other.OfferType) && PricePerItem == other.PricePerItem;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BasketItem) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductId, ProductName, NumberOfItems, OfferType, PricePerItem);
        }
    }
}