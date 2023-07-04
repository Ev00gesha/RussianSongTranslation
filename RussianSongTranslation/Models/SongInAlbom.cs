using System.ComponentModel.DataAnnotations;

namespace RussianSongTranslation.Models
{
    public class SongInAlbom
    {
        [Key]
        public int Id { get; set; }
        public int AlbomId { get; set; }
        public Albom Albom { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
