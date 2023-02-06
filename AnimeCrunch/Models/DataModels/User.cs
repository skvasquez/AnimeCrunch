namespace AnimeCrunch.Models.DataModels {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User {
        public enum UserType { Administrator, Donator, Member }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [RegularExpression(@"^[A-Za-z]{1,1}[A-Za-z0-9]{3,14}$", ErrorMessageResourceName  ="UsernameMustStartWithALetter", ErrorMessageResourceType =typeof(AnimeCrunch.Resources.Models.StringsUser))]
        [Display(Name = "UserName", Description = "YourUserName",ResourceType =typeof(AnimeCrunch.Resources.Models.StringsUser))]
        public string UserName { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Description = "YourPassword",ResourceType =typeof(AnimeCrunch.Resources.Models.StringsUser))]
        [RegularExpression(@"^[A-Za-z0-9!@/$%#&*?\-""']+$", ErrorMessageResourceName ="UseLettersDigitsAndSymbols",ErrorMessageResourceType =typeof(AnimeCrunch.Resources.Models.StringsUser))]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Description = "YourEmail",ResourceType =typeof(AnimeCrunch.Resources.Models.StringsUser))]
        public string Email { get; set; }

        [EnumDataType(typeof(UserType))]
        public UserType Type { get; set; } = UserType.Member;

        [InverseProperty("Users")]
        public virtual ICollection<TvShow> Favorites { get; set; } = new HashSet<TvShow>();

        [InverseProperty("Users")]
        public virtual ICollection<Episode> History { get; set; } = new HashSet<Episode>();
    }
}