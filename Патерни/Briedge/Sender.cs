using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briedge
{
    public abstract class Sender
    {
        protected IDataReader _reader;
        public Sender(IDataReader reader)    { _reader = reader; }
        public void SetDatareader(IDataReader reader) { _reader = reader;}
        public abstract void Send();

    }
}
