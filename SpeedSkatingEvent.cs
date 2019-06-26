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
    class SpeedSkatingEvent:Event
    {
        public void printSpeedSkatingEventsByAttributesToList(Athlete currAthlete, ListBox curr)
        {
            curr.Items.Clear();
            string level = currAthlete.giveLevel();
            string sex = currAthlete.giveSex();
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, Name from Event where Type = 'Speed Skating' and Level = '"+ level +"' and Sex = '"+ sex +"' and ID not in (select ID from EventResults);", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                curr.Items.Add(reader[0].ToString() + '\t' + reader[1].ToString());
            }
            conn.Close();
        }

        public void printSpeedSkatingEvents(ListBox rec)
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, Name from Event where Type = 'Speed Skating' and ID not in (select ID from EventResults); ", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rec.Items.Add(reader[0].ToString() + '\t' + reader[1].ToString());
            }
            conn.Close();
        }

        public void printAthletesBySSEvent(ListBox rec)
        {
            rec.Items.Clear();
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, firstName, lastName from Athlete where ID in (Select ParticipantID from SpeedSkatingTimes where EventID = "+ id + "and Time is NULL);", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rec.Items.Add(reader[0].ToString() + '\t' + reader[1].ToString() + " " + reader[2].ToString());
            }
            conn.Close();

        }

        public void registerAthleteForEvent(Athlete currAthlete)
        {
            string insertString = "Insert into SpeedSkatingTimes(EventID,ParticipantID) values(" + id + "," + currAthlete.giveAthleteID() + ");";
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(insertString, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void printCurrentEventResultsToGrid(DataGridView rec)
        {
            string selectstring = "Select Athlete.firstName, Athlete.lastName, SpeedSkatingTimes.Time from Athlete INNER JOIN SpeedSkatingTimes on Athlete.ID = SpeedSkatingTimes.ParticipantID where SpeedSkatingTimes.EventID = " + id + " order by SpeedSkatingTimes.Time";
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(selectstring, conn);
            DataTable datatable = new DataTable();
            sqlDataAdap.Fill(datatable);
            rec.DataSource = datatable;
            conn.Close();
        }
    }
}
