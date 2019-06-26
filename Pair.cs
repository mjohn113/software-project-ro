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
    class Pair:Athlete
    {
        int otherAthleteID;
        int pairID;

        public int givePairID()
        {
            return pairID;
        }

        public void setpairid(ListBox Give)
        {
            pairID = Convert.ToInt32(Give.SelectedItem.ToString().Substring(0, 4));
        }

        public void produceAndSetPairKey()
        {
            SqlDataReader reader;
            string insertString = "Insert into Participant(type) values ('Pair');";
            string returnString = "Select MAX(ID) from Participant;";
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(insertString, conn);
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand(returnString, conn);
            reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                pairID = Convert.ToInt32(reader[0]);
            }

            conn.Close();
        }

        public void setBothAthleteIDs(ListBox Athlete1, ListBox Athlete2)
        {
            athleteID = Convert.ToInt32(Athlete1.SelectedItem.ToString().Substring(0, 4));
            otherAthleteID = Convert.ToInt32(Athlete2.SelectedItem.ToString().Substring(0, 4));
        }

        public void makePair()
        {
            string insertString = "Insert into Pair(ID, Athlete1ID, Athlete2ID) values (" + pairID + "," + athleteID + "," + otherAthleteID + ");";
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(insertString, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }


    }
}
