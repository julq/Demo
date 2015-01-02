using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MYB
{
    class AutoID
    {
        //private string id;

        public string MakeID(string TableName, string ColumnName, string Prefix, int nDigits)
        {
            string id = "";
            try
            {
                string cmd_string = string.Format("select max({0}) from {1}", ColumnName, TableName);

                ConnectionString cs = new ConnectionString();
                string con_string = cs.GetCS;

                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand cmd = new SqlCommand(cmd_string, con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        string max_id = dr.GetString(0);
                        int id_num = int.Parse(max_id.Substring(Prefix.Length, nDigits));
                        id_num++;
                        string f = "";
                        for (int i = 0; i < nDigits; i++)
                        {
                            f += "0";
                        }
                        id = Prefix + id_num.ToString(f);
                    }
                    else
                    {
                        for (int i = 0; i < nDigits - 1; i++)
                        {
                            id += "0";
                        }
                        id = Prefix + id + "1";

                    }
                }


                con.Close();
            }
            catch (Exception ex)
            {
                
            }
            return id;
        }
    }
}
