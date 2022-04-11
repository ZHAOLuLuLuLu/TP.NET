using System.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Windows.Input;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    public class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ReadCommand { get; init; } = new RelayCommand(x => {
            var service = Ioc.Default.GetRequiredService<INavigationService>();
            service.Navigate<ReadBook>(x);
        });

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public Book CurrentBook { get; init; }

        public DetailsBook(Book book)
        {
            CurrentBook = book;
        }
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new Book() { Name = "Test Book",
            Author="Test Author",
            Price = 1,
            Genres=new[] { new Genre { Id = 1, Name = "classic" }, new Genre { Id = 2, Name = "test genre" } }
        }) { }
    }
}
