using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace BlockchainApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        delegate void AddMessage(byte[] message);

        const int port = 54545;
        const string broadcastAddress = "25.255.255.255";
        public UdpClient receivingClient;
        public UdpClient sendingClient;
        Thread receivingThread;

        Blockchain credentials;
        
        int acceptedRequest = 0;
        int userAccounts = 0;
        int counter = 0;
        bool acceptedAccount = false;
        
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeSender();
            InitializeReceiver();

            credentials = Blockchain.DeserializeJsonCredentialsToBlockchain();

            MessagePack mp = new MessagePack();
            mp.Text = "credentialsRequest";
            mp.Sender = userName;
            mp.Hash = credentials.Chain[credentials.Chain.Count() - 1].Hash;
            byte[] data = Blockchain.ObjectToByteArray(mp);
            sendingClient.Send(data, data.Length);

            tbUserName.Focus();
        }

        private string userName = " ";
        
        private void InitializeSender()
        {
            sendingClient = new UdpClient(broadcastAddress, port);
            sendingClient.EnableBroadcast = true;
        }

        private void InitializeReceiver()
        {
            receivingClient = new UdpClient(port);
            ThreadStart start = new ThreadStart(Receiver);
            receivingThread = new Thread(start);
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }

        public void Receiver()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            AddMessage messageDelegate = MessageReceived;

            while (true)
            {
                byte[] message = receivingClient.Receive(ref endPoint);
                Dispatcher.Invoke(messageDelegate, message);
            }
        }

        private void MessageReceived(byte[] message)
        {
            MessagePack mp = (Blockchain.ByteArrayToObject(message)) as MessagePack;

            switch (mp.Text)
            {
                //SYNCHRONIZACJA DANYCH LOGOWANIA
                case "userCredentialsData":
                    if (mp.Sender != userName && mp.Receiver == userName)
                    {
                        userAccounts++;

                        if (mp.listOfBlocks == null)
                            break;

                        credentials = new Blockchain();
                        foreach (Block el in mp.listOfBlocks)
                            credentials.Chain.Add(el);
                        
                        Blockchain.SerializeToJsonCredentials(credentials);
                    }
                    break;

                //TWORZENIE NOWEGO KONTA W SYSTEMIE
                case "createNewUserDecision":
                    counter++;

                    if (mp.SmObj != null)
                        acceptedRequest++;

                    if (acceptedRequest == 2 && acceptedAccount == false)
                    {
                        acceptedAccount = true;
                        credentials.AddBlock(mp.SmObj);
                        MessageBox.Show("Twoje konto w systemie zostało pomyślnie założone!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);//

                        mp.Text = "createNewUserAccepted";
                        byte[] data = Blockchain.ObjectToByteArray(mp);
                        sendingClient.Send(data, data.Length);

                        Blockchain.SerializeToJsonCredentials(credentials);
                    }
                    else if (acceptedRequest < 2 && counter == userAccounts)
                        MessageBox.Show("Użytkownicy sieci nie wyrazili zgody na utworzenie Twojego konta!", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            userName = tbUserName.Text.Trim();
            string password = tbPassword.Password.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Login lub hasło nie mogą być puste!", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUserName.BorderBrush = Brushes.Red;
                tbUserName.Background = Brushes.MistyRose;
                tbPassword.Clear();
                tbPassword.BorderBrush = Brushes.Red;
                tbPassword.Background = Brushes.MistyRose;
                return;
            }
            
            string passhash = ComputeSha256Hash(tbPassword.Password);
            
                            
            string pass="";
            foreach (var p in credentials.Chain)
                if (p.DataCredentials.Login == userName)
                    pass = p.DataCredentials.Password;
            
            if (passhash == pass.ToString())
            {
                Connection c = new Connection(userName, credentials);
                this.Close();
                receivingThread.Abort();
                receivingClient.Close();
                receivingClient = null;
                c.Show();
            }
            else
            {
                MessageBox.Show("Podane login i hasło są nieprawidłowe! Proszę ponownie uzupełnić formularz", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUserName.BorderBrush = Brushes.Red;
                tbUserName.Background = Brushes.MistyRose;
                tbPassword.Clear();
                tbPassword.BorderBrush = Brushes.Red;
                tbPassword.Background = Brushes.MistyRose;
            }
        }
        
        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
 
                StringBuilder passhash = new StringBuilder();
                foreach(byte el in bytes)
                    passhash.Append(el.ToString("x2"));

                return passhash.ToString();
            }
        }

        public void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Czy napewno chcesz zakończyć działanie aplikacji?", "Ostrzeżenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer==MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbUserName.Text) || string.IsNullOrEmpty(tbPassword.Password))
            {
                MessageBox.Show("Login lub hasło nie mogą być puste!", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUserName.BorderBrush = Brushes.Red;
                tbUserName.Background = Brushes.MistyRose;
                tbPassword.Clear();
                tbPassword.BorderBrush = Brushes.Red;
                tbPassword.Background = Brushes.MistyRose;
                return;
            }

            bool userExist = false;

            foreach (var p in credentials.Chain)
            {
                if (p.DataCredentials.Login == tbUserName.Text)
                {
                    userExist = true;
                    break;
                }
            }

            if (userExist == false)
            {
                counter = 0;
                acceptedRequest = 0;
                acceptedAccount = false;

                MessagePack mp = new MessagePack();
                BlockchainCredentials bc = new BlockchainCredentials();
                bc.Login = tbUserName.Text;
                bc.Password = ComputeSha256Hash(tbPassword.Password);
                mp.SmObj = new Block(DateTime.Now, null, bc);
                mp.Text = "createNewUserRequest";
                byte[] data = Blockchain.ObjectToByteArray(mp);
                sendingClient.Send(data, data.Length);
                MessageBox.Show("Prośba o dołączenie do systemu została wysłana do innych użytkowników. Zaczekaj, aż inni użytkownicy zaakceptują Twoją prośbę!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);

                tbUserName.BorderBrush = Brushes.Gray;
                tbUserName.Background = Brushes.White;
                tbPassword.BorderBrush = Brushes.Gray;
                tbPassword.Background = Brushes.White;
            }
            else
            {
                MessageBox.Show("Użytkownik o podanym loginie istnieje już w systemie!", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUserName.BorderBrush = Brushes.Red;
                tbUserName.Background = Brushes.MistyRose;
                tbPassword.Clear();
                tbPassword.BorderBrush = Brushes.Red;
                tbPassword.Background = Brushes.MistyRose;
                return;
            }
        }
    }
}
