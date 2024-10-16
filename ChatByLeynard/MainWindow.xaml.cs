using ChatByLeynard;
using System.Windows;
using System.Windows.Controls;

namespace ChatByLeynard
{
    public partial class MainWindow : Window
    {
        private Database database;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string server = serverTextBox.Text;
            string databaseName = databaseTextBox.Text;
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            database = new Database(server, databaseName, username, password);

            try
            {
                database.OpenConnection();
                MessageBox.Show("Connected to Database!");
                ChatWindow chatWindow = new ChatWindow(database);
                chatWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}");
            }
        }
    }
}