using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RussianSongTranslation.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [ForeignKey("UserId")]
        public ICollection<Favorites> Favorites { get; set; }
        [ForeignKey("UserId")]
        public ICollection<Comment> Comments { get; set; }
        [ForeignKey("UserId")]
        public ICollection<ProposedTranslation> ProposedTranslations { get; set; }
    }
}
