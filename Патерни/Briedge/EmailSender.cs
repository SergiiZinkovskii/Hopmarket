using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Briedge
{
    public class EmailSender : Sender
    {
        public EmailSender(IDataReader reader) : base(reader)
        {
        }
        public override void Send()
        {
            _reader.Read();
            Console.WriteLine("Send to Email");
         }


    }

    
       
    
}
