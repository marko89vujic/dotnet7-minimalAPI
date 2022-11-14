using MinimalAPI_Udemy.Models;

namespace MinimalAPI_Udemy.Data
{
    public static class CouponStore
    {
        public static List<Coupon> Coupons = new List<Coupon>
        {
            new Coupon{ Id = 1, Name = "0XFF", IsActive = true, Percent = 10, DateCreated = DateTime.Now, DateModified = DateTime.Now },
            new Coupon{ Id = 2, Name = "0XFA", IsActive = true, Percent = 20, DateCreated = DateTime.Now, DateModified = DateTime.Now },
        };
    }
}
