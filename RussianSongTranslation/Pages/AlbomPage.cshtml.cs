using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RussianSongTranslation.Models;
using System.Data.Entity;

namespace RussianSongTranslation.Pages
{
    public class AlbomPageModel : PageModel
    {
        public SongsDbContext db = new SongsDbContext();
        public Albom Albom { get; set; }
        public List<Song> Songs { get; set; } 
        public Songer Songer { get; set; }

        public void Load()
        {
            db.Alboms.Load();
            db.SongsInAlbom.Load();
            Albom = db.Alboms.Where(x => x.Id == UserInfo.AlbomId).FirstOrDefault();
            Songs = db.SongsInAlbom.Where(x => x.AlbomId == UserInfo.AlbomId).Include(x => x.Song).Select(x => x.Song).ToList();
            int songerId = Songs[0].SongerId;
            Songer = db.Songers.Where(x => x.Id == songerId).FirstOrDefault();
            UserInfo.CurrentPage = "AlbomPage";
        }
        public void OnGet(int id)
        {
            if (id != 0)
                UserInfo.AlbomId = id;
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
