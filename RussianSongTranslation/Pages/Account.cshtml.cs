using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RussianSongTranslation.Models;
using System.Data.Entity;

namespace RussianSongTranslation.Pages
{
	public class AccountModel : PageModel
	{
		public SongsDbContext db = new SongsDbContext();
		public List<Favorites> Favorites { get; set; }

		public void Load()
		{
			db.Favorites.Load();
			Favorites = db.Favorites.Where(x => x.UserId == UserInfo.UserId).Include(x => x.Song).ToList();
		}
		public void OnGet()
		{
			Load();
		}

		private string CreateOutputText()
		{
			Dictionary<string, int> songs = new Dictionary<string, int>();
			for (int i = 0; i < Favorites.Count(); i++)
				songs.Add($"song{i + 1}", Favorites[i].Song.Id);
			return JsonConvert.SerializeObject(songs);
		}

		public IActionResult OnPostCreate()
		{
			Load();

			string fileContent = CreateOutputText();
			string fileName = $"{UserInfo.Email}.json";

			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "", fileName);
			System.IO.File.WriteAllText(filePath, fileContent);

			byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
			System.IO.File.Delete(filePath);

			string contentType = "application/octet-stream";
			return File(fileBytes, contentType, fileName);
		}

		public async void OnPostLoad(IFormFile uploadedFile)
		{
			Load();
			if (uploadedFile != null && uploadedFile.Length > 0)
			{
				var filePath = Path.Combine("", uploadedFile.FileName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
					await uploadedFile.CopyToAsync(fileStream);

				Dictionary<string, int> songs = songs = JsonConvert.DeserializeObject<Dictionary<string, int>>(System.IO.File.ReadAllText(filePath));

				foreach (int value in songs.Values)
				{
					if (Favorites.Any(x => x.SongId == value)) continue;
					db.Favorites.Add(new Favorites { UserId = UserInfo.UserId, SongId = value });
				}
				System.IO.File.Delete(filePath);
				db.SaveChanges();
				Load();
			}
		}

		public void OnPostTranslation(string song, string nameTranslation, string textTranslation)
		{
			Load();
			Song currentSong = db.Songs.Where(x => x.Name == song).FirstOrDefault();
			User user = db.Users.Where(x => x.Email == UserInfo.Email).FirstOrDefault();
			db.UsersTranslations.Add(new ProposedTranslation() { Song = currentSong, User = user, TranslationName = nameTranslation, Translation = textTranslation });
			db.SaveChanges();
		}
	}
}
