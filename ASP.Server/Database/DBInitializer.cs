using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {

            if (bookDbContext.Books.Any())
                return;

            Genre SF, Classic, Romance, Thriller;
            bookDbContext.Genre.AddRange(
                SF = new Genre(){Name = "SF"},
                Classic = new Genre(){Name = "Classic"},
                Romance = new Genre(){Name = "Romance"},
                Thriller = new Genre(){Name = "Thriller"}
            );
            bookDbContext.SaveChanges();

            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }
            bookDbContext.Books.AddRange(
                new Book(){ Name = "111", Author = "1", Price = 1, Content = "1111", Genres = new() { Romance, Thriller } }, 
                new Book(){ Name = "222", Author = "2", Price = 1, Content = "2222", Genres = new() { SF, Thriller } },
                new Book(){ Name = "333", Author = "3", Price = 1, Content = "3333", Genres = new() { Romance, SF } },
                new Book(){ Name = "444", Author = "4", Price = 1, Content = "4444", Genres = new() { Classic } }
            );
            // Vous pouvez initialiser la BDD ici

            bookDbContext.SaveChanges();
        }
    }
}