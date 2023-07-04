using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RussianSongTranslation.Models;
using System.Data.Entity;

namespace RussianSongTranslation.Pages
{
    public class SongPageModel : PageModel
    {
        SongsDbContext db = new SongsDbContext();

        public static string FavouriteStatus { get; set; } = "hidden";
        public static string FormCommentStatus { get; set; } = "hidden";
        public User User { get; set; }
        public Song Song { get; set; }
        public TranslatedSong TranslatedSong { get; set; }
        public List<string> SongSentences { get; set; } = new List<string>();
        public List<string> TranslationSentences { get; set; } = new List<string>();
        public List<Comment> Comments { get; set; }
        public string AlbomOutput { get; set; } = "";
        
        public void Load()
        {
            db.Favorites.Load();
            db.Users.Load();
            db.Songs.Load();
            db.TranslatedSongs.Load();
            db.SongsInAlbom.Include(x => x.Song).Include(x => x.Albom).Load();
            db.Comments.Include(x => x.User).Include(x => x.Song).Load();

            Song = db.Songs.Include(x => x.Songer).Include(x => x.Genre).FirstOrDefault(x => x.Id == UserInfo.SongId);
            User = db.Users.FirstOrDefault(x => x.Id == UserInfo.UserId);
            Comments = db.Comments.Where(x => x.SongId == Song.Id).ToList();
            Comments.Reverse();


            var SongInAlbom = db.SongsInAlbom.FirstOrDefault(x => x.Song.Id == Song.Id);
            if (SongInAlbom != null)
                AlbomOutput = "Альбом: " + SongInAlbom.Albom.Name;

            TranslatedSong = db.TranslatedSongs.FirstOrDefault(x => x.Song.Id == Song.Id);
            
            foreach (string sentences in Song.Text.Split("\r\n\r\n"))
            {
                SongSentences.Add(sentences);
            }
            foreach (string sentences in TranslatedSong.Translation.Split("\r\n\r\n"))
            {
                TranslationSentences.Add(sentences);
            }
            UserInfo.CurrentPage = "SongPage";
        }
        public void OnGet(int id)
        {
            if(id != 0)
                UserInfo.SongId = id;
            Load();

            if(UserInfo.Email == null)
            {
                UserInfo.StatusButton = "Войти";
                UserInfo.UrlButton = "/Auth";
                FavouriteStatus = "hidden";
                FormCommentStatus = "hidden";
            }

            if(UserInfo.Email != null)
            {
                if (UserInfo.Email.Equals("admin@gmail.com"))
                {
                    UserInfo.StatusButton = "Панель администратора";
                    UserInfo.UrlButton = "/AdminPanel";
                    return;
                }

                Favorites favorites = db.Favorites.FirstOrDefault(x => x.UserId == UserInfo.UserId && x.SongId == UserInfo.SongId);
                if (favorites != null)
                    FavouriteStatus = "selected";
                else
                    FavouriteStatus = "";

                FormCommentStatus = "";
                UserInfo.StatusButton = "Аккаунт";
                UserInfo.UrlButton = "/Account";
            }
        }

        public void OnPostFavourite()
        {
            Load();
            if (FavouriteStatus.Equals("selected"))
            {
                FavouriteStatus = "";
                Favorites favorites = db.Favorites.FirstOrDefault(x => x.UserId == UserInfo.UserId && x.SongId == UserInfo.SongId);
                db.Favorites.Remove(favorites);
            }
            else
            {
                FavouriteStatus = "selected";
                db.Favorites.Add(new Favorites { Song = Song, User = User });
            }
            db.SaveChanges();
        }

        public void OnPostComment(string text)
        {
            Load();
            db.Comments.Add(new Comment { Song = Song, User = User, Text = text});
            db.SaveChanges();
            Load();
        }
    }
}
