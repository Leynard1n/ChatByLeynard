using ChatByLeynard;
using System;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace ChatByLeynard
{
    public partial class ChatWindow : Window
    {
        private Database database;
        private DispatcherTimer timer;

        public ChatWindow(Database db)
        {
            InitializeComponent();
            database = db;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            RefreshMessages();
        }

        private void RefreshMessages()
        {
            DataTable messages = database.GetMessages();
            messagesListBox.Items.Clear();

            foreach (DataRow row in messages.Rows)
            {
                messagesListBox.Items.Add($"{row["id"]}: {row["text"]} (from: {row["from_author"]})");
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string messageText = messageTextBox.Text;
            string author = authorTextBox.Text;

            if (!string.IsNullOrWhiteSpace(messageText) && !string.IsNullOrWhiteSpace(author))
            {
                database.SendMessage(messageText, author);
                messageTextBox.Clear();
                authorTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Сообщение и имя не могут быть пустыми!");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
             MainWindow mainWindow = new MainWindow();

            mainWindow.Show();

            this.Close();
        }
    }
}