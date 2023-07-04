using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Mapping;

namespace RussianSongTranslation.Models
{
    public class ProposedTranslation
    {
        [Key]
        public int Id { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string TranslationName { get; set; }
        public string Translation { get; set; }

    }
}
