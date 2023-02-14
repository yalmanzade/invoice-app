using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoice.Models
{
    public class Company
    {
        [Key]
        public ulong Id { get; set; }
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 50, MinimumLength = 3,
        ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters.")]
        public string? Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        //[StringLength(maximumLength: 10, MinimumLength = 11, ErrorMessage = "The Phone Number should be between 10 and 11 characters.")]
        public string? Phone { get; set; }
        [DataType(DataType.Text)]
        public string? Address { get; set; }
        [NotMapped]
        public string? FormatedPhone
        {
            get
            {
                if (this.Phone == null)
                {
                    return "Invalid Phone Number.";
                }
                else
                {
                    var phone = String.Format("{0:(###) ###-####}", double.Parse(this.Phone));
                    return phone;
                }
            }
        }
    }
}