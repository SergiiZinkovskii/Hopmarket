using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briedge
{
    public class DataBaseReader : IDataReader
    {
        public void Read()
        {
            Console.WriteLine("Read dats from database");
        }
    }
}
