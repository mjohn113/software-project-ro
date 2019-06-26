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
    class Oeo
    {
        public Oeo()
        {
            medalistID = new int[3];
            medalistcountryID = new int[3];
            secondmedalistID = new int[3];
            pairmedalistID = new int[3];
        }
        private int[] medalistID;
        private int[] secondmedalistID;
        private int[] pairmedalistID;
        private int[] medalistcountryID;

        public void printAthleteMedalCountToGrid(DataGridView curr)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter("Select firstName, lastName,GoldMedalCount, SilverMedalCount, BronzeMedalCount from Athlete order by GoldMedalCount DESC, SilverMedalCount DESC, BronzeMedalCount DESC;", conn);
            DataTable datatable = new DataTable();
            sqlDataAdap.Fill(datatable);
            curr.DataSource = datatable;
            conn.Close();
        }

        public void printCountryMedalCountToGrid(DataGridView curr)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter("Select CountryName, GoldCount, SilverCount, BronzeCount from Country order by GoldCount DESC, SilverCount DESC, BronzeCount DESC;", conn);
            DataTable datatable = new DataTable();
            sqlDataAdap.Fill(datatable);
            curr.DataSource = datatable;
            conn.Close();
        }

        public void printSingleMedalWinnersToGrid(DataGridView curr)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter("Select Event.Name as 'Event Name', Athlete1.firstName + ' ' + Athlete1.lastName as 'Gold Medalist', Athlete2.firstName + ' ' + Athlete2.lastName as 'Silver Medalist', Athlete3.firstName + ' ' + Athlete3.lastName as 'Bronze Medalist' from EventResults inner join Athlete as Athlete1 on Athlete1.ID = EventResults.GoldMedalistID inner join Athlete as Athlete2 on Athlete2.ID = EventResults.SilverMedalistID inner join Athlete as Athlete3 on Athlete3.ID = EventResults.BronzeMedalistID inner join Event on Event.ID = EventResults.ID where GoldMedalistID in (select ID from Athlete); ", conn);
            DataTable datatable = new DataTable();
            sqlDataAdap.Fill(datatable);
            curr.DataSource = datatable;
            conn.Close();
        }

        public void printPairMedalWinnersToGrid(DataGridView curr)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter("Select Event.Name as 'Event Name', Athlete11.firstName + ' ' + Athlete11.lastName + ' and ' + Athlete12.firstName + ' ' + Athlete12.lastName  as 'Gold Medalist', Athlete21.firstName + ' ' + Athlete21.lastName + ' and ' + Athlete22.firstName + ' ' + Athlete22.lastName as 'Silver Medalist', Athlete31.firstName + ' ' + Athlete31.lastName + ' and ' + Athlete32.firstName + ' ' + Athlete32.lastName as 'Bronze Medalist' from EventResults inner join Pair as Pair1 on Pair1.ID = EventResults.GoldMedalistID inner join Pair as Pair2 on Pair2.ID = EventResults.SilverMedalistID inner join Pair as Pair3 on Pair3.ID = EventResults.BronzeMedalistID inner join Athlete as Athlete11 on Athlete11.ID = Pair1.Athlete1ID inner join Athlete as Athlete12 on Athlete12.ID = Pair1.Athlete2ID inner join Athlete as Athlete21 on Athlete21.ID = Pair2.Athlete1ID inner join Athlete as Athlete22 on Athlete22.ID = Pair2.Athlete2ID inner join Athlete as Athlete31 on Athlete31.ID = Pair3.Athlete1ID inner join Athlete as Athlete32 on Athlete32.ID = Pair3.Athlete2ID inner join Event on Event.ID = EventResults.ID where GoldMedalistID not in (select ID from Athlete); ", conn);
            DataTable datatable = new DataTable();
            sqlDataAdap.Fill(datatable);
            curr.DataSource = datatable;
            conn.Close();
        }

        public void finalizeSpeedSkatingEvent(SpeedSkatingEvent CurrSpeedSkatingEvent)
        {
            medalistID[0] = Convert.ToInt32(null);
            medalistID[1] = Convert.ToInt32(null);
            medalistID[2] = Convert.ToInt32(null);
            medalistcountryID[0] = Convert.ToInt32(null);
            medalistcountryID[1] = Convert.ToInt32(null);
            medalistcountryID[2] = Convert.ToInt32(null);
            string selectstring = "Select top 3 Athlete.ID, Athlete.countryID from Athlete INNER JOIN SpeedSkatingTimes on Athlete.ID = SpeedSkatingTimes.ParticipantID where SpeedSkatingTimes.EventID = " + CurrSpeedSkatingEvent.giveEventkId() + " order by SpeedSkatingTimes.Time";
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(selectstring, conn);
            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                medalistID[i] = Convert.ToInt32(reader[0]);
                medalistcountryID[i] = Convert.ToInt32(reader[1]);
                i++;
            }

            updateAthleteMedalCount();
            updateCountryMedalCount();
            updateEventMedalWinners(Convert.ToInt32(CurrSpeedSkatingEvent.giveEventkId()));
        }

        public void finalizeSingleFigureSkatingEvent(FigureSkatingEvent CurrFigureSkatingEvent)
        {
            medalistID[0] = Convert.ToInt32(null);
            medalistID[1] = Convert.ToInt32(null);
            medalistID[2] = Convert.ToInt32(null);
            medalistcountryID[0] = Convert.ToInt32(null);
            medalistcountryID[1] = Convert.ToInt32(null);
            medalistcountryID[2] = Convert.ToInt32(null);
            string selectstring = "Select Top 3 Athlete.ID, Athlete.countryID from Athlete INNER JOIN FigureSkatingScores on Athlete.ID = FigureSkatingScores.ParticipantID where FigureSkatingScores.EventID = " + CurrFigureSkatingEvent.giveEventkId() + " order by FigureSkatingScores.Score DESC";
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(selectstring, conn);
            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                medalistID[i] = Convert.ToInt32(reader[0]);
                medalistcountryID[i] = Convert.ToInt32(reader[1]);
                i++;
            }

            updateAthleteMedalCount();
            updateCountryMedalCount();
            updateEventMedalWinners(Convert.ToInt32(CurrFigureSkatingEvent.giveEventkId()));
        }

        public void finalizePairFigureSkatingEvent(FigureSkatingEvent CurrFigureSkatingEvent)
        {
            medalistID[0] = Convert.ToInt32(null);
            medalistID[1] = Convert.ToInt32(null);
            medalistID[2] = Convert.ToInt32(null);
            medalistcountryID[0] = Convert.ToInt32(null);
            medalistcountryID[1] = Convert.ToInt32(null);
            medalistcountryID[2] = Convert.ToInt32(null);
            secondmedalistID[0] = Convert.ToInt32(null);
            secondmedalistID[1] = Convert.ToInt32(null);
            secondmedalistID[2] = Convert.ToInt32(null);
            pairmedalistID[0] = Convert.ToInt32(null);
            pairmedalistID[1] = Convert.ToInt32(null);
            pairmedalistID[2] = Convert.ToInt32(null);

            string selectstring = "Select Top 3 Athlete1.ID, Athlete2.ID, Athlete2.countryID, Pair.ID from FigureSkatingScores left join Pair on  Pair.ID = FigureSkatingScores.ParticipantID left join Athlete as Athlete1 on Athlete1.ID = Pair.Athlete1ID left join Athlete as Athlete2 on Athlete2.ID = Pair.Athlete2ID where FigureSkatingScores.ParticipantID not in (select ID from Athlete) and  FigureSkatingScores.EventID = " + CurrFigureSkatingEvent.giveEventkId() + "  order by Score DESC;";
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(selectstring, conn);
            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                medalistID[i] = Convert.ToInt32(reader[0]);
                secondmedalistID[i] = Convert.ToInt32(reader[1]);
                medalistcountryID[i] = Convert.ToInt32(reader[2]);
                pairmedalistID[i] = Convert.ToInt32(reader[3]);
                i++;
            }

            updateAthleteMedalCount();
            updateCountryMedalCount();
            for (int j = 0; j < 3; j++)
                medalistID[j] = secondmedalistID[j];
            updateAthleteMedalCount();
            for (int j = 0; j < 3; j++)
                medalistID[j] = pairmedalistID[j];
            updateEventMedalWinners(Convert.ToInt32(CurrFigureSkatingEvent.giveEventkId()));

        }

        private void updateAthleteMedalCount()
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            string goldinsertstring = "Update Athlete set GoldMedalCount = GoldMedalCount + 1 where ID =" + medalistID[0] + " ;";
            string silverinsertstring = "Update Athlete set SilverMedalCount = SilverMedalCount + 1 where ID =" + medalistID[1] + " ;";
            string bronzeinsertstring = "Update Athlete set BronzeMedalCount = BronzeMedalCount + 1 where ID =" + medalistID[2] + " ;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(goldinsertstring, conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand(silverinsertstring, conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand(bronzeinsertstring, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void updateCountryMedalCount()
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            string goldinsertstring = "Update Country set GoldCount = GoldCount + 1 where ID = " + medalistcountryID[0] + " ;";
            string silverinsertstring = "Update Country set SilverCount = SilverCount + 1 where ID = " + medalistcountryID[1] + " ;";
            string bronzeinsertstring = "Update Country set BronzeCount = BronzeCount + 1 where ID = " + medalistcountryID[2] + " ;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(goldinsertstring, conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand(silverinsertstring, conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand(bronzeinsertstring, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void updateEventMedalWinners(int eventid)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            string insertstring = "Insert into EventResults(ID,GoldMedalistID,SilverMedalistID,BronzeMedalistID) values ( " + eventid + ", " + medalistID[0] + ", " + medalistID[1] + ", " + medalistID[2] +") ;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(insertstring, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
