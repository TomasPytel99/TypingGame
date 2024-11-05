using System.IO;
using System.Windows;
using System.Windows.Controls;
using TypingGame.GameLogic;

namespace TypingGame.Pages
{
    /// <summary>
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        private readonly MainWindow parent;
        private readonly List<HistoryRecord> records;
        public HistoryPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.parent = mainWindow;
            this.records = [];
            StreamReader streamReader = new(@"..\\..\\..\\History.csv");
            while (!streamReader.EndOfStream)
            {
                string? line = streamReader.ReadLine();
                if (line != null)
                {
                    string[] buffer = line.Split(';'); //chat gpt
                    if (int.TryParse(buffer[3], out int lives))
                    {
                        HistoryRecord historyRecord = new(buffer[0], buffer[1], buffer[2], lives, buffer[4]);
                        records.Add(historyRecord);
                    }
                }
            }
            MyListView.ItemsSource = records;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            parent.PageOrder--;
            parent.Frame.Content = parent.Pages[parent.PageOrder];
            parent.Pages.Remove(parent.Pages[^1]);
        }
    }
}
