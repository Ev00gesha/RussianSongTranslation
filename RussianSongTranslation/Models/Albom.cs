using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RussianSongTranslation.Models
{
    public class Albom
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Url { get; set; }
        [ForeignKey("AlbomId")]
        public ICollection<SongInAlbom> SongInAlboms { get; set; }
    }
}
