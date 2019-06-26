using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace WinterOlympics
{
    class Rink
    {
        public void printRinksToList(ListBox curr)
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Rinks;", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                curr.Items.Add(reader[0].ToString()+'\t'+reader[1].ToString());
            }
            conn.Close();
        }



        public string giveRinkId()
        {
            return id;
        }

        public void setRinkID(ListBox curr)
        {
           id = curr.SelectedItem.ToString().Substring(0,5);
        }

        private string id;
    }
}
