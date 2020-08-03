using System;

namespace ShoppingBasketCalculator.Models
{
    public class Offer : IEquatable<Offer>
    {
        public bool Equals(Offer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ProductId == other.ProductId && DiscountCode == other.DiscountCode && DiscountDetail == other.DiscountDetail;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Offer) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductId, DiscountCode, DiscountDetail);
        }

        public int ProductId { get; set; }
        public string DiscountCode { get; set; }
        public string DiscountDetail { get; set; }
    }
}