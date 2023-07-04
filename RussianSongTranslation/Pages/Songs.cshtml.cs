using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RussianSongTranslation.Models;
using System.Data.Entity;

namespace RussianSongTranslation.Pages
{
    public class SongsModel : PageModel
    {
        public SongsDbContext db = new SongsDbContext();
        public List<Song> Songs { get; set; } = new List<Song>();
		public List<Genre> Genres { get; set; } = new List<Genre>();

        public void Load()
        {
            db.Songs.Load();
            db.Genres.Include(x => x.Songs).Load();

            Songs = db.Songs.Include(x => x.Genre).ToList();
			Genres = db.Genres.ToList();
            UserInfo.CurrentPage = "Songs";
        }
        public void OnGet()
        {
			Load();

			if (UserInfo.Email == null)
			{
				UserInfo.StatusButton = "Войти";
				UserInfo.UrlButton = "/Auth";
			}

			if (UserInfo.Email != null)
			{
				if (UserInfo.Email.Equals("admin@gmail.com"))
				{
					UserInfo.StatusButton = "Панель администратора";
					UserInfo.UrlButton = "/AdminPanel";
					return;
				}

				UserInfo.StatusButton = "Аккаунт";
				UserInfo.UrlButton = "/Account";
			}
		}

		public void OnPostSort(string genre, string language)
		{
			Load();
			Genre genreQuery = db.Genres.Where(x => x.Name == genre).FirstOrDefault();
			if (genre == null && language == null) return;
			if (genre == null) Songs = db.Songs.Where(x => x.Language == language).ToList();
			if (language == null)
			{
				Songs = db.Songs.Where(x => x.GenreId == genreQuery.Id).ToList();
			}
			if (genre != null && language != null) Songs = db.Songs.Where(x => x.Language == language && x.Genre.Id == genreQuery.Id).ToList();
		}
    }
}
