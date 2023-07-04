using System.ComponentModel.DataAnnotations;

namespace RussianSongTranslation.Models
{
	public class Comment
	{
		[Key]
		public int Id { get; set; }
		public Song Song { get; set; }
		public int SongId { get; set;}
		public User User { get; set; }
		public int UserId { get; set; }
		public string Text { get; set; }
	}
}
