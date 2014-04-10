using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nosc.Repository
{
    public class UNID
    {
        public long getNewUNID()
        {
            long Unid = long.Parse(String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", DateTime.Now).Replace(":", ""));
            return Unid;
        }
    }
}
