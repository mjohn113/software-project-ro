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
    class Athlete
    {
        protected int athleteID;
        private string level;
        private string sex;
        private string firstName;
        private string lastName;
        private int age;

        public int giveAthleteID()
        {
            return athleteID;
        }

        public void setAttributes(string newFirstName, string newLastName, int newAge, string newLevel, string newSex)
        {
            firstName = newFirstName;
            lastName = newLastName;
            age = newAge;
            level = newLevel;
            sex = newSex;
        }

        public void setAttributesByID(ListBox Give)
        {
            athleteID = Convert.ToInt32(Give.SelectedItem.ToString().Substring(0, 4));
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select level, sex from Athlete where ID = " + athleteID + ";", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                level = reader[0].ToString();
                sex = reader[1].ToString();
            }
            conn.Close();
        }

        public string giveLevel()
        {
            return level;
        }

        public string giveSex()
        {
            return sex;
        }

        public void produceAndSetKey()
        {
            SqlDataReader reader;
            string insertString = "Insert into Participant(type) values ('Single');";
            string returnString = "Select MAX(ID) from Participant;";
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(insertString, conn);
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand(returnString, conn);
            reader = cmd2.ExecuteReader();
            while(reader.Read())
            {
                athleteID = Convert.ToInt32(reader[0]);
            }

            conn.Close();

        }

        public void registerCurr(Country CurrCountry)
        {
            string insertString = "Insert into Athlete(ID,firstName,lastName,age,level,sex,GoldMedalCount, SilverMedalCount, BronzeMedalCount, countryID) values(" + athleteID + ",'" + firstName + "','" + lastName + "'," + age + ",'" + level + "','" + sex + "',0,0,0," + CurrCountry.returnCountryID() + ");";
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(insertString, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void printAthletesToListByCountry(ListBox give,ListBox rec)
        {
            rec.Items.Clear();
            string countryID = give.SelectedItem.ToString().Substring(0,4);
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, firstName, lastName from Athlete where countryID = "+ countryID + ";", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rec.Items.Add(reader[0].ToString() + '\t' + reader[1].ToString() + " " + reader[2].ToString());
            }
            conn.Close();
        }

        public void printAthletesToListByCountryAndSex(ListBox give, ListBox rec, String chosensex)
        {
            rec.Items.Clear();
            string countryID = give.SelectedItem.ToString().Substring(0, 4);
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, firstName, lastName from Athlete where countryID = " + countryID + "and sex = '" + chosensex + "';", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rec.Items.Add(reader[0].ToString() + '\t' + reader[1].ToString() + " " + reader[2].ToString());
            }
            conn.Close();
        }


    }
}
