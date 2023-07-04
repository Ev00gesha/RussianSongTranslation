using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RussianSongTranslation.Models;
using System.Data.Entity;

namespace RussianSongTranslation.Pages
{
    public class AdminPanelModel : PageModel
    {
        public static string Form1 { get; set; } = "";
        public static string Form2 { get; set; } = "hidden";
        public static string Form3 { get; set; } = "hidden";
		public static string Form4 { get; set; } = "hidden";
		public static string Form5 { get; set; } = "hidden";
		public static string Form6 { get; set; } = "hidden";
		public SongsDbContext db = new SongsDbContext();

        
		public void OnGet()
        {
            db.Genres.Load();
            db.Songers.Load();
            db.Songs.Load();
            db.Alboms.Load();
			db.SongsInAlbom.Load();
        }

        public void OnPostSong(string songTitle, string songText, string duration, string language, string genre, string singer, string songLink)
		{
			HiddenForms();
			Form1 = "";
			Genre songGenre = db.Genres.Where(x => x.Name == genre).FirstOrDefault();
			Songer songer = db.Songers.Where(x => x.Name == singer).FirstOrDefault();
			db.Songs.Add(new Song { Name = songTitle, Text = songText, Duration = Convert.ToInt32(duration), Language = language, Genre = songGenre, Songer = songer, Url = songLink });
			db.SaveChanges();
		}

        public void OnPostAlbom(string nameAlbom, string releaseDate, string albomLink)
		{
			HiddenForms();
			Form2 = "";
			db.Alboms.Add(new Albom { Name = nameAlbom, ReleaseDate = DateTime.Parse(releaseDate), Url = albomLink });
			db.SaveChanges();
		}

        public void OnPostGenre(string nameGenre)
        {
			HiddenForms();
			Form3 = "";
			db.Genres.Add(new Genre { Name = nameGenre });
			db.SaveChanges();
		}

        public void OnPostSonger(string nameSonger, string description, string songerLink)
        {
            HiddenForms();
            Form4 = "";
			db.Songers.Add(new Songer { Name = nameSonger, Description = description, Url = songerLink });
			db.SaveChanges();
		}
		public void OnPostTranslation(string song, string nameTranslation, string translationText)
		{
			HiddenForms();
			Form5 = "";
			Song songTranslated = db.Songs.Where(x => x.Name == song).FirstOrDefault();
			db.TranslatedSongs.Add(new TranslatedSong { Song = songTranslated, TranslationName=nameTranslation, Translation = translationText });
			db.SaveChanges();
		}

		public void OnPostSongInAlbom(string albom, string song)
		{
			HiddenForms();
			Form6 = "";
			Albom albom1 = db.Alboms.Where(x => x.Name == albom).FirstOrDefault();
			Song song1 = db.Songs.Where(x => x.Name == song).FirstOrDefault();
			db.SongsInAlbom.Add(new SongInAlbom { Albom = albom1, Song = song1});
			db.SaveChanges();
		}
		public void HiddenForms()
		{
			Form1 = "hidden";
			Form2 = "hidden";
			Form3 = "hidden";
			Form4 = "hidden";
			Form5 = "hidden";
			Form6 = "hidden";

			UserInfo.StatusButton = "Войти";
			UserInfo.UrlButton = "/Auth";
		}
	}
}
