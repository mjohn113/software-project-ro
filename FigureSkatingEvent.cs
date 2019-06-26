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
    class FigureSkatingEvent:Event
    {
        public void printFigureSkatingPairEventsToList(ListBox rec)
        {
            rec.Items.Clear();
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, Name from Event where sex = 'Both' and ID not in (select ID from EventResults);", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rec.Items.Add(reader[0].ToString() + '\t' + reader[1].ToString());
            }
            conn.Close();

        }
        public void printFigureSkatingEventsByAttributesToList(Athlete currAthlete, ListBox curr)
        {
            curr.Items.Clear();
            string level = currAthlete.giveLevel();
            string sex = currAthlete.giveSex();
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, Name from Event where Type = 'Figure Skating' and Level = '" + level + "' and Sex = '" + sex + "' and ID not in (select ID from EventResults);", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                curr.Items.Add(reader[0].ToString() + '\t' + reader[1].ToString());
            }
            conn.Close();
        }

        public void registerAthleteForEvent(Athlete currAthlete)
        {
            string insertString = "Insert into FigureSkatingScores(EventID,ParticipantID) values(" + id + "," + currAthlete.giveAthleteID() + ");";
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(insertString, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void registerPairForEvent(Pair currPair)
        {
            string insertString = "Insert into FigureSkatingScores(EventID,ParticipantID) values(" + id + "," + currPair.givePairID() + ");";
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(insertString, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void printAthletesByFSSEvent(ListBox rec)
        {
            rec.Items.Clear();
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, firstName, lastName from Athlete where ID in (Select ParticipantID from FigureSkatingScores where EventID = " + id + "and Score is NULL);", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rec.Items.Add(reader[0].ToString() + '\t' + reader[1].ToString() + " " + reader[2].ToString());
            }
            conn.Close();

        }

        public void printAthletesFromFSPEvent(ListBox rec)
        {
            rec.Items.Clear();
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Participant.ID , Athlete1.firstName + ' ' + Athlete1.lastName as 'Athlete One Name', Athlete2.firstName + ' ' + Athlete2.lastName as 'Athlete Two Name' from Participant inner join Pair on Pair.ID = Participant.ID inner join Athlete as Athlete1 on Athlete1.ID = Pair.Athlete1ID inner join Athlete as Athlete2 on Athlete2.ID = Pair.Athlete2ID where Participant.ID in (select ParticipantID from FigureSkatingScores where score is NULL); ", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rec.Items.Add(reader[0].ToString() + '\t' + reader[1].ToString() + " " + reader[2].ToString());
            }
            conn.Close();

        }

        public void printFigureSkatingEvents(ListBox rec)
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, Name from Event where Type = 'Figure Skating' and Sex != 'Both' and ID not in (select ID from EventResults);", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rec.Items.Add(reader[0].ToString() + '\t' + reader[1].ToString());
            }
            conn.Close();
        }

        public void printCurrentSingleEventResultsToGrid(DataGridView rec)
        {
            string selectstring = "Select Athlete.firstName, Athlete.lastName, FigureSkatingScores.Score from Athlete INNER JOIN FigureSkatingScores on Athlete.ID = FigureSkatingScores.ParticipantID where FigureSkatingScores.EventID = " + id + " order by FigureSkatingScores.Score DESC";
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(selectstring, conn);
            DataTable datatable = new DataTable();
            sqlDataAdap.Fill(datatable);
            rec.DataSource = datatable;
            conn.Close();
        }

        public void printCurrentPairEventResultsToGrid(DataGridView rec)
        {
            string selectstring = "Select FigureSkatingScores.ParticipantID, FigureSkatingScores.Score, Athlete1.firstName, Athlete1.lastName, Athlete2.firstName, Athlete2.lastName from FigureSkatingScores left join Pair on  Pair.ID = FigureSkatingScores.ParticipantID left join Athlete as Athlete1 on Athlete1.ID = Pair.Athlete1ID left join Athlete as Athlete2 on Athlete2.ID = Pair.Athlete2ID where FigureSkatingScores.ParticipantID not in (select ID from Athlete) and  FigureSkatingScores.EventID = " + id + " order by Score DESC; ";
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
