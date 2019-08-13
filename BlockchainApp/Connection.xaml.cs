using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace BlockchainApp
{
    /// <summary>
    /// Logika interakcji dla klasy Connection.xaml
    /// </summary>
    public partial class Connection : Window
    {
        public Connection(string user, Blockchain credentials)
        {
            InitializeComponent();
            chain = new Blockchain();
            peerNames = new List<string>();
            userName = user;
            this.credentials = credentials;
        }

        delegate void AddMessage(byte[] message);
        public string userName;
        const int port = 54545;
        const string broadcastAddress = "25.255.255.255";
        public UdpClient receivingClient;
        public UdpClient sendingClient;
        Thread receivingThread;

        Blockchain chain;
        Blockchain credentials;
        MessagePack mp;
        //Object obj = new Object();                
        public List<string> peerNames { get; set; }
        
        #region FILTROWANIE DANYCH
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(tbEvidenceNumber.Text) && String.IsNullOrEmpty(tbName.Text) && String.IsNullOrEmpty(tbSerialNumber.Text) && String.IsNullOrEmpty(tbProducer.Text)
                && String.IsNullOrEmpty(tbModel.Text) && String.IsNullOrEmpty(tbSymbolKST.Text) && String.IsNullOrEmpty(tbType.Text) 
                
                && String.IsNullOrEmpty(tbAcceptanceDate.Text) && String.IsNullOrEmpty(tbStartDate.Text) && String.IsNullOrEmpty(tbEndDate.Text) && String.IsNullOrEmpty(tbDepreciationStartDate.Text)
                && String.IsNullOrEmpty(tbProductionYear.Text) && String.IsNullOrEmpty(tbWarrantyDate.Text) && String.IsNullOrEmpty(tbModifyDate.Text) 

                && String.IsNullOrEmpty(tbAddress.Text) && String.IsNullOrEmpty(tbSection.Text) && String.IsNullOrEmpty(tbRoomNumber.Text) && String.IsNullOrEmpty(tbResponsiblePerson.Text)

                && String.IsNullOrEmpty(tbOriginValueFrom.Text) && String.IsNullOrEmpty(tbOriginValueTo.Text) && String.IsNullOrEmpty(tbDepreciationRate.Text) && String.IsNullOrEmpty(cbStatus.Text) 
                )
                return true;
            return (((item as Block).Data.NumerInwentaryzacyjny ?? String.Empty).ToString().IndexOf(tbEvidenceNumber.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.Nazwa ?? String.Empty).ToString().IndexOf(tbName.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.NumerFabryczny ?? String.Empty).ToString().IndexOf(tbSerialNumber.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.Producent ?? String.Empty).ToString().IndexOf(tbProducer.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.Model ?? String.Empty).ToString().IndexOf(tbModel.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.SymbolKST ?? String.Empty).ToString().IndexOf(tbSymbolKST.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.Rodzaj ?? String.Empty).ToString().IndexOf(tbType.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                
                && (((item as Block).Data.DataZakupu ?? String.Empty).ToString().IndexOf(tbAcceptanceDate.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.DataPrzyjecia ?? String.Empty).ToString().IndexOf(tbStartDate.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.DataZbycia ?? String.Empty).ToString().IndexOf(tbEndDate.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.DataRozpoczeciaAmortyzacji ?? String.Empty).ToString().IndexOf(tbDepreciationStartDate.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.RokProdukcji.ToString() ?? String.Empty).ToString().IndexOf(tbProductionYear.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.DataKoncaGwarancji ?? String.Empty).ToString().IndexOf(tbWarrantyDate.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).TimeStamp ?? String.Empty).ToString().IndexOf(tbModifyDate.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                
                && (((item as Block).Data.MiejsceUzytkowania ?? String.Empty).ToString().IndexOf(tbAddress.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.Dzial ?? String.Empty).ToString().IndexOf(tbSection.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.NumerPomieszczenia ?? String.Empty).ToString().IndexOf(tbRoomNumber.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.OsobaOdpowiedzialna ?? String.Empty).ToString().IndexOf(tbResponsiblePerson.Text, StringComparison.OrdinalIgnoreCase) >= 0)  //zmieniałem

                && (((item as Block).Data.WartoscPoczatkowa) >= Convert.ToDouble(string.IsNullOrEmpty(tbOriginValueFrom.Text) ? "0,0" : tbOriginValueFrom.Text))
                && (((item as Block).Data.WartoscPoczatkowa) <= Convert.ToDouble(string.IsNullOrEmpty(tbOriginValueTo.Text) ? "999999999999,99" : tbOriginValueTo.Text))
                && (((item as Block).Data.StawkaAmortyzacji.ToString() ?? String.Empty).ToString().IndexOf(tbDepreciationRate.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                && (((item as Block).Data.Status ?? String.Empty).ToString().IndexOf(cbStatus.Text, StringComparison.Ordinal) >= 0);
        }
        #endregion 
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeSender();
            InitializeReceiver();

            chain = Blockchain.DeserializeJsonToBlockchain();
            
            for (int i = 0; i < chain.Chain.Count(); i++)
                chain.Chain[i].Data.WartoscZamortyzowana = CountCurrentDepreciationValue(chain.Chain[i]);
            
            var temp = from p in chain.Chain
                       select p;
            lstProducts.ItemsSource = temp.GroupBy(x => x.Data.NumerInwentaryzacyjny).Select(y => y.Last());
            
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lstProducts.ItemsSource);
            view.Filter = UserFilter;

            tbUserName.Text = userName;
            
            MessagePack mp = new MessagePack();
            mp.Text = "requestUpdate";
            mp.Sender = userName;
            mp.Receiver = userName;
            mp.Hash = chain.Chain[chain.Chain.Count() - 1].Hash;
            byte[] data = Blockchain.ObjectToByteArray(mp);
            sendingClient.Send(data, data.Length);
        }

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
                case "credentialsRequest":
                    if (mp.Sender != userName)
                    {
                        string hash = credentials.Chain[credentials.Chain.Count() - 1].Hash;
                        if (hash != mp.Hash)
                        {
                            List<Block> newBlocks = new List<Block>();
                            foreach (Block el in credentials.Chain)
                                newBlocks.Add(el);

                            mp.listOfBlocks = newBlocks;
                        }
                        else
                            mp.listOfBlocks = null;
                        
                        mp.Text = "userCredentialsData";
                        mp.Receiver = mp.Sender;
                        mp.Sender = userName;
                        byte[] data = Blockchain.ObjectToByteArray(mp);
                        sendingClient.Send(data, data.Length);
                    }
                    break;

                //SYNCHRONIZACJA DANYCH DOTYCZĄCYCH ZASOBÓW
                case "requestUpdate":
                    if (mp.Sender != userName)
                    {
                        mp.Text = "peerReady";
                        mp.Receiver = mp.Sender;
                        mp.Sender = userName;
                        mp.Hash = chain.Chain[chain.Chain.Count() - 1].Hash;
                        byte[] data = Blockchain.ObjectToByteArray(mp);
                        sendingClient.Send(data, data.Length);
                    }
                    break;
                case "peerReady":
                    peerNames.Add(mp.Sender);
                    if (mp.Sender != userName && mp.Receiver == userName && peerNames[0] == mp.Sender)
                    {
                        int length = chain.Chain.Count();
                        string hash = chain.Chain[chain.Chain.Count() - 1].Hash;
                        if (hash != mp.Hash && length != 1)
                        {
                            mp.Text = "chainRequest";
                            mp.Receiver = mp.Sender;
                            mp.Sender = userName;
                            mp.Hash = hash;
                            byte[] data = Blockchain.ObjectToByteArray(mp);
                            sendingClient.Send(data, data.Length);
                        }
                        else if (hash == mp.Hash)
                        {
                            if (chain.IsValid() == false || chain.Chain.Count() == 0)
                            {
                                chain = new Blockchain();
                                chain.AddGenesisBlock();
                                mp.Text = "requestUpdate";
                                mp.Sender = userName;
                                mp.Receiver = userName;
                                mp.Hash = chain.Chain[chain.Chain.Count() - 1].Hash;
                                byte[] data = Blockchain.ObjectToByteArray(mp);
                                sendingClient.Send(data, data.Length);
                            }
                            else
                                MessageBox.Show("Synchronizacja zakończona pomyślnie. Posiadasz aktualną kopię blockchaina!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            chain = new Blockchain();
                            mp.Text = "chainRequest";
                            mp.Receiver = mp.Sender;
                            mp.Sender = userName;
                            mp.Hash = hash;
                            byte[] data = Blockchain.ObjectToByteArray(mp);
                            sendingClient.Send(data, data.Length);
                        }
                    }
                    break;
                case "chainRequest":
                    if (mp.Sender != userName && mp.Receiver == userName)
                    {
                        List<Block> newBlocks = new List<Block>();

                        if (chain.Chain.Find(i => i.Hash == mp.Hash) != null)
                        {
                            Block lastBlock = chain.Chain.Find(i => i.Hash == mp.Hash);
                            int startIndex = lastBlock.Index + 1;
                            int finishIndex = lastBlock.Index + 10;
                            int lastIndex = chain.Chain[chain.Chain.Count() - 1].Index;

                            if ((lastIndex - lastBlock.Index) < 10)
                                finishIndex = lastIndex;

                            for (int i = startIndex; i <= finishIndex; i++)
                                newBlocks.Add(chain.Chain[i]);
                        }
                        else
                        {
                            int lastIndex = chain.Chain[chain.Chain.Count() - 1].Index;
                            int finishIndex = (lastIndex < 9) ? lastIndex : 9;

                            for (int i = 0; i <= finishIndex; i++)
                                newBlocks.Add(chain.Chain[i]);
                        }

                        mp.listOfBlocks = newBlocks;
                        mp.Text = "newBlockchain";
                        mp.Receiver = mp.Sender;
                        mp.Sender = userName;
                        byte[] data = Blockchain.ObjectToByteArray(mp);
                        sendingClient.Send(data, data.Length);
                    }
                    break;
                case "newBlockchain":
                    if (mp.Sender != userName && mp.Receiver == userName)
                    {
                        foreach (Block el in mp.listOfBlocks)
                            chain.Chain.Add(el);
                        
                        mp.Text = "requestUpdate";
                        mp.Sender = userName;
                        mp.Receiver = userName;
                        mp.Hash = chain.Chain[chain.Chain.Count() - 1].Hash;

                        byte[] data = Blockchain.ObjectToByteArray(mp);
                        sendingClient.Send(data, data.Length);
                        Blockchain.SerializeToJson(chain);
                        peerNames.Clear();

                        // Załadowanie danych do listview
                        for (int i = 0; i < chain.Chain.Count(); i++)
                            chain.Chain[i].Data.WartoscZamortyzowana = CountCurrentDepreciationValue(chain.Chain[i]);

                        var temp = from p in chain.Chain
                                   select p;
                        lstProducts.ItemsSource = temp.GroupBy(x => x.Data.NumerInwentaryzacyjny).Select(y => y.Last());

                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lstProducts.ItemsSource);
                        view.Filter = UserFilter;
                    }
                    break;

                //DODAWANIE NOWEGO UŻYTKOWNIKA DO SYSTEMU
                case "createNewUserRequest":
                    MessageBoxResult decision = MessageBox.Show("Czy zgadzasz się na dodanie użytkownika o loginie '" + mp.SmObj.DataCredentials.Login.ToString() + "' do systemu",
                                "Akceptacja nowego użytkownika", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (decision != MessageBoxResult.Yes)
                        mp.SmObj = null;

                    mp.Text = "createNewUserDecision";
                    byte[] accountData = Blockchain.ObjectToByteArray(mp);
                    sendingClient.Send(accountData, accountData.Length);
                    break;
                case "createNewUserAccepted":
                    credentials.AddBlock(mp.SmObj);
                    Blockchain.SerializeToJsonCredentials(credentials);
                    break;

                //DODAWANIE NOWEGO ZASOBU DO SYSTEMU
                case "newBlock":
                    chain.AddBlock(mp.SmObj);
                    chain.Chain[chain.Chain.Count() - 1].Data.WartoscZamortyzowana = CountCurrentDepreciationValue(chain.Chain[chain.Chain.Count() - 1]);
                    CollectionViewSource.GetDefaultView(lstProducts.ItemsSource).Refresh();
                    MessageBox.Show("Nowy rekord został dodany do systemu!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                    Blockchain.SerializeToJson(chain);
                    break;
            }
        }
       
        private void BtnAdd_Click(object sender, RoutedEventArgs e) 
        {
            var chainUniqID = from nrInw in chain.Chain
                                select nrInw.Data.NumerInwentaryzacyjny;
            
            AddProduct a = new AddProduct(chainUniqID, userName);
            a.Show();
        }

        private void BtnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (lstProducts.SelectedItem != null && lstProducts.SelectedIndex != 0) {
                ProductDetails okno2 = new ProductDetails(this,chain,userName);
                okno2.Show();
            }
            else 
                MessageBox.Show("Aby wyświetlić szczegóły należy zaznaczyć środek!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnFiltr_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lstProducts.ItemsSource).Refresh();
            if (lstProducts.Items.Count == 0)
                MessageBox.Show("Brak rekordów spełniających podane kryteria!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            //Ogólne
            tbEvidenceNumber.Clear();
            tbName.Clear();
            tbSerialNumber.Clear();
            tbModel.Clear();
            tbProducer.Clear();
            tbSymbolKST.Clear();
            tbType.SelectedItem = null;

            //Daty
            tbAcceptanceDate.SelectedDate = null;
            tbStartDate.SelectedDate = null;
            tbEndDate.SelectedDate = null;
            tbDepreciationStartDate.SelectedDate = null;
            tbProductionYear.Clear();
            tbWarrantyDate.SelectedDate = null;
            tbModifyDate.Clear();

            //Lokalizacja
            tbAddress.Clear();
            tbSection.Clear();
            tbRoomNumber.Clear();
            tbResponsiblePerson.Clear();

            //Amortyzacja
            tbOriginValueFrom.Clear();
            tbOriginValueTo.Clear();
            tbDepreciationRate.Clear();
            cbStatus.SelectedItem = null;

            CollectionViewSource.GetDefaultView(lstProducts.ItemsSource).Refresh();
        }

        //Eksport danych do pliku Excel
        private void BtnExcelExport_Click(object sender, RoutedEventArgs e)
        {
            //Źródło: https://www.codebyamir.com/blog/create-excel-files-in-c-sharp
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Zestawienie");

                var headerRow = new List<string[]>()
                {
                    new string[] 
                    {
                        "Numer inwentaryzacyjny", "Nazwa środka", "Numer fabryczny", "Producent", "Model", "Symbol KŚT", "Rodzaj środka",
                        "Data zakupu", "Data przyjęcia", "Data zbycia", "Data rozpoczecia amortyzacji", "Rok produkcji", "Data konca gwarancji",
                        "Miejsce uzytkowania", "Dział", "Numer pomieszczenia", "Osoba odpowiedzialna",
                        "Wartosc początkowa", "Stawka amortyzacji", "Status", "Wartość zamortyzowana"
                    }
                };
                
                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
                
                var worksheet = excel.Workbook.Worksheets["Zestawienie"];
                worksheet.Cells[headerRange].LoadFromArrays(headerRow);
                
                var cellData = new List<object[]>();
                foreach (Block row in lstProducts.Items)
                {
                    cellData.Add(new object[] {
                        row.Data.NumerInwentaryzacyjny, row.Data.Nazwa, row.Data.NumerFabryczny, row.Data.Producent, row.Data.Model, row.Data.SymbolKST, row.Data.Rodzaj,
                        row.Data.DataZakupu, row.Data.DataPrzyjecia, row.Data.DataZbycia, row.Data.DataRozpoczeciaAmortyzacji, row.Data.RokProdukcji, row.Data.DataKoncaGwarancji,
                        row.Data.MiejsceUzytkowania, row.Data.Dzial, row.Data.NumerPomieszczenia, row.Data.OsobaOdpowiedzialna,
                        row.Data.WartoscPoczatkowa, row.Data.StawkaAmortyzacji, row.Data.Status, CountCurrentDepreciationValue(row)
                    });
                }

                worksheet.Cells[2, 1].LoadFromArrays(cellData);
                FileInfo excelFile = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Zestawienie_" + DateTime.Now.ToString("yyyy'_'MM'_'dd") + ".xlsx");
                excel.SaveAs(excelFile);

                MessageBox.Show("Dane zostały pomyślnie wyeksportowane do pliku o nazwie 'Zestawienie_" + DateTime.Now.ToString("yyyy'_'MM'_'dd") + ".xlsx' " +
                    "znajdującym się w folderze 'Moje dokumenty'!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        
        //Obliczanie aktualnej wartości zamortyzowanej
        private double CountCurrentDepreciationValue(Block item)
        {
            // Źródło: https://stackoverflow.com/questions/4638993/difference-in-months-between-two-dates
            if (item.Data.Status == "Amortyzowany" && item.Data.DataRozpoczeciaAmortyzacji != "")
            {
                int months = ((DateTime.Now.Year - Convert.ToDateTime(item.Data.DataRozpoczeciaAmortyzacji).Year) * 12) + DateTime.Now.Month - Convert.ToDateTime(item.Data.DataRozpoczeciaAmortyzacji).Month;
                double value = Convert.ToDouble(item.Data.WartoscPoczatkowa) * (Convert.ToDouble(item.Data.StawkaAmortyzacji) / 100);
                double valuePerMonth = value / 12;
                double zamort = Math.Round(valuePerMonth * months, 2);
                if (zamort <= 0)
                    return 0;
                if (zamort <= item.Data.WartoscPoczatkowa)
                    return zamort;
                return item.Data.WartoscPoczatkowa;
            }
            else
                return item.Data.WartoscZamortyzowana;
        }

        private void BtnExpanderOn_Click(object sender, RoutedEventArgs e)
        {
            btnExpanderOn.Visibility = Visibility.Hidden;
            btnExpanderOff.Visibility = Visibility.Visible;

            lstProducts.ItemsSource = chain.Chain;
           
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lstProducts.ItemsSource);
            view.Filter = UserFilter;
        }

        private void BtnExpanderOff_Click(object sender, RoutedEventArgs e)
        {
            btnExpanderOn.Visibility = Visibility.Visible;
            btnExpanderOff.Visibility = Visibility.Hidden;
            
            var temp = from p in chain.Chain
                       select p;
            lstProducts.ItemsSource = temp.GroupBy(x => x.Data.NumerInwentaryzacyjny).Select(y => y.Last());

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lstProducts.ItemsSource);
            view.Filter = UserFilter;
        }

        private void BtnLogOut_MouseDown(object sender, MouseEventArgs e)
        {
            Window o = new LoginForm();
            
            receivingThread.Abort();
            receivingClient.Close();
            receivingClient = null;
            MessageBox.Show("Zostałeś pomyślnie wylogowany", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
            o.Show();
        }

        private void BtnLogOut_MouseEnter(object sender, MouseEventArgs e)
        {
            btnLogOut.Foreground = Brushes.SteelBlue;
            btnLogOut.Cursor = Cursors.Hand;
        }

        private void BtnLogOut_MouseLeave(object sender, MouseEventArgs e)
        {
            btnLogOut.Foreground = Brushes.DarkRed;
        }
        
        ////////
        // Źródło: https://docs.microsoft.com/pl-pl/dotnet/framework/wpf/controls/how-to-sort-a-gridview-column-when-a-header-is-clicked
        GridViewColumnHeader lastColumn = null, 
                             selectedColumn;
        ListSortDirection lastDir = ListSortDirection.Ascending, 
                          dir;

        private void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            selectedColumn = (GridViewColumnHeader)e.OriginalSource;

            if (selectedColumn != null && selectedColumn.Role != GridViewColumnHeaderRole.Padding)
            {
                dir = (selectedColumn != lastColumn) ? ListSortDirection.Ascending :
                    ((lastDir == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending);
                    
                Binding columnBinding = (Binding)selectedColumn.Column.DisplayMemberBinding;
                string col = columnBinding?.Path.Path ?? selectedColumn.Column.Header as string;

                Sort(col, dir);
                    
                lastColumn = selectedColumn;
                lastDir = dir;
            }
        }

        private void Sort(string col, ListSortDirection dir)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(lstProducts.ItemsSource);
            view.SortDescriptions.Clear();
            SortDescription details = new SortDescription(col, dir);
            view.SortDescriptions.Add(details);
            view.Refresh();
        }
    }
}
