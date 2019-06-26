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
    class Referee
    {
        private string minutes;
        private string seconds;
        private string milliseconds;

        public void setTime(ListBox minutebox, ListBox secondbox, ListBox millisecondbox)
        {
            minutes = minutebox.SelectedItem.ToString();
            seconds = secondbox.SelectedItem.ToString();
            milliseconds = millisecondbox.SelectedItem.ToString();
        }

        public void enterAthleteTime(Athlete currAthlete, SpeedSkatingEvent currEvent)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            string timestring = "00:" + minutes + ":" + seconds + ":" + milliseconds;
            string updatestring = "update SpeedSkatingTimes set Time = '" + timestring + "' where EventID = " + currEvent.giveEventkId() + " and ParticipantID = " + currAthlete.giveAthleteID() + ";";
            conn.Open();
            SqlCommand cmd = new SqlCommand(updatestring, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
