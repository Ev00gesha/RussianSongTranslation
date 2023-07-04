using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RussianSongTranslation.Models;
using System.Data.Entity;
using System.Security.Claims;

namespace RussianSongTranslation.Pages
{
    public class AuthModel : PageModel
    {
        SongsDbContext db = new SongsDbContext();
        public static string Error { get; set; }
        public void OnGet()
        {
            db.Users.Load();
        }
        public IActionResult OnPostRegistration(string email, string password, string repeatPassword)
        {
            try
            {
				if (email == null || password == null || repeatPassword == null) throw new Exception("Заполните все поля");
                if (password == repeatPassword)
                {
                    if (db.Users.Any(x => x.Email == email)) throw new Exception("Данный email уже существует");
                    
                    db.Users.Add(new User { Email = email, Password = password });
                    db.SaveChanges();
                    db.Users.Load();
                    User user = db.Users.FirstOrDefault(x => x.Email == email);

                    UserInfo.Email = email;
                    UserInfo.UserId = user.Id;

                    return RedirectToPage(UserInfo.CurrentPage);
                }
                else throw new Exception("Пароли не совпадают");
			}
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToPage("Auth");
        }

        public IActionResult OnPostLogin(string email, string password)
        {
            try
            {
				User user = db.Users.FirstOrDefault(x => x.Email == email);

                if (user == null) throw new Exception("Пользователь не зарегистрирован");
                if (user.Password != password) throw new Exception("Пароли не совпадают");

				UserInfo.Email = email;
                UserInfo.UserId = user.Id;

				return RedirectToPage(UserInfo.CurrentPage);
			}
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToPage("Auth");
        }
    }
}
