using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RussianSongTranslation.Models
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public string Language { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int SongerId { get; set; }
        public Songer Songer { get; set; }
        [ForeignKey("SongId")]
        public ICollection<Favorites> Favorites { get; set; }
        [ForeignKey("SongId")]
        public ICollection<ProposedTranslation> Translations { get; set; }
        [ForeignKey("SongId")]
        public ICollection<SongInAlbom> SongInAlboms { get; set; }
        [ForeignKey("SongId")]
        public ICollection<Comment> Comments { get; set; }
        public string Url { get; set; }
    }
}
