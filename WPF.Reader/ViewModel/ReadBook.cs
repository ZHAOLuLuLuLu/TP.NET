using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    class ReadBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // A vous de jouer maintenant
        public Book CurrentBook =>Ioc.Default.GetRequiredService<LibraryService>().CurrentBook;
 

        public ReadBook(Book book)
        {
            _ = Ioc.Default.GetRequiredService<LibraryService>().GetBook(book.Id);
        }

    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    class InDesignReadBook : ReadBook
    {
        public InDesignReadBook() : base(new Book()
        {
            Name = "Test Book",
            Author = "Test Author",
            Price = 1,
            Content = "qwertyuiolfghjk",
            Genres = new[] { new Genre { Id = 1, Name = "classic" }, new Genre { Id = 2, Name = "test genre" } }
        })
        { }
    }
}
