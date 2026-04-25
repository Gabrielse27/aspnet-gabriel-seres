using System.ComponentModel.DataAnnotations;


namespace CoreFitness.Web.Models
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }


    }
}
