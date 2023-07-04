using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RussianSongTranslation.Models;
using System.Data.Entity;

namespace RussianSongTranslation.Pages
{
	public class IndexModel : PageModel
	{
		SongsDbContext db = new SongsDbContext();
		public List<Song> Songs { get; set; }
		public List<Albom> Alboms { get; set; }

		public void Load()
		{
            db.Songs.Load();
            db.Alboms.Load();
            Songs = db.Songs.Include(x => x.Genre).Include(x => x.Songer).ToList();
            Alboms = db.Alboms.Include(x => x.SongInAlboms).ToList();
			UserInfo.CurrentPage = "Index";
        }

		public void OnGet(bool exit = false)
		{
			Load();
			if (exit)
			{
				UserInfo.UserId = 0;
				UserInfo.Email = null;
			}
            UserInfo.StatusButton = "Войти";
            UserInfo.UrlButton = "/Auth";

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
	}
}