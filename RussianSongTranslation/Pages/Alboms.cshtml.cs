using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RussianSongTranslation.Models;
using System.Data.Entity;

namespace RussianSongTranslation.Pages
{
	public class AlbomsModel : PageModel
	{
		SongsDbContext db = new SongsDbContext();
		public List<Albom> Alboms { get; set; }
		public List<Genre> Genres { get; set; }
		public string AlbomOutput { get; set; }

		public void Load()
		{
			db.Alboms.Load();
			db.Songs.Load();
			db.Songers.Load();
			db.Genres.Load();

			Alboms = db.Alboms.Include(x => x.SongInAlboms).ToList();
			Genres = db.Genres.ToList();
			UserInfo.CurrentPage = "Alboms";
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


		public List<Albom> GetAlbomsReleased(int year)
		{

			var alboms = db.Alboms
				.Where(a => a.ReleaseDate.Year == year)
				.ToList();

			return alboms;

		}

		public void OnPostSort(int albomYear)
		{
			Load();
			Alboms = GetAlbomsReleased(albomYear);
		}
	}
}
