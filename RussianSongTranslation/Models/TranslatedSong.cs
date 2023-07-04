using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RussianSongTranslation.Models
{
    public class TranslatedSong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Song Song { get; set; }
        public string TranslationName { get; set; }
        public string Translation { get; set; }
    }
}
