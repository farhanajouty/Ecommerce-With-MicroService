﻿namespace Mango.Services.CouponAPI.Models.Dto
{
    public class CouponDto
    {
          
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public string DiscountAmount { get; set; }
        public string MinAmount { get; set; }
    }
}
