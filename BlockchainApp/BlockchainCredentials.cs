using System;

namespace BlockchainApp
{
    [Serializable]
    public class BlockchainCredentials
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public BlockchainCredentials(string Login, string Password)
        {
            this.Login = Login;
            this.Password = Password;
        }

        public BlockchainCredentials() { }
    }
}
