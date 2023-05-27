using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briedge
{
    public class TelegramSender : Sender
    {
        public TelegramSender(IDataReader reader) : base(reader)
        {
        }

        public override void Send()
        {
           _reader.Read();
            Console.WriteLine("Send to Telegram");
        }
    }
}
