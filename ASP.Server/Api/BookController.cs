using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Server.Database;

namespace ASP.Server.Api
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext libraryDbContext;

        public BookController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        // Methode a ajouter : 
        // - GetBooks
        //   - Entrée: Optionel -> Liste d'Id de genre, limit -> defaut à 10, offset -> défaut à 0
        //     Le but de limit et offset est dé créer un pagination pour ne pas retourner la BDD en entier a chaque appel
        //   - Sortie: Liste d'object contenant uniquement: Auteur, Genres, Titre, Id, Prix
        //     la liste restourner doit être compsé des élément entre <offset> et <offset + limit>-
        //     Dans [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20] si offset=8 et limit=5, les élément retourner seront : 8, 9, 10, 11, 12

        // - GetBook
        //   - Entrée: Id du livre
        //   - Sortie: Object livre entier

        // - GetGenres
        //   - Entrée: Rien
        //   - Sortie: Liste des genres

        // Aide:
        // Pour récupéré un objet d'une table :
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.First()
        // Pour récupéré des objets d'une table :
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.ToList()
        // Pour faire une requète avec filtre:
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.Skip().<Selecteurs>
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.Take().<Selecteurs>
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.Where(x => x == y).<Selecteurs>
        // Pour récupérer une 2nd table depuis la base:
        //   - .Include(x => x.yyyyy)
        //     ou yyyyy est la propriété liant a une autre table a récupéré
        //
        // Exemple:
        //   - Ex: libraryDbContext.MyObjectCollection.Include(x => x.yyyyy).Where(x => x.yyyyyy.Contains(z)).Skip(i).Take(j).ToList()


        // Vous vous montre comment faire la 1er, a vous de la compléter et de faire les autres !
        public ActionResult<List<BookView>> GetBooks(List<int> genresIds, int limit=10, int offset=0)
        {
            List<Book> books = null;
            List<Genre> listGenres = new List<Genre>();
            foreach (int gen in genresIds)
            {
                 var g = this.libraryDbContext.Genre.Find(gen);
                 if (g != null)
                 {
                     listGenres.Add(g);
                 }
            }
            if (listGenres.Count()>0) 
            {
                var exp = libraryDbContext.Books.Include(x => x.Genres).Where(x => x.Genres.Contains(listGenres[0]));
                listGenres.ForEach(x =>
                {
                    exp = exp.Where(t => t.Genres.Contains(x));
                });
                books = exp.Where(b => b.Price > 0).Skip(offset).Take(limit).ToList();
            }
            else {
                books =  libraryDbContext.Books.Include(x => x.Genres).Where(b => b.Price > 0).Skip(offset).Take(limit).ToList();
            }
            var bookViews = new List<BookView>();
            foreach (Book book in books)
            {
                var bb = new BookView(){Id = book.Id, Author = book.Author, Name = book.Name, Price = book.Price};
                bb.Genres = book.Genres;
                bookViews.Add(bb);
            }
            return bookViews;
        }

        public ActionResult<Book> GetBook(int id)
        {
            return libraryDbContext.Books.Find(id);
        }
        public ActionResult<List<Genre>> GetGenres()
        {
            List<Genre> ListGenres = null;
            ListGenres = this.libraryDbContext.Genre.ToList();
            return ListGenres;
        }

    }
}

