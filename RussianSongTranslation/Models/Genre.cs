using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RussianSongTranslation.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("GenreId")]
        public ICollection<Song> Songs { get; set;}
    }
}
