namespace AnimeCrunch.Models.DataModels {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Genre {
        [Key]
        public int GenreId { get; set; }

        [Index(IsUnique = true), MaxLength(20)]
        [Display(Name="Name")]
        public string Name { get; set; }

        [InverseProperty("Genres")]
        public virtual ICollection<TvShow> TvShows { get; set; } = new HashSet<TvShow>();

    }
}
