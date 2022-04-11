using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Reader.Model;

namespace WPF.Reader.Service
{
    public class LibraryService
    {
        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouer et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        public Book CurrentBook { get; set; } = new Book();
        private static System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
        // C'est aussi ici que vous ajouterez les requète réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
        private BookClient bookClient = new BookClient(httpClient);

        public void GetBooks()
        {
            try
            {
                var result = bookClient.GetBooks(new List<int>(), 1000, 0);
                Books.Clear();
                foreach (var book in result)
                {
                    Book book1 = new Book();
                    book1.Id = book.Id;
                    book1.Name = book.Name;
                    book1.Author = book.Author;
                    book1.Price = book.Price;
                    book1.Genres = book.Genres;
                    Books.Add(book1);
                }
            }
            finally { }
        }

        public Book GetBook(int id)
        {
            try
            {
                CurrentBook = bookClient.GetBook(id);
            }
            finally
            {
                
            }
            return CurrentBook;
        }
    }
}
