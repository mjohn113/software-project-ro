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
    class Event
    {
        public void printEventsToList(ListBox curr)
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, Name from Event where ID not in (select ID from EventResults);", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                curr.Items.Add(reader[0].ToString()+'\t'+reader[1].ToString());
            }
            conn.Close();
        }

        public void setEventID(ListBox curr)
        {
            if(curr.SelectedIndex  != -1)
                id = curr.SelectedItem.ToString().Substring(0,2);
        }

        public string giveEventkId()
        {
            return id;
        }


        protected   string id;
    } 
}
