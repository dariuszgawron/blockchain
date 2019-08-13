using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Xml.Serialization;

namespace PeerToPeerChat
{
    public partial class ChatForm : Form
    {
        delegate void AddMessage(byte[] message);
        string userName;
        const int port = 54545;
        const string broadcastAddress = "25.255.255.255";
        UdpClient receivingClient;
        UdpClient sendingClient;
        Thread receivingThread;

        Blockchain chain;
        MessagePack mp;
        //Object obj = new Object();                
        public List<string> peerNames { get; set; }

        public ChatForm()
        {
            InitializeComponent();
            chain = new Blockchain();
            peerNames = new List<string>();
            this.Load += new EventHandler(ChatForm_Load);
            btnSend.Click += new EventHandler(btnSend_Click);
        }

        void ChatForm_Load(object sender, EventArgs e)
        {
            this.Hide();
            using (LoginForm loginForm = new LoginForm())
            {
                loginForm.ShowDialog();

                if (loginForm.UserName == "")
                    this.Close();
                else
                {
                    userName = loginForm.UserName;
                    this.Show();
                }
            }

            tbSend.Focus();

            InitializeSender();
            InitializeReceiver();

            
            chain = Blockchain.DeserializeJsonToBlockchain();

            foreach (Block el in chain.Chain)
            {
                rtbChat.Text += el.Data + "\n";
            }

            MessagePack mp = new MessagePack();
            mp.Text = userName + ";request";
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

        void btnSend_Click(object sender, EventArgs e)
        {
            tbSend.Text = tbSend.Text.TrimEnd();

            if (!string.IsNullOrEmpty(tbSend.Text))
            {
                MessagePack mp = new MessagePack();
                mp.SmObj = new Block(System.DateTime.Now, null, tbSend.Text);
                byte[] data = Blockchain.ObjectToByteArray(mp);
                sendingClient.Send(data, data.Length);
                tbSend.Text = "";
            }
            tbSend.Focus();
        }

        private void Receiver()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            AddMessage messageDelegate = MessageReceived;

            while (true)
            {
                byte[] message = receivingClient.Receive(ref endPoint);
                Invoke(messageDelegate, message);
            }
        }

        private void MessageReceived(byte[] message)
        {
            MessagePack mp = (Blockchain.ByteArrayToObject(message)) as MessagePack;
            if (mp.Text != null)
            {
                string[] tokens = mp.Text.Split(new string[] { @";" }, StringSplitOptions.None);

                string sender = (tokens[0] == userName) ? "me" : "other";
                string function = tokens[1];

                switch (function)
                {
                    case "request":
                        if (sender == "me")
                        {
                            string hash = chain.Chain[chain.Chain.Count() - 1].Hash;
                            mp.Text = "Nobody" + ";requestUpdate;" + hash + ";" + userName;
                            //mp.Val.Add(hash);
                            //mp.Val.Add(length.ToString());
                            byte[] data = Blockchain.ObjectToByteArray(mp);
                            sendingClient.Send(data, data.Length);
                        }
                        break;

                    case "requestUpdate":
                        if (sender == "other")
                        {
                            string hash = chain.Chain[chain.Chain.Count() - 1].Hash;
                            if (hash != tokens[2])
                            {
                                mp.Text = userName + ";peerReady;" + tokens[3] + ";" + hash;
                                byte[] data = Blockchain.ObjectToByteArray(mp);
                                sendingClient.Send(data, data.Length);

                            }
                        }
                        break;
                    case "peerReady":
                        peerNames.Add(tokens[0]);
                        if (sender == "other" && tokens[2] == userName && peerNames[0] == tokens[0])
                        {
                            int length = chain.Chain.Count();
                            string hash = chain.Chain[chain.Chain.Count() - 1].Hash;
                            if (hash != tokens[3] && length != 1)
                            {
                                mp.Text = userName + ";chainRequest;" + tokens[0] + ";" + hash;
                                byte[] data = Blockchain.ObjectToByteArray(mp);
                                sendingClient.Send(data, data.Length);
                            }
                            else if (hash == tokens[3])
                            {
                                MessageBox.Show("Czy padany łańcuch jest poprawny: " + chain.IsValid().ToString());
                                if (chain.IsValid() == false || chain.Chain.Count() == 0)
                                {
                                    chain = new Blockchain();
                                    chain.AddGenesisBlock();
                                    hash = chain.Chain[chain.Chain.Count() - 1].Hash;
                                    mp.Text = "Nobody" + ";requestUpdate;" + hash + ";" + userName;
                                    //mp.Val.Add(hash);
                                    //mp.Val.Add(length.ToString());
                                    byte[] data = Blockchain.ObjectToByteArray(mp);
                                    sendingClient.Send(data, data.Length);
                                }
                            }
                            else
                            {
                                rtbChat.Text = null;
                                chain = new Blockchain();
                                mp.Text = userName + ";chainRequest;" + tokens[0] + ";" + hash;
                                byte[] data = Blockchain.ObjectToByteArray(mp);
                                sendingClient.Send(data, data.Length);
                            }


                        }
                        break;
                    case "chainRequest":
                        if (sender == "other" && tokens[2] == userName)
                        {
                            if (chain.Chain.Find(i => i.Hash == tokens[3]) != null)
                            {
                                Block lastBlock = chain.Chain.Find(i => i.Hash == tokens[3]);
                                int startIndex = lastBlock.Index + 1;
                                int finishIndex = lastBlock.Index + 10;
                                int lastIndex = chain.Chain[chain.Chain.Count() - 1].Index;
                                List<Block> newBlocks = new List<Block>();

                                if ((lastIndex - lastBlock.Index) < 10)
                                {
                                    finishIndex = lastIndex + 1;
                                }

                                for (int i = startIndex; i < finishIndex; i++)
                                {
                                    newBlocks.Add(chain.Chain[i]);
                                }
                                mp.Text = userName + ";newBlockchain;" + tokens[0];
                                mp.listOfBlocks = newBlocks;
                                byte[] data = Blockchain.ObjectToByteArray(mp);
                                sendingClient.Send(data, data.Length);
                            }
                            else
                            {
                                List<Block> newBlocks = new List<Block>();

                                for (int i = 0; i < 10; i++)
                                {
                                    newBlocks.Add(chain.Chain[i]);
                                }
                                mp.Text = userName + ";newBlockchain;" + tokens[0];
                                mp.listOfBlocks = newBlocks;
                                byte[] data = Blockchain.ObjectToByteArray(mp);
                                sendingClient.Send(data, data.Length);

                            }

                        }
                        break;
                    case "newBlockchain":
                        if (sender == "other" && tokens[2] == userName)
                        {
                            foreach (Block el in mp.listOfBlocks)
                            {
                                chain.Chain.Add(el);
                                rtbChat.Text += el.Data + "\n";
                            }
                            string hash = chain.Chain[chain.Chain.Count() - 1].Hash;
                            mp.Text = "Nobody" + ";requestUpdate;" + hash + ";" + userName;
                            //mp.Val.Add(hash);
                            //mp.Val.Add(length.ToString());

                            byte[] data = Blockchain.ObjectToByteArray(mp);
                            sendingClient.Send(data, data.Length);
                            Blockchain.SerializeToJson(chain);
                            peerNames.Clear();
                        }
                        break;
                }
            }
            else
            {
                chain.AddBlock(mp.SmObj);
                rtbChat.Text += chain.Chain[chain.Chain.Count() - 1].Data + "\n";
                Blockchain.SerializeToJson(chain);
            }
        }

        private void rtbChat_TextChanged(object sender, EventArgs e)
        {
            if (rtbChat.Visible)
            {
                rtbChat.SelectionStart = rtbChat.TextLength;
                rtbChat.ScrollToCaret();
            }
        }

        private void tbSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
            }
        }

        private void rtbChat_VisibleChanged(object sender, EventArgs e)
        {
            if (rtbChat.Visible)
            {
                rtbChat.SelectionStart = rtbChat.TextLength;
                rtbChat.ScrollToCaret();
            }
        }
    }
}
