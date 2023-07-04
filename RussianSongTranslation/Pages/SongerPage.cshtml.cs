using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RussianSongTranslation.Models;
using System.Data.Entity;

namespace RussianSongTranslation.Pages
{
    public class SongerPageModel : PageModel
    {
        public SongsDbContext db = new SongsDbContext();
        public Songer Songer { get; set; }
        public List<Song> Songs { get; set; }
        
        public void Load()
        {
            db.Songers.Load();
            db.Genres.Load();
            Songer = db.Songers.Where(x => x.Id == UserInfo.SongerId).Include(x => x.Songs).FirstOrDefault();
            Songs = Songer.Songs.ToList();
            UserInfo.CurrentPage = "SongerPage";
        }
        public void OnGet(int id)
        {
            if(id != 0)
                UserInfo.SongerId = id;
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
    }
}
