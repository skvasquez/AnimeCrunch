namespace AnimeCrunch.Models.DataModels {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [DisplayColumn("Name")]
    public class Studio {
        [Key]
        public int StudioId { get; set; }

        [Required, MaxLength(50), Display(Name= "Name")]
        public string Name { get; set; }

        [InverseProperty("Studio")]
        public virtual ICollection<TvShow> TvShows { get; set; } = new HashSet<TvShow>();

    }
}
