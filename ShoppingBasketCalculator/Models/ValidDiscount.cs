using System;

namespace ShoppingBasketCalculator.Models
{
    public class ValidDiscount : IEquatable<ValidDiscount>
    {
        public Offer OfferType { get; set; }
        public decimal DiscountAmount { get; set; }

        public bool Equals(ValidDiscount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(OfferType, other.OfferType) && DiscountAmount == other.DiscountAmount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ValidDiscount) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OfferType, DiscountAmount);
        }
    }
}