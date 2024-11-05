using System.Windows;
using System.Windows.Controls;

namespace TypingGame.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly MainWindow parent;
        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            parent = mainWindow;
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            parent.MovePageForward();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            parent.Close();
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            HistoryPage historyPage = new(this.parent);
            this.parent.Pages.Add(historyPage);
            this.parent.Frame.Content = historyPage;
            this.parent.PageOrder++;
        }
    }
}
