using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinterOlympics
{
    public partial class Form1 : Form
    {
        Schedule ScheduleCurr = new Schedule();
        Oeo OeoCurr = new Oeo();
        Event EventCurr = new Event();
        Rink RinkCurr = new Rink();
        Country CountryCurr = new Country();
        Athlete AthleteCurr = new Athlete();
        SpeedSkatingEvent SpeedSkatingEventCurr = new SpeedSkatingEvent();
        FigureSkatingEvent FigureSkatingEventCurr = new FigureSkatingEvent();
        Referee RefereeCurr = new Referee();
        Judge JudgeCurr = new Judge();
        Pair PairCurr = new Pair();
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-TGJJO15J\\sqlexpress;Initial Catalog=WinterOlympics;Integrated Security=TRUE;MultipleActiveResultSets=True");
        public Form1()
        {
            InitializeComponent();
            ScheduleCurr.printtogrid(dataGridView1);
            ScheduleCurr.printdatestolist(EventDateList);
            ScheduleCurr.printtimestolist(EventTimeList);
            OeoCurr.printAthleteMedalCountToGrid(AthleteMedalGrid);
            OeoCurr.printCountryMedalCountToGrid(CountryMedalGrid);
            OeoCurr.printPairMedalWinnersToGrid(PairMedalWinners);
            OeoCurr.printSingleMedalWinnersToGrid(SingleMedalWinners);
            EventCurr.printEventsToList(ScheduleEventList);
            RinkCurr.printRinksToList(RinkList);
            CountryCurr.printCountriesToList(AthleteCountry);
            CountryCurr.printCountriesToList(CountrySSBox);
            CountryCurr.printCountriesToList(CountryFSSBox);
            CountryCurr.printCountriesToList(CountryFSPBox);
            SpeedSkatingEventCurr.printSpeedSkatingEvents(TimingSpeedSkatingEventBox);
            JudgeCurr.printJudgesToListBox(Judge1ID);
            FigureSkatingEventCurr.printFigureSkatingPairEventsToList(FSPEventBox);
            FigureSkatingEventCurr.printFigureSkatingEvents(FinalizeSingleFigureBox);
            SpeedSkatingEventCurr.printSpeedSkatingEvents(FinalizeSpeedBox);
            FigureSkatingEventCurr.printFigureSkatingPairEventsToList(FinalizePairFigureBox);


            initializejudgescores();
            addages();
            initializetimes();

        }

        private void EnterIntoScheduleButton_Click(object sender, EventArgs e)
        {
            ScheduleCurr.enterIntoSchedule(EventCurr, RinkCurr, TestingLabel);
            ScheduleCurr.printtogrid(dataGridView1);
        }

        private void EventDateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScheduleCurr.setenterdate(EventDateList);
        }

        private void EventTimeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScheduleCurr.setentertime(EventTimeList);
        }

        private void RinkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RinkCurr.setRinkID(RinkList);
        }

        private void ScheduleEventList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventCurr.setEventID(ScheduleEventList);
        }

        private void CountryRegistrationButton_Click(object sender, EventArgs e)
        {
            CountryCurr.setCountryName(CountryRegistrationTextBox);
            CountryCurr.registerTheCountry();
            OeoCurr.printCountryMedalCountToGrid(CountryMedalGrid);
            CountryRegistrationTextBox.Clear();
            CountryCurr.printCountriesToList(AthleteCountry);
            CountryCurr.printCountriesToList(CountrySSBox);
            CountryCurr.printCountriesToList(CountryFSSBox);
            CountryCurr.printCountriesToList(CountryFSPBox);
        }

        private void addages()
        {
            for (int i = 10; i <= 100; i++)
                AthleteAge.Items.Add(i);
        }

        private void AthleteCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(AthleteCountry.SelectedIndex != -1)
                CountryCurr.setCountryID(Convert.ToInt32(AthleteCountry.SelectedItem.ToString().Substring(0,4)));
        }

        private void registerAthleteButton_Click(object sender, EventArgs e)
        {
            AthleteCurr.setAttributes(FirstNameText.Text, LastNameText.Text, Convert.ToInt32(AthleteAge.SelectedItem), AthleteLevel.SelectedItem.ToString(), AthleteSex.SelectedItem.ToString());
            AthleteCurr.produceAndSetKey();
            AthleteCurr.registerCurr(CountryCurr);
            FirstNameText.Clear();
            LastNameText.Clear();
            AthleteAge.SelectedIndex = -1;
            AthleteLevel.SelectedIndex = -1;
            AthleteSex.SelectedIndex = -1;
            AthleteCountry.SelectedIndex = -1;
        }

        private void CountrySSBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AthleteCurr.printAthletesToListByCountry(CountrySSBox, AthleteSSBox);
        }

        private void CountryFSSBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AthleteCurr.printAthletesToListByCountry(CountryFSSBox, AthleteFSSBox);
        }

        private void AthleteSSBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AthleteCurr.setAttributesByID(AthleteSSBox);
            SpeedSkatingEventCurr.printSpeedSkatingEventsByAttributesToList(AthleteCurr, SSEventBox);
        }

        private void AthleteFSSBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AthleteCurr.setAttributesByID(AthleteFSSBox);
            FigureSkatingEventCurr.printFigureSkatingEventsByAttributesToList(AthleteCurr, FSSEventBox);
        }

        private void CountryFSPBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AthleteCurr.printAthletesToListByCountryAndSex(CountryFSPBox, MaleAthleteFSPBox, "Male");
            AthleteCurr.printAthletesToListByCountryAndSex(CountryFSPBox, FemaleAthleteFSPBox, "Female");
        }

        private void TimingSpeedSkatingEventBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TimingSpeedSkatingEventBox.SelectedIndex != -1)
            {
                SpeedSkatingEventCurr.setEventID(TimingSpeedSkatingEventBox);
                SpeedSkatingEventCurr.printAthletesBySSEvent(TimingAthleteBox);
                SpeedSkatingEventCurr.printCurrentEventResultsToGrid(CurrentEventResults);
            }
        }

        private void RegisterSSEventButton_Click(object sender, EventArgs e)
        {
            AthleteCurr.setAttributesByID(AthleteSSBox);
            SpeedSkatingEventCurr.setEventID(SSEventBox);
            SpeedSkatingEventCurr.registerAthleteForEvent(AthleteCurr);
        }

        private void initializetimes()
        {
            for(int i=0;i<60;i++)
            {
                string twodigits = i.ToString();
                if (twodigits.Length == 1)
                    twodigits = "0" + twodigits;
                Minutes.Items.Add(twodigits);
                Seconds.Items.Add(twodigits);
            }
            for (int i = 0; i < 1000; i++)
            {
                string threedigits = i.ToString();
                if (threedigits.Length == 1)
                    threedigits = "00" + threedigits;
                if (threedigits.Length == 2)
                    threedigits = "0" + threedigits;
                Milliseconds.Items.Add(threedigits);
            }
        }

        private void EnterTimeButton_Click(object sender, EventArgs e)
        {
            AthleteCurr.setAttributesByID(TimingAthleteBox);
            SpeedSkatingEventCurr.setEventID(TimingSpeedSkatingEventBox);
            RefereeCurr.setTime(Minutes, Seconds, Milliseconds);
            RefereeCurr.enterAthleteTime(AthleteCurr, SpeedSkatingEventCurr);
            SpeedSkatingEventCurr.printCurrentEventResultsToGrid(CurrentEventResults);
            TimingSpeedSkatingEventBox.ClearSelected();
            Minutes.ClearSelected();
            Seconds.ClearSelected();
            Milliseconds.ClearSelected();
        }

        private void Judge1ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Judge2ID.Items.Clear();
            Judge3ID.Items.Clear();
            Judge4ID.Items.Clear();
            Judge5ID.Items.Clear();
            Judge6ID.Items.Clear();
            Judge7ID.Items.Clear();
            Judge8ID.Items.Clear();
            Judge9ID.Items.Clear();
            if (Judge1ID.SelectedIndex != -1)
            {
                Judge2ID.Items.AddRange(Judge1ID.Items);
                Judge2ID.Items.RemoveAt(Judge1ID.SelectedIndex);
            }
        }

        private void Judge2ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Judge3ID.Items.Clear();
            Judge4ID.Items.Clear();
            Judge5ID.Items.Clear();
            Judge6ID.Items.Clear();
            Judge7ID.Items.Clear();
            Judge8ID.Items.Clear();
            Judge9ID.Items.Clear();
            Judge3ID.Items.AddRange(Judge2ID.Items);
            Judge3ID.Items.RemoveAt(Judge2ID.SelectedIndex);
        }

        private void Judge3ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Judge4ID.Items.Clear();
            Judge5ID.Items.Clear();
            Judge6ID.Items.Clear();
            Judge7ID.Items.Clear();
            Judge8ID.Items.Clear();
            Judge9ID.Items.Clear();
            Judge4ID.Items.AddRange(Judge3ID.Items);
            Judge4ID.Items.RemoveAt(Judge3ID.SelectedIndex);
        }

        private void Judge4ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Judge5ID.Items.Clear();
            Judge6ID.Items.Clear();
            Judge7ID.Items.Clear();
            Judge8ID.Items.Clear();
            Judge9ID.Items.Clear();
            Judge5ID.Items.AddRange(Judge4ID.Items);
            Judge5ID.Items.RemoveAt(Judge4ID.SelectedIndex);
        }

        private void Judge5ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Judge6ID.Items.Clear();
            Judge7ID.Items.Clear();
            Judge8ID.Items.Clear();
            Judge9ID.Items.Clear();
            Judge6ID.Items.AddRange(Judge5ID.Items);
            Judge6ID.Items.RemoveAt(Judge5ID.SelectedIndex);
        }

        private void Judge6ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Judge7ID.Items.Clear();
            Judge8ID.Items.Clear();
            Judge9ID.Items.Clear();
            Judge7ID.Items.AddRange(Judge6ID.Items);
            Judge7ID.Items.RemoveAt(Judge6ID.SelectedIndex);
        }

        private void Judge7ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Judge8ID.Items.Clear();
            Judge9ID.Items.Clear();
            Judge8ID.Items.AddRange(Judge7ID.Items);
            Judge8ID.Items.RemoveAt(Judge7ID.SelectedIndex);
        }

        private void Judge8ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Judge9ID.Items.Clear();
            Judge9ID.Items.AddRange(Judge8ID.Items);
            Judge9ID.Items.RemoveAt(Judge8ID.SelectedIndex);
        }

        private void FigureSkatingEventName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SingleOrPair.SelectedItem.ToString() == "Single")
            {
                if (FigureSkatingEventName.SelectedIndex != -1)
                {
                    FigureSkatingEventCurr.setEventID(FigureSkatingEventName);
                    FigureSkatingEventCurr.printAthletesByFSSEvent(FigureSkatingAthleteName);
                    FigureSkatingEventCurr.printCurrentSingleEventResultsToGrid(FigureSkatingCurrGrid);
                }
            }
            if (SingleOrPair.SelectedItem.ToString() == "Pair")
            {
                if (FigureSkatingEventName.SelectedIndex != -1)
                {
                    FigureSkatingEventCurr.setEventID(FigureSkatingEventName);
                    FigureSkatingEventCurr.printAthletesFromFSPEvent(FigureSkatingAthleteName);
                    FigureSkatingEventCurr.printCurrentPairEventResultsToGrid(FigureSkatingCurrGrid);
                }

            }

        }

        private void RegisterFSSEventButton_Click(object sender, EventArgs e)
        {
            AthleteCurr.setAttributesByID(AthleteFSSBox);
            FigureSkatingEventCurr.setEventID(FSSEventBox);
            FigureSkatingEventCurr.registerAthleteForEvent(AthleteCurr);
        }

        private void RegisterFSPEventButton_Click(object sender, EventArgs e)
        {
            PairCurr.produceAndSetPairKey();
            PairCurr.setBothAthleteIDs(MaleAthleteFSPBox, FemaleAthleteFSPBox);
            PairCurr.makePair();
            FigureSkatingEventCurr.setEventID(FSPEventBox);
            FigureSkatingEventCurr.registerPairForEvent(PairCurr);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FigureSkatingEventName.Items.Clear();
            FigureSkatingAthleteName.Items.Clear();
            if (SingleOrPair.SelectedItem.ToString() == "Single")
                FigureSkatingEventCurr.printFigureSkatingEvents(FigureSkatingEventName);
            if (SingleOrPair.SelectedItem.ToString() == "Pair")
                FigureSkatingEventCurr.printFigureSkatingPairEventsToList(FigureSkatingEventName);

        }

        private void initializejudgescores()
        {
            for(decimal i = 10; i>= 0; i = i - Convert.ToDecimal(.1))
            {
                Judge1Score.Items.Add(i);
                Judge2Score.Items.Add(i);
                Judge3Score.Items.Add(i);
                Judge4Score.Items.Add(i);
                Judge5Score.Items.Add(i);
                Judge6Score.Items.Add(i);
                Judge7Score.Items.Add(i);
                Judge8Score.Items.Add(i);
                Judge9Score.Items.Add(i);
            }
        }

        private void EnterJudgesScores_Click(object sender, EventArgs e)
        {
            JudgeCurr.getJudgesScores(Judge1ID, Judge1Score, Judge2ID, Judge2Score, Judge3ID, Judge3Score, Judge4ID, Judge4Score, Judge5ID, Judge5Score, Judge6ID, Judge6Score, Judge7ID, Judge7Score, Judge8ID, Judge8Score, Judge9ID, Judge9Score);
            if (SingleOrPair.SelectedItem.ToString() == "Single")
            {
                JudgeCurr.enterSingleScoresIntoDB(FigureSkatingEventCurr, AthleteCurr);
                JudgeCurr.sortscores();
                JudgeCurr.computeFinalScore();
                JudgeCurr.enterSingleFinalScoreIntoDB(FigureSkatingEventCurr, AthleteCurr);
            }
            if (SingleOrPair.SelectedItem.ToString() == "Pair")
            {
                JudgeCurr.enterPairScoresIntoDB(FigureSkatingEventCurr, PairCurr);
                JudgeCurr.sortscores();
                JudgeCurr.computeFinalScore();
                JudgeCurr.enterPairFinalScoreIntoDB(FigureSkatingEventCurr, PairCurr);
            }
            JudgeCurr.emptyJudges();
            Judge1ID.SelectedIndex = -1;
        }

        private void FigureSkatingAthleteName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SingleOrPair.SelectedItem.ToString() == "Single")
            {
                if (FigureSkatingAthleteName.SelectedIndex != -1)
                    AthleteCurr.setAttributesByID(FigureSkatingAthleteName);
            };
            if (SingleOrPair.SelectedItem.ToString() == "Pair")
            {
                if (FigureSkatingAthleteName.SelectedIndex != -1)
                    PairCurr.setpairid(FigureSkatingAthleteName);
            }
        }

        private void FinalizeSingleFigureBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FinalizeSingleFigureBox.SelectedIndex != -1)
            {
                FigureSkatingEventCurr.setEventID(FinalizeSingleFigureBox);
                FigureSkatingEventCurr.printCurrentSingleEventResultsToGrid(FinalizeSingleFigureGrid);
            }
        }

        private void FinalizeSpeedBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FinalizeSpeedBox.SelectedIndex != -1)
            {
                SpeedSkatingEventCurr.setEventID(FinalizeSpeedBox);
                SpeedSkatingEventCurr.printCurrentEventResultsToGrid(FinalizeSpeedGrid);
            }

        }


        private void FinalizeSpeedButton_Click(object sender, EventArgs e)
        {
            OeoCurr.finalizeSpeedSkatingEvent(SpeedSkatingEventCurr);
            OeoCurr.printAthleteMedalCountToGrid(AthleteMedalGrid);
            OeoCurr.printCountryMedalCountToGrid(CountryMedalGrid);
            OeoCurr.printSingleMedalWinnersToGrid(SingleMedalWinners);
        }

        private void FinalizeSingleFigureButton_Click(object sender, EventArgs e)
        {
            OeoCurr.finalizeSingleFigureSkatingEvent(FigureSkatingEventCurr);
            OeoCurr.printAthleteMedalCountToGrid(AthleteMedalGrid);
            OeoCurr.printCountryMedalCountToGrid(CountryMedalGrid);
            OeoCurr.printSingleMedalWinnersToGrid(SingleMedalWinners);
        }

        private void FinalizePairFigureBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FinalizePairFigureBox.SelectedIndex != -1)
            {
                FigureSkatingEventCurr.setEventID(FinalizePairFigureBox);
                FigureSkatingEventCurr.printCurrentPairEventResultsToGrid(FinalizePairFigureGrid);
            }
        }

        private void FinalizePairFigure_Click(object sender, EventArgs e)
        {
            OeoCurr.finalizePairFigureSkatingEvent(FigureSkatingEventCurr);
            OeoCurr.printAthleteMedalCountToGrid(AthleteMedalGrid);
            OeoCurr.printCountryMedalCountToGrid(CountryMedalGrid);
            OeoCurr.printPairMedalWinnersToGrid(PairMedalWinners);
        }

        private void FSPEventBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(FSPEventBox.SelectedIndex != -1)
            {
                FigureSkatingEventCurr.setEventID(FSPEventBox);
            }
        }
    }
}
