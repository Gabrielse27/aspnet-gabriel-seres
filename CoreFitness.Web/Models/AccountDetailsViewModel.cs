using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Web.Models
{
    public class AccountDetailsViewModel
    {

        [Required(ErrorMessage = "Förnamn måste fyllas i")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn måste fyllas i")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "E-postadress måste fyllas i")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Message måste fyllas i")]
        [MinLength(5, ErrorMessage = "Meddelandet måste vara minst 5 tecken")]
        [Display(Name = "Message")]
        public string Message { get; set; } = null!;



        [Phone(ErrorMessage = "Ogiltigt telefonnummer")]
        public string? PhoneNumber { get; set; }



        // Denna används för att visa bilden på sidan
        public string? ProfileImageUrl { get; set; }

        // Denna används för själva uppladdningen av filen
        public IFormFile? ProfileImage { get; set; }
    }


}