using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RomelSportingGoods.Models
{
    public class BillingInfo
    {
        [DisplayName("Full Name")]
        public string name { get; set; } = string.Empty;

        [DisplayName("Address")]
        public string address { get; set; } = string.Empty;
        
        [DisplayName("City")]
        public string city { get; set; } = string.Empty;
        
        [DisplayName("Province")]
        public string province { get; set; } = string.Empty;

        [DisplayName("Postal Code")]
        public string postalCode { get; set; } = string.Empty;

        [DisplayName("Country")]
        public string country { get; set; } = string.Empty;

        [DisplayName("Credit Card")]
        [Range(1111111111111111,9999999999999999, ErrorMessage="Value must be 16 digits")]
        [Column(TypeName = "bigint(16)")]
        public ulong ccNumber { get; set; }

        [DisplayName("Credit Card Expiration (MMYY)")]
        public string ccExpiryDate { get; set; } = string.Empty;

        [DisplayName("CVV")]
        [Range(100, 999, ErrorMessage = "Value must be 3 digits")]
        public int cvv { get; set; }
        public string products { get; set; } = string.Empty;
    }
}
