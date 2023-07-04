using System.ComponentModel.DataAnnotations;

namespace RussianSongTranslation.Models
{
    public class Favorites
    {
        [Key]
        public int Id { get; set; }
        public int SongId { get; set; } 
        public Song Song { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
