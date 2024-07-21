using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp_classtask2
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public List<string> Choices = new List<string>() { "Authors", "Themes", "Categories" };

        private List<Book> books;

        public List<Book> Books { get => books; set { books = value; OnPropertyChanged(); } }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;


            cmb1.ItemsSource = Choices;

        }

        private void cmb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using LibraryContext db = new LibraryContext();

            if (cmb1.SelectedItem != null)
            {
                if (cmb1.SelectedItem.ToString() == Choices[0])
                    cmb2.ItemsSource = db.Authors.ToList();



                else if (cmb1.SelectedItem.ToString() == Choices[1])
                    cmb2.ItemsSource = db.Themes.ToList();


                else
                    cmb2.ItemsSource = db.Categories.ToList();


            }
        }

        private void cmb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using LibraryContext db = new LibraryContext();
            string text = cmb1.SelectedItem.ToString();

            if (text is not null)
            {
                List<Book> myBooks = new();

                if (text == "Authors")
                {
                    var author = db.Authors.FirstOrDefault(a => (a.FirstName + " " + a.LastName) == cmb2.SelectedItem.ToString());
                    myBooks = db.Books.Where(b => b.IdAuthor == author.Id).ToList();
                    
                }

                else if (text == "Themes")
                {
                    var theme = db.Themes.FirstOrDefault(a => a.Name == cmb2.SelectedItem.ToString());
                    myBooks = db.Books.Where(b => b.IdThemes == theme.Id).ToList();
                }

                else
                {
                    if(cmb2.SelectedItem.ToString() is not null)
                    {
                        var category = db.Categories.FirstOrDefault(a => a.Name == cmb2.SelectedItem.ToString());
                        myBooks = db.Books.Where(b => b.IdCategory == category.Id).ToList();
                    }
                 
                }

                Books = new();
                Books = myBooks;

            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}