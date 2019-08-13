using System;
using System.Text;

namespace BlockchainApp
{
    [Serializable]
    public class Block
    {
        public int Index { get; set; }
        public string TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public BlockchainData Data { get; set; }
        public BlockchainCredentials DataCredentials { get; set; }
        public int Nonce { get; set; } = 0;
        
        public Block(DateTime timeStamp, string previousHash, BlockchainData data)
        {
            Index = 0;
            TimeStamp = timeStamp.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffff");
            PreviousHash = previousHash;
            Data = data;
            Hash = CalculateHash();
        }
        
        public Block(DateTime timeStamp, string previousHash, BlockchainCredentials data)
        {
            Index = 0;
            TimeStamp = timeStamp.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffff");
            PreviousHash = previousHash;
            DataCredentials = data;
            Hash = CalculateHash();
        }
        
        public Block() { }

        public override bool Equals(object obj)
        {
            Block block = obj as Block;
            if (block == null)
                return false;
            else
                return Hash.Equals(block.Hash);
        }

        public string CalculateHash()
        {
            string input = TimeStamp + PreviousHash + Nonce;
            if(Data!=null)
                foreach (var prop in Data.GetType().GetProperties())
                    input += prop.GetValue(Data);
            else
                foreach (var prop in DataCredentials.GetType().GetProperties())
                    input += prop.GetValue(DataCredentials);

            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
                hash.Append(theByte.ToString("x2"));
            return hash.ToString();
        }

        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }
}
