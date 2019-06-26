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
    class Judge
    {
        public Judge()
        {
            idsandscores = new string[2,9];
        }

        string[,] idsandscores;

        private decimal finalscore;

        public void printJudgesToListBox(ListBox rec)
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, Name from Judges;", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rec.Items.Add(reader[0].ToString() + '\t' + reader[1].ToString());
            }
            conn.Close();
        }

        public void getJudgesScores(ListBox Judge1Name, ListBox Judge1Score, ListBox Judge2Name, ListBox Judge2Score, ListBox Judge3Name, ListBox Judge3Score, ListBox Judge4Name, ListBox Judge4Score, ListBox Judge5Name, ListBox Judge5Score, ListBox Judge6Name, ListBox Judge6Score, ListBox Judge7Name, ListBox Judge7Score, ListBox Judge8Name, ListBox Judge8Score, ListBox Judge9Name, ListBox Judge9Score)
        {
            idsandscores[0,0] = Judge1Name.SelectedItem.ToString().Substring(0, 2);
            idsandscores[1,0] = Judge1Score.SelectedItem.ToString();
            idsandscores[0,1] = Judge2Name.SelectedItem.ToString().Substring(0, 2);
            idsandscores[1,1] = Judge2Score.SelectedItem.ToString();
            idsandscores[0,2] = Judge3Name.SelectedItem.ToString().Substring(0, 2);
            idsandscores[1,2] = Judge3Score.SelectedItem.ToString();
            idsandscores[0,3] = Judge4Name.SelectedItem.ToString().Substring(0, 2);
            idsandscores[1,3] = Judge4Score.SelectedItem.ToString();
            idsandscores[0,4] = Judge5Name.SelectedItem.ToString().Substring(0, 2);
            idsandscores[1,4] = Judge5Score.SelectedItem.ToString();
            idsandscores[0,5] = Judge6Name.SelectedItem.ToString().Substring(0, 2);
            idsandscores[1,5] = Judge6Score.SelectedItem.ToString();
            idsandscores[0,6] = Judge7Name.SelectedItem.ToString().Substring(0, 2);
            idsandscores[1,6] = Judge7Score.SelectedItem.ToString();
            idsandscores[0,7] = Judge8Name.SelectedItem.ToString().Substring(0, 2);
            idsandscores[1,7] = Judge8Score.SelectedItem.ToString();
            idsandscores[0,8] = Judge9Name.SelectedItem.ToString().Substring(0, 2);
            idsandscores[1,8] = Judge9Score.SelectedItem.ToString();
        }

        public void enterSingleScoresIntoDB(FigureSkatingEvent CurrFigureSkatingEvent, Athlete CurrAthlete)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            string updatestring = "Update FigureSkatingScores Set Judge1ID = " + idsandscores[0, 0] + ", Judge1Score = " + idsandscores[1, 0] + ", Judge2ID = " + idsandscores[0, 1] + ", Judge2Score = " + idsandscores[1, 1] + ", Judge3ID = " + idsandscores[0, 2] + ", Judge3Score = " + idsandscores[1, 2] + ", Judge4ID = " + idsandscores[0, 3] + ", Judge4Score = " + idsandscores[1, 3] + ", Judge5ID = " + idsandscores[0, 4] + ", Judge5Score = " + idsandscores[1, 4] + ", Judge6ID = " + idsandscores[0, 5] + ", Judge6Score = " + idsandscores[1, 5] + ", Judge7ID = " + idsandscores[0, 6] + ", Judge7Score = " + idsandscores[1, 6] + ", Judge8ID = " + idsandscores[0, 7] + ", Judge8Score = " + idsandscores[1, 7] + ", Judge9ID = " + idsandscores[0, 8] + ", Judge9Score = " + idsandscores[1, 8] + " where EventID = "+ CurrFigureSkatingEvent.giveEventkId() +" and ParticipantID = "+ CurrAthlete.giveAthleteID() +"; ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(updatestring, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void enterSingleFinalScoreIntoDB(FigureSkatingEvent CurrFigureSkatingEvent, Athlete CurrAthlete)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            string updatestring = "Update FigureSkatingScores Set Score = "+ finalscore + "where EventID = " + CurrFigureSkatingEvent.giveEventkId() + " and ParticipantID = " + CurrAthlete.giveAthleteID() + "; ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(updatestring, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void enterPairScoresIntoDB(FigureSkatingEvent CurrFigureSkatingEvent, Pair CurrPair)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            string updatestring = "Update FigureSkatingScores Set Judge1ID = " + idsandscores[0, 0] + ", Judge1Score = " + idsandscores[1, 0] + ", Judge2ID = " + idsandscores[0, 1] + ", Judge2Score = " + idsandscores[1, 1] + ", Judge3ID = " + idsandscores[0, 2] + ", Judge3Score = " + idsandscores[1, 2] + ", Judge4ID = " + idsandscores[0, 3] + ", Judge4Score = " + idsandscores[1, 3] + ", Judge5ID = " + idsandscores[0, 4] + ", Judge5Score = " + idsandscores[1, 4] + ", Judge6ID = " + idsandscores[0, 5] + ", Judge6Score = " + idsandscores[1, 5] + ", Judge7ID = " + idsandscores[0, 6] + ", Judge7Score = " + idsandscores[1, 6] + ", Judge8ID = " + idsandscores[0, 7] + ", Judge8Score = " + idsandscores[1, 7] + ", Judge9ID = " + idsandscores[0, 8] + ", Judge9Score = " + idsandscores[1, 8] + " where EventID = " + CurrFigureSkatingEvent.giveEventkId() + " and ParticipantID = " + CurrPair.givePairID() + "; ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(updatestring, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void enterPairFinalScoreIntoDB(FigureSkatingEvent CurrFigureSkatingEvent, Pair CurrPair)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            string updatestring = "Update FigureSkatingScores Set Score = " + finalscore + "where EventID = " + CurrFigureSkatingEvent.giveEventkId() + " and ParticipantID = " + CurrPair.givePairID() + "; ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(updatestring, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void sortscores()
        {
            for(int i = 0; i<9-1 ;i++)
            {
                for(int j = 0; j< 9 - i - 1 ;j++)
                {
                    if(Convert.ToDecimal(idsandscores[1,j]) > Convert.ToDecimal(idsandscores[1,j+1]))
                    {
                        string tempscore = idsandscores[1,j];
                        string tempid = idsandscores[0,j];
                        idsandscores[1,j] = idsandscores[1,j+1];
                        idsandscores[0,j] = idsandscores[0,j+1];
                        idsandscores[1,j + 1] = tempscore;
                        idsandscores[0,j + 1] = tempid;
                    }
                }
            }
        }

        public void computeFinalScore()
        {
        finalscore = 0;
         for (int i = 1; i <= 7; i++)
           finalscore = Convert.ToDecimal(idsandscores[1,i]) + finalscore;
        finalscore = finalscore / 7;
            finalscore = Decimal.Round(finalscore, 2);
        }

        public void emptyJudges()
        {
            for(int i = 0; i < 9;i++ )
            {
                for (int j = 0; j < 2; j++)
                    idsandscores[j, i] = null;
            }
        }


    }
}
