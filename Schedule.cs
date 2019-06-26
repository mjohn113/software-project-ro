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
    class Schedule
    {
        public Schedule()
        { }

        public void printtogrid(DataGridView curr)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter("select Schedule.Date, Schedule.Time, Rink1.Name as 'Samsung Rink' , Rink2.Name as 'Apple Rink', Rink3.Name as 'Pixel Rink' from Schedule left join Event as Rink1 on Rink1.ID = Schedule.Rink1Event left join Event as Rink2 on Rink2.ID = Schedule.Rink2Event left join Event as Rink3 on Rink3.ID = Schedule.Rink3Event; ", conn);
            DataTable datatable = new DataTable();
            sqlDataAdap.Fill(datatable);
            curr.DataSource = datatable;
            conn.Close();
        }
        
        public void printdatestolist(ListBox curr)
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select distinct Date from Schedule;", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                curr.Items.Add(reader[0].ToString().Substring(0,9));
            }
            conn.Close();
        }

        public void printtimestolist(ListBox curr)
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select distinct Time from Schedule;", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                curr.Items.Add(reader[0].ToString());
            }
            conn.Close();
        }

        public void enterIntoSchedule(Event CurrEvent, Rink CurrRink, Label TestingLabel)
        {
            string rinkid = CurrRink.giveRinkId() + "Event";
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            string insertionstring = "UPDATE Schedule SET " + rinkid + " = " + CurrEvent.giveEventkId() + " where Date = '" + date + "' and Time = '" + time + "';";
            TestingLabel.Text = insertionstring;
            conn.Open();
            SqlCommand cmd = new SqlCommand(insertionstring  , conn);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void setenterdate(ListBox curr)
        {
            date = curr.SelectedItem.ToString();
        }

        public void setentertime(ListBox curr)
        {
            time = curr.SelectedItem.ToString();
        }



        private string date;
        private string time;
    }

}
