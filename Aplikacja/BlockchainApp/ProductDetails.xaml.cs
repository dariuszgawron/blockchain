using System;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace BlockchainApp
{
    /// <summary>
    /// Logika interakcji dla klasy ProductDetails.xaml
    /// </summary>
    public partial class ProductDetails : Window
    {
        delegate void AddMessage(byte[] message);

        const int port = 54545;
        const string broadcastAddress = "25.255.255.255";
        public UdpClient receivingClient;
        public UdpClient sendingClient;

        private string userID;
        
        Connection con;
        public ProductDetails()
        {
            InitializeComponent();
            InitializeSender();
        }
        private void InitializeSender()
        {
            sendingClient = new UdpClient(broadcastAddress, port);
            sendingClient.EnableBroadcast = true;
        }
        public ProductDetails(Connection mainWin, Blockchain chain, string userName)
        {
            InitializeComponent();
            InitializeSender();

            userID = userName;

            con = mainWin;
            var item = con.lstProducts.SelectedItem as Block;
            if (item != null)
            {
                //Dane podstawowe
                tbRegNumber.Text = item.Data.NumerInwentaryzacyjny.ToString();
                tbName.Text = item.Data.Nazwa.ToString();
                tbSerialNumber.Text = item.Data.NumerFabryczny.ToString();
                tbModel.Text = item.Data.Model.ToString();
                tbProducer.Text = item.Data.Producent.ToString();
                tbSymbolKST.Text = item.Data.SymbolKST.ToString();
                tbType.Text = item.Data.Rodzaj.ToString();

                //Daty
                tbPurchaseDate.Text = item.Data.DataZakupu.ToString();
                tbStartDate.Text = item.Data.DataPrzyjecia.ToString();
                tbEndDate.Text = item.Data.DataZbycia.ToString();
                tbDepreciationStartDate.Text = item.Data.DataRozpoczeciaAmortyzacji.ToString();
                tbProductionYear.Text = item.Data.RokProdukcji.ToString();
                tbWarrantyDate.Text = item.Data.DataKoncaGwarancji.ToString();

                //Lokalizacja
                tbAddress.Text = item.Data.MiejsceUzytkowania.ToString();
                tbSection.Text = item.Data.Dzial.ToString();
                tbRoomNumber.Text = item.Data.NumerPomieszczenia.ToString();
                tbResponsiblePerson.Text = item.Data.OsobaOdpowiedzialna.ToString();

                //Amortyzacja
                tbOriginValue.Text = item.Data.WartoscPoczatkowa.ToString();
                tbDepreciationRate.Text = item.Data.StawkaAmortyzacji.ToString();
                tbAmortizedValue.Text = item.Data.WartoscZamortyzowana.ToString();
                cbStatus.Text = item.Data.Status.ToString();

                //Inne
                FlowDocument text = new FlowDocument();
                text.Blocks.Add(new Paragraph(new Run(item.Data.Uwagi.ToString())));
                tbDescription.Document = text;
                
                //gridProductForm.DataContext = item;
            }

            if (item.Data.IdentyfikatorModyfikatora.ToString() != userID)
            {
                lbPermissionsWarning.Visibility = Visibility.Visible;
                tbEndDate.IsEnabled = false;
                tbProductionYear.IsEnabled = false;
                tbWarrantyDate.IsEnabled = false;
                tbAddress.IsEnabled = false;
                tbSection.IsEnabled = false;
                tbRoomNumber.IsEnabled = false;
                tbResponsiblePerson.IsEnabled = false;
                tbDepreciationRate.IsEnabled = false;
                cbStatus.IsEnabled = false;
                tbDescription.IsEnabled = false;
                btnConfirmChanges.IsEnabled = false;
            } 

            //Załadowanie historii związanych z danym środkiem
            lstProductLink.ItemsSource = from p in chain.Chain
                                         where p.Data.NumerInwentaryzacyjny == item.Data.NumerInwentaryzacyjny
                                         select p;
            
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lstProductLink.ItemsSource);
            view.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(item.TimeStamp), System.ComponentModel.ListSortDirection.Descending));

            CountCurrentDepreciationValue(item);
        }

        private void BtnConfirmChanges_Click(object sender, RoutedEventArgs e)
        {
            Regex DepreciationRate = new Regex(@"^(100|[0-9]{1,2})$");

            if (tbDepreciationRate.Text == String.Empty)
            {
                MessageBox.Show("Formularz nie jest w pełni wypełniony. Proszę uzupełnić wymagane pola!", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbEndDate.BorderBrush = Brushes.LightSlateGray; tbStartDate.Background = Brushes.White;
                if (tbDepreciationRate.Text == String.Empty) { tbDepreciationRate.BorderBrush = Brushes.Red; tbDepreciationRate.Background = Brushes.MistyRose; }
                else { tbDepreciationRate.BorderBrush = Brushes.LightSlateGray; tbDepreciationRate.Background = Brushes.White; }
            }
            else if (tbEndDate.Text != String.Empty && Convert.ToDateTime(tbStartDate.Text) > Convert.ToDateTime(tbEndDate.Text))
            {
                MessageBox.Show("Data zakończenia użytkowania nie może być wcześniejsza niż data rozpoczęcia użytkowania", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbDepreciationRate.BorderBrush = Brushes.LightSlateGray; tbDepreciationRate.Background = Brushes.White;
                tbEndDate.BorderBrush = Brushes.Red; tbStartDate.Background = Brushes.MistyRose;
            }
            else if (!DepreciationRate.IsMatch(tbDepreciationRate.Text))
            {
                MessageBox.Show("Pole stawka amortyzacji ma niepoprawny format! Wartość powinna zawierać się w przedziale 0-100", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbEndDate.BorderBrush = Brushes.LightSlateGray; tbStartDate.Background = Brushes.White;
                tbDepreciationRate.BorderBrush = Brushes.Red; tbDepreciationRate.Background = Brushes.MistyRose;
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
                
            bd.IdentyfikatorModyfikatora = userID;

            mp.SmObj = new Block(System.DateTime.Now, null, bd);
            mp.Text = "newBlock";
            byte[] data = Blockchain.ObjectToByteArray(mp);
            sendingClient.Send(data, data.Length);
            
            this.Close();
        }
        

        //Obliczanie aktualnej wartości zamortyzowanej
        private void CountCurrentDepreciationValue(Block item)
        {
            //https://stackoverflow.com/questions/4638993/difference-in-months-between-two-dates
            if (item.Data.Status == "Amortyzowany")
            {
                int months = ((DateTime.Now.Year - Convert.ToDateTime(item.Data.DataRozpoczeciaAmortyzacji).Year) * 12) + DateTime.Now.Month - Convert.ToDateTime(item.Data.DataRozpoczeciaAmortyzacji).Month;
                double value = Convert.ToDouble(item.Data.WartoscPoczatkowa) * (Convert.ToDouble(item.Data.StawkaAmortyzacji) / 100);
                double valuePerMonth = value / 12;
                double zamort = Math.Round(valuePerMonth * months, 2);
                if (zamort <= 0)
                    tbAmortizedValue.Text = "0";
                else if (zamort <= item.Data.WartoscPoczatkowa)
                    tbAmortizedValue.Text = zamort.ToString();
                else
                    tbAmortizedValue.Text = item.Data.WartoscPoczatkowa.ToString();
            }
            else
                tbAmortizedValue.Text = item.Data.WartoscZamortyzowana.ToString();
        }
    }
}
