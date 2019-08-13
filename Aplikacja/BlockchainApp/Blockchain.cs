using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace BlockchainApp
{
    [Serializable]
    public class Blockchain
    {
        public List<Block> Chain { set; get; }
        public int Difficulty { set; get; } = 2;

        public Blockchain()
        {
            InitializeChain();
        }
        
        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            BlockchainData bd = new BlockchainData();
            return new Block(DateTime.Now, null, bd);
        }

        public Block CreateCredentialsGenesisBlock()
        {
            BlockchainCredentials bc = new BlockchainCredentials();
            return new Block(DateTime.Now, null, bc);
        }

        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public void AddCredentialsGenesisBlock()
        {
            Chain.Add(CreateCredentialsGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Hash = block.CalculateHash();
            block.Mine(this.Difficulty);
            Chain.Add(block);
        }
        
        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];
                if (currentBlock.Hash != currentBlock.CalculateHash())
                    return false;

                if (currentBlock.PreviousHash != previousBlock.Hash)
                    return false;
            }
            return true;
        }
        
        public static void SerializeToJson(Blockchain blockchainObj)
        {
            string serial = JsonConvert.SerializeObject(blockchainObj, Newtonsoft.Json.Formatting.Indented);
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\chain.dat";

            if (File.Exists(path))
                File.WriteAllText(path, serial);
            else
            {
                StreamWriter sw = File.CreateText(path);
                sw.Write(serial);
                sw.Close();               
            }
        }

        public static Blockchain DeserializeJsonToBlockchain()
        {
            string blockchain;
            Blockchain chain = new Blockchain();
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\chain.dat";

            if (File.Exists(path) && new FileInfo(path).Length != 0)
            {
                blockchain = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\chain.dat");
                chain = new JavaScriptSerializer().Deserialize<Blockchain>(blockchain);
                
                if (chain.IsValid() == false || chain.Chain.Count() == 0)
                {
                    chain = new Blockchain();
                    chain.AddGenesisBlock();
                }
            }
            else
                chain.AddGenesisBlock();

            return chain;
        }
        
        public static void SerializeToJsonCredentials(Blockchain blockchainObj)
        {
            string serial = JsonConvert.SerializeObject(blockchainObj, Newtonsoft.Json.Formatting.Indented);
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\credentials.dat";

            if (File.Exists(path)) {
                File.WriteAllText(path, String.Empty);
                File.WriteAllText(path, serial);
            } else {
                StreamWriter sw = File.CreateText(path);
                sw.Write(serial);
                sw.Close();
            }
        }

        public static Blockchain DeserializeJsonCredentialsToBlockchain()
        {
            string blockchain;
            Blockchain credentials = new Blockchain();
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\credentials.dat";

            if (File.Exists(path) && new FileInfo(path).Length != 0) {
                blockchain = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\credentials.dat");
                credentials = new JavaScriptSerializer().Deserialize<Blockchain>(blockchain);
                
                if (credentials.IsValid() == false || credentials.Chain.Count() == 0) {
                    credentials = new Blockchain();
                    credentials.AddCredentialsGenesisBlock();
                }
            } else {
                credentials.AddCredentialsGenesisBlock();
            }
            return credentials;
        }

        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}
