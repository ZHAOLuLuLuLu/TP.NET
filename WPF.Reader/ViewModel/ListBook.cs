using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    public class ListBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ItemSelectedCommand { get; set; }

        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!
        public ObservableCollection<Book> Books => Ioc.Default.GetRequiredService<LibraryService>().Books;

        public ListBook()
        {
            ItemSelectedCommand = new RelayCommand(book =>
            {
                var service = Ioc.Default.GetRequiredService<INavigationService>();
                service.Navigate<DetailsBook>(book);
            });
            Ioc.Default.GetRequiredService<LibraryService>().GetBooks();
        }

    }
}