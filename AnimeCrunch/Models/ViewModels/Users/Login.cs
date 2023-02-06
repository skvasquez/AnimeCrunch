namespace AnimeCrunch.Models.ViewModels.User {
    using System.ComponentModel.DataAnnotations;
    using AnimeCrunch.Models.DataModels;
    using System.Linq;

    [CustomValidation(typeof(Login), "ValidateLogin")]
    public class Login {
        public static ValidationResult ValidateLogin(Login login) {
            TVShowDb db = new TVShowDb();
            User user = db.Users.SingleOrDefault(u=>u.UserName == login.UserName);
            if (user == null) { return new ValidationResult(AnimeCrunch.Resources.Models.StringsLogin.BadUsernameOrPassword); }
            if (user.Password != login.Password) { return new ValidationResult(AnimeCrunch.Resources.Models.StringsLogin.BadUsernameOrPassword); }
            return ValidationResult.Success;
        }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [Display(Name = "UserName", Description = "YourUserName",ResourceType =typeof(AnimeCrunch.Resources.Models.StringsUser))]
        public string UserName { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Description = "YourPassword", ResourceType = typeof(AnimeCrunch.Resources.Models.StringsUser))]
        public string Password { get; set; }

        [Required]
        [Display(Name = "RememberMe",ResourceType =typeof(AnimeCrunch.Resources.Models.StringsLogin))]
        public bool RememberMe { get; set; }
    }
}