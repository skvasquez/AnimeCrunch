namespace AnimeCrunch.Models.ViewModels.User {
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    [CustomValidation(typeof(SignUp), "ValidateSignUp")]
    public class SignUp {
        public static ValidationResult ValidateSignUp(SignUp i) {
            if (!i.IAgree) { return new ValidationResult("YOU MUST AGREE!"); }
            return ValidationResult.Success;
        }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [RegularExpression(@"^[A-Za-z]{1,1}[A-Za-z0-9]{3,14}$", ErrorMessageResourceName = "UsernameMustStartWithALetterHaveBetween4And15CharactersUsingOnlyLettersAndDigits", ErrorMessageResourceType = typeof(AnimeCrunch.Resources.Models.StringsSignUp))]
        [Display(Name = "UserName", Description = "YourUserName" ,ResourceType = typeof(AnimeCrunch.Resources.Models.StringsSignUp))]
        public string UserName { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Description = "YourPassword" ,ResourceType = typeof(AnimeCrunch.Resources.Models.StringsSignUp))]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirmation", Description = "YourPasswordAgain", ResourceType = typeof(AnimeCrunch.Resources.Models.StringsSignUp))]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Description = "YourEmail", ResourceType = typeof(AnimeCrunch.Resources.Models.StringsSignUp))]
        public string Email { get; set; }   

        [Required]
        [Display(Name = "IAgreeToTheSiteConditions", ResourceType = typeof(AnimeCrunch.Resources.Models.StringsSignUp))]
        public bool IAgree { get; set; }
    }
}