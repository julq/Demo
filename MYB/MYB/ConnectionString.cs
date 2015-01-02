using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MYB
{
    class ConnectionString
    {
        private string connection_string = "SERVER=(LOCAL); UID=sa; pwd=1; DATABASE=MYB";
        public string GetCS
        {
            get { return connection_string; }
        }
    }
}
