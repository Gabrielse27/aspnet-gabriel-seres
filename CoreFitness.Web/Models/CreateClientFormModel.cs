using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Web.Models
{
    public class CreateClientFormModel
    {
        [Display(Name = "First Name", Prompt="Enter First name")]
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; } = null!;


        [Display(Name = "Last Name", Prompt = "Enter Last name")]
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; } = null!;


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Enter e-mail adress")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]
        public string Email { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number", Prompt = "Enter phone number")]
        public string? PhoneNumber { get; set; }


        [Display(Name = "Message", Prompt = "Write your message here")]
        [Required(ErrorMessage = "Message måste fyllas i")]
        [MinLength(5, ErrorMessage = "Meddelandet måste vara minst 5 tecken")]
        public string Message { get; set; } = null!;

    }
}
