using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace BlockchainApp
{
    /// <summary>
    /// Logika interakcji dla klasy AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        delegate void AddMessage(byte[] message);
        const int port = 54545;
        const string broadcastAddress = "25.255.255.255";
        public UdpClient receivingClient;
        public UdpClient sendingClient;

        public IEnumerable<string> UniqID = new List<string>();
        public string UserID; 

        public AddProduct(IEnumerable<string> chainUniqID, string userName)
        {
            InitializeComponent();
            UniqID = chainUniqID;
            UserID = userName;
        }

        private void InitializeSender()
        {
            sendingClient = new UdpClient(broadcastAddress, port);
            sendingClient.EnableBroadcast = true;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            Regex PriceValue = new Regex(@"^\d+(\,[0-9]{1,2})?$");
            Regex DepreciationRate = new Regex(@"^(100|[0-9]{1,2})$");
        
            if (tbRegNumber.Text == String.Empty || tbName.Text == String.Empty || tbSymbolKST.Text == String.Empty || cbStatus.Text == String.Empty || tbPurchaseDate.Text == String.Empty
                                                || tbStartDate.Text == String.Empty || tbDepreciationStartDate.Text == String.Empty || tbType.Text == String.Empty
                                                || tbOriginValue.Text == String.Empty || tbDepreciationRate.Text == String.Empty)
            {
                MessageBox.Show("Formularz nie jest w pełni wypełniony. Proszę uzupełnić wymagane pola!", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                
                if (tbRegNumber.Text == String.Empty) { tbRegNumber.BorderBrush = Brushes.Red; tbRegNumber.Background = Brushes.MistyRose; }
                    else { tbRegNumber.BorderBrush = Brushes.LightSlateGray; tbRegNumber.Background = Brushes.White; }
                if (tbName.Text == String.Empty) { tbName.BorderBrush = Brushes.Red; tbName.Background = Brushes.MistyRose; }
                    else { tbName.BorderBrush = Brushes.LightSlateGray; tbName.Background = Brushes.White; }
                if (tbSymbolKST.Text == String.Empty) { tbSymbolKST.BorderBrush = Brushes.Red; tbSymbolKST.Background = Brushes.MistyRose; }
                    else { tbSymbolKST.BorderBrush = Brushes.LightSlateGray; tbSymbolKST.Background = Brushes.White; }
                if (tbType.Text == String.Empty) { tbType.BorderBrush = Brushes.Red; tbType.Background = Brushes.MistyRose; }
                    else { tbType.BorderBrush = Brushes.LightSlateGray; tbType.Background = Brushes.White; }
                //if (cbStatus.Text == String.Empty) { cbStatus.BorderBrush = Brushes.Red; cbStatus.Background = Brushes.MistyRose; }
                if (tbPurchaseDate.Text == String.Empty) { tbPurchaseDate.BorderBrush = Brushes.Red; tbPurchaseDate.Background = Brushes.MistyRose; }
                    else { tbPurchaseDate.BorderBrush = Brushes.Gray; tbPurchaseDate.Background = Brushes.White; }
                if (tbStartDate.Text == String.Empty) { tbStartDate.BorderBrush = Brushes.Red; tbStartDate.Background = Brushes.MistyRose; } 
                    else { tbStartDate.BorderBrush = Brushes.Gray; tbStartDate.Background = Brushes.White; }
                if (tbDepreciationStartDate.Text == String.Empty) { tbDepreciationStartDate.BorderBrush = Brushes.Red; tbDepreciationStartDate.Background = Brushes.MistyRose; }
                    else { tbDepreciationStartDate.BorderBrush = Brushes.Gray; tbDepreciationStartDate.Background = Brushes.White; }
                if (tbOriginValue.Text == String.Empty) { tbOriginValue.BorderBrush = Brushes.Red; tbOriginValue.Background = Brushes.MistyRose; }
                    else { tbOriginValue.BorderBrush = Brushes.LightSlateGray; tbOriginValue.Background = Brushes.White; }
                if (tbDepreciationRate.Text == String.Empty) { tbDepreciationRate.BorderBrush = Brushes.Red; tbDepreciationRate.Background = Brushes.MistyRose; }
                    else { tbDepreciationRate.BorderBrush = Brushes.LightSlateGray; tbDepreciationRate.Background = Brushes.White; }
            }
            else if (UniqID.Contains(tbRegNumber.Text))
            {
                MessageBox.Show("W systemie istnieje już środek o podanym numerze inwentaryzacyjnym! Proszę wprowadzić inny identyfikator", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                RestoreTextboxProperties();
                tbRegNumber.BorderBrush = Brushes.Red; tbRegNumber.Background = Brushes.MistyRose;
            }
            else if (Convert.ToDateTime(tbStartDate.Text) < Convert.ToDateTime(tbPurchaseDate.Text))
            {
                MessageBox.Show("Data rozpoczęcia użytkowania nie może być wcześniejsza niż data zakupu", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                RestoreTextboxProperties();
                tbStartDate.BorderBrush = Brushes.Red; tbStartDate.Background = Brushes.MistyRose;
                tbPurchaseDate.BorderBrush = Brushes.Red; tbPurchaseDate.Background = Brushes.MistyRose;
            }
            else if (!PriceValue.IsMatch(tbOriginValue.Text))
            {
                MessageBox.Show("Pole wartość początkowa ma niepoprawny format! Wartość powinna mieć postać: xxxx,xx", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                RestoreTextboxProperties();
                tbOriginValue.BorderBrush = Brushes.Red; tbOriginValue.Background = Brushes.MistyRose;
            }
            else if (!DepreciationRate.IsMatch(tbDepreciationRate.Text))
            {
                MessageBox.Show("Pole stawka amortyzacji ma niepoprawny format! Wartość powinna zawierać się w przedziale 0-100", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                RestoreTextboxProperties();
                tbDepreciationRate.BorderBrush = Brushes.Red; tbDepreciationRate.Background = Brushes.MistyRose;
            }
            else if (tbAmortizedValue.Text != String.Empty && !PriceValue.IsMatch(tbAmortizedValue.Text))
            {
                MessageBox.Show("Pole wartość zamortyzowana ma niepoprawny format! Wartość powinna mieć postać: xxxx,xx", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                RestoreTextboxProperties();
                tbAmortizedValue.BorderBrush = Brushes.Red; tbAmortizedValue.Background = Brushes.MistyRose;
            }
            else
                SendMessage();
        }
        private void SendMessage()
        {
            MessagePack mp = new MessagePack();
            BlockchainData bd = new BlockchainData();

            //Dane podstawowe
            bd.NumerInwentaryzacyjny = tbRegNumber.Text;
            bd.Nazwa = tbName.Text;
            bd.NumerFabryczny = tbSerialNumber.Text;
            bd.Model = tbModel.Text;
            bd.Producent = tbProducer.Text;
            bd.SymbolKST = tbSymbolKST.Text;
            bd.Rodzaj = tbType.Text;

            //Daty
            bd.DataZakupu = tbPurchaseDate.Text;
            bd.DataPrzyjecia = tbStartDate.Text;
            bd.DataZbycia = tbEndDate.Text;
            bd.DataRozpoczeciaAmortyzacji = tbDepreciationStartDate.Text;
            bd.RokProdukcji = (tbProductionYear.Text != String.Empty) ? Convert.ToInt32(tbProductionYear.Text) : 0;
            bd.DataKoncaGwarancji = tbWarrantyDate.Text;

            //Lokalizacja
            bd.MiejsceUzytkowania = tbAddress.Text;
            bd.Dzial = tbSection.Text;
            bd.NumerPomieszczenia = tbRoomNumber.Text;
            bd.OsobaOdpowiedzialna = tbResponsiblePerson.Text;

            //Amortyzacja
            bd.WartoscPoczatkowa = (tbOriginValue.Text != String.Empty) ? Convert.ToDouble(tbOriginValue.Text) : 0;
            bd.StawkaAmortyzacji = (tbDepreciationRate.Text != String.Empty) ? Convert.ToDouble(tbDepreciationRate.Text) : 0;
            bd.WartoscZamortyzowana = (tbAmortizedValue.Text != String.Empty) ? Convert.ToDouble(tbAmortizedValue.Text) : 0;
            bd.Status = cbStatus.Text;

            //Dodatkowe informacje
            bd.Uwagi = new TextRange(tbDescription.Document.ContentStart, tbDescription.Document.ContentEnd).Text;
            bd.IdentyfikatorModyfikatora = UserID;

            mp.SmObj = new Block(System.DateTime.Now, null, bd);
            mp.Text = "newBlock";
            byte[] data = Blockchain.ObjectToByteArray(mp);
            sendingClient.Send(data, data.Length);
            
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbRegNumber.Focus();
            InitializeSender();
        }

        //Przywracanie domyślnych właściwości pól
        private void RestoreTextboxProperties()
        {
            tbOriginValue.BorderBrush = Brushes.Gray; tbOriginValue.Background = Brushes.White;
            tbDepreciationRate.BorderBrush = Brushes.Gray; tbDepreciationRate.Background = Brushes.White;
            tbRegNumber.BorderBrush = Brushes.Gray; tbRegNumber.Background = Brushes.White;
            tbName.BorderBrush = Brushes.Gray; tbName.Background = Brushes.White;
            tbSymbolKST.BorderBrush = Brushes.Gray; tbSymbolKST.Background = Brushes.White;
            tbType.BorderBrush = Brushes.Gray; tbType.Background = Brushes.White;
            tbPurchaseDate.BorderBrush = Brushes.Gray; tbPurchaseDate.Background = Brushes.White;
            tbStartDate.BorderBrush = Brushes.Gray; tbStartDate.Background = Brushes.White;
            tbDepreciationStartDate.BorderBrush = Brushes.Gray; tbDepreciationStartDate.Background = Brushes.White;
            //cbStatus.BorderBrush = Brushes.Red; cbStatus.Background = Brushes.MistyRose;
        }
    }
}
