using Microsoft.Identity.Client;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_classtask2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Choices= new List<string>() { "Authors", "Themes", "Categories" };
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;


            cmb1.ItemsSource = Choices;
           
        }

        private void cmb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using LibraryContext db = new LibraryContext();

            if(cmb1.SelectedItem != null)
            {
                if (cmb1.SelectedItem.ToString() == Choices[0])
                {
                    cmb2.ItemsSource = db.Authors.ToList();
                }


                else if (cmb1.SelectedItem.ToString() == Choices[1])
                    cmb2.ItemsSource = db.Themes.ToList();


                else
                    cmb2.ItemsSource = db.Categories.ToList();

                
            }
        }
    }
}