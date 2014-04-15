using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinCharts.Net {
    internal class Message {
        public byte[] Buffer;
        public Socket Socket;
        public int Count;        
    }
}
