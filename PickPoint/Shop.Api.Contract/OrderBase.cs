using System.ComponentModel.DataAnnotations;
using Shop.Api.Contract.Infrastructure.Attributes;

namespace Shop.Api.Contract
{
    public abstract class OrderBase
    {
        [Required]
        [ArrayLengthValidation(1, 10)]
        public string[] Products { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public string PostamatId { get; set; }

        [RegularExpression(@"\+7\d{3}(-)\d{3}(-)\d{2}-\d{2}")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^[\p{L} \.'\-]+$")]
        public string RecipientFullName { get; set; }
    }
}