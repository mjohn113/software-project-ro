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
    class Country
    {
        public void registerTheCountry()
        {
            string insertionstring = "Insert into Country(CountryName, GoldCount, SilverCount,BronzeCount) values ('" + countryname + "',0,0,0);";
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(insertionstring, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void setCountryName(TextBox currTextBox)
        {
            countryname = currTextBox.Text;
        }

        public void printCountriesToList(ListBox curr)
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ID, CountryName from Country;", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                curr.Items.Add(reader[0].ToString()+" "+ reader[1].ToString());
            }
            conn.Close();
        }

        public int returnCountryID()
        {
            return countryid;
        }

        public void setCountryID(int currID)
        {
            countryid = currID;
        }

        private string countryname;
        private int countryid;
    }
}
