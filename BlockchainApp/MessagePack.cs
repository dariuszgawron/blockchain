using System;
using System.Collections.Generic;

namespace BlockchainApp
{
    [Serializable]
    public class MessagePack
    {
        public string Text { get; set; }
        public string Hash { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public Blockchain Obj { get; set; }
        public Block SmObj { get; set; }
        public List<Block> listOfBlocks { get; set; }

        public MessagePack(string text = null, string hash = null, string sender = null, string receiver = null,
                            Blockchain obj = null, Block smobj = null)
        {
            Text = text;
            Hash = hash;
            Sender = sender;
            Receiver = receiver;
            Obj = obj;
            SmObj = smobj;
        }
        public MessagePack() { }
    }
}
