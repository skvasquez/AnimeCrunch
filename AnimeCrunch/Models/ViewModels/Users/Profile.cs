namespace AnimeCrunch.Models.ViewModels.User {
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Linq;
    using AnimeCrunch.Models.DataModels;
    using System;

    [CustomValidation(typeof(Profile), "ValidateProfile")]
    public class Profile {
        public static ValidationResult ValidateProfile(Profile profil) {
            if (!string.IsNullOrEmpty(profil.NewPassword)) {
                TVShowDb db = new TVShowDb();
                User u = db.Users.Find(int.Parse(HttpContext.Current.User.Identity.Name));
                if (u.Password != profil.OldPassword) {
                    return new ValidationResult(AnimeCrunch.Resources.Models.StringsProfile.YourOldPasswordDoesNotMatchYourPasswordHasNotBeenChanged, new[] { "" });
                }
            }
            return ValidationResult.Success;
        }

        [MaxLength(15)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword", Description = "YourOldPassword", ResourceType = typeof(AnimeCrunch.Resources.Models.StringsProfile))]
        public string OldPassword { get; set; }

        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", Description = "YourNewPassword",ResourceType = typeof(AnimeCrunch.Resources.Models.StringsProfile))]
        [Compare("ConfirmNewPassword")]
        [RegularExpression(@"^[A-Za-z0-9!@/$%#&*?\-""']+$", ErrorMessageResourceName = "UseLettersDigitsAndSymbols", ErrorMessageResourceType = typeof(AnimeCrunch.Resources.Models.StringsUser))]
        public string NewPassword { get; set; }

        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", Description = "YourNewPasswordAgain",ResourceType =typeof(AnimeCrunch.Resources.Models.StringsProfile))]
        [Compare("NewPassword")]
        [RegularExpression(@"^[A-Za-z0-9!@/$%#&*?\-""']+$", ErrorMessageResourceName = "UseLettersDigitsAndSymbols", ErrorMessageResourceType = typeof(AnimeCrunch.Resources.Models.StringsUser))]
        public string ConfirmNewPassword { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Description = "YourEmail",ResourceType =typeof(AnimeCrunch.Resources.Models.StringsProfile))]
        public string Email { get; set; }
    }
}