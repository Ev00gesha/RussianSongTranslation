using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RussianSongTranslation.Models
{
    public class Songer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("SongerId")]
        public ICollection<Song> Songs { get; set; }
        public string Url { get; set; }

    }
}
