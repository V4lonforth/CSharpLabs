using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab4;

namespace Lab4Server
{
    class Connection : IConnection
    {
        public static int id = 0;
        public static bool connected;

        public int Connect()
        {
            connected = true;
            return ++id;
        }
    }
}
