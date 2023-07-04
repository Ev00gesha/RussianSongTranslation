using System.Data.Entity;

namespace RussianSongTranslation.Models
{
    public class SongsDbContext:DbContext
    {
        public SongsDbContext() : base("Data Source=.\\SQLEXPRESS;Initial Catalog=Songs;Integrated Security=True") { }
        public DbSet<Albom> Alboms { get; set;}
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Genre> Genres { get; set;}
        public DbSet<ProposedTranslation> UsersTranslations { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Songer> Songers { get; set; }
        public DbSet<TranslatedSong> TranslatedSongs { get; set; }
        public DbSet<SongInAlbom> SongsInAlbom { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; } 
    }
}
