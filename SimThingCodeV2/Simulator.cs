using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimThingCodeV2
{
    class Simulator
    {
        // Fields

        string seriesName, seasonNumber, saveFolder;
        int roundNumber, fileNumber, q2Entries, maxEntries, gridSpacer;
        int baseMinStint, baseMinIncident, baseMinDNF;
        int baseMaxStint, baseMaxIncident, baseMaxDNF;
        int minStint, maxStint;
        int minIncident, maxIncident;
        int minDNF, maxDNF;
        int minTyreWear, maxTyreWear;
        int stintScore, incidentScore, dnfScore, tyreWearScore, pitScore;
        bool firstRace;

        List<int> calendarSpacers, spacers;

        Random randomiser;

        Event currentRound;

        List<Entrant> entryList;
        List<Event> calendar;


        // Constructors

        public Simulator()
        {
            LoadSeriesDetails();

            randomiser = new Random();

            firstRace = true;

            q2Entries = 8;
            maxEntries = 30;
            gridSpacer = 10;

            baseMinStint = 35; // Was previously 25
            baseMaxStint = 61; // Was previously 60
            minIncident = 5;
            baseMaxIncident = 100;// + currentRound.IncidentModifier + entrant.EntrantDriver.CrashScore;
            minDNF = 5;
            baseMaxDNF = 15;// + currentRound.DNF + entrant.EntrantDriver.CrashScore;

            calendarSpacers = new List<int>();
            spacers = new List<int>();

            entryList = new List<Entrant>();
            LoadCalendar();

            currentRound = calendar[0];
            roundNumber = 1;
        }


        // Other Functions

        public void Start()
        {
            roundNumber = SelectStartRace();

            do
            {
                currentRound = calendar[roundNumber];
                roundNumber++;

                SimulateEvent();
                firstRace = false;
            }
            while (Continue());
        }

        public int SelectStartRace()
        {
            int selectedRound;

            for (int roundNumber = 0; roundNumber < calendar.Count(); roundNumber++)
            {
                Console.WriteLine("Round {0}: {1} - {2} - {3} - {4} Laps / {5} Laps", Convert.ToString(roundNumber + 1).PadRight(2, ' '), calendar[roundNumber].RoundTitle.PadRight(calendarSpacers[0], ' '), calendar[roundNumber].TrackName.PadRight(calendarSpacers[1], ' '), calendar[roundNumber].Country.PadRight(calendarSpacers[2], ' '), calendar[roundNumber].R1Distance, calendar[roundNumber].R2Distance);
            }

            Console.Write("Select Start Round Number: ");
            string userInput = Console.ReadLine();

            bool validInt = int.TryParse(userInput, out selectedRound);

            if (validInt && selectedRound > 0 && selectedRound <= calendar.Count())
            {
                return selectedRound - 1;
            }

            else
            {
                Console.WriteLine("INVALID INPUT");
                return SelectStartRace();
            }
        }

        public bool Continue()
        {
            Console.Write("Continue Simulator?\nY - Yes\nN - No\nChoice: ");
            string continueChoice = Console.ReadLine().ToLower();

            if (continueChoice == "y" || continueChoice == "yes")
            {
                return true;
            }

            else if (continueChoice == "n" || continueChoice == "no")
            {
                return false;
            }

            else if (continueChoice == "reload")
            {
                CommonData.Setup();
                Console.WriteLine("Application Reloaded - Press Enter to Continue");
                Console.ReadLine();
                return true;
            }

            return Continue();
        }

        public void LoadSeriesDetails()
        {
            string[] seriesDetails = CommonData.ReadFile(CommonData.SeriesDetailsFile)[0].Split(',');

            seriesName = seriesDetails[0];
            seasonNumber = seriesDetails[1];
        }

        public void LoadEntrants()
        {
            entryList.Clear();
            spacers.Clear();

            List<string> entrantData = CommonData.ReadFile(CommonData.EntryListFile);

            foreach (string entrant in entrantData)
            {
                entryList.Add(new Entrant(entrant.Split(',')));

                if (spacers.Count() == 0)
                {
                    spacers.Add(entryList[entryList.Count() - 1].EntrantDriver.DriverName.Length);
                    spacers.Add(entryList[entryList.Count() - 1].CarNumber.Length);
                    spacers.Add(entryList[entryList.Count() - 1].EntrantTeam.TeamName.Length);
                    spacers.Add(Convert.ToString(entryList[entryList.Count() - 1].EntrantTeam.Tyres.TyreScore).Length);
                }
                else
                {
                    if (entryList[entryList.Count() - 1].EntrantDriver.DriverName.Length > spacers[0])
                    {
                        spacers[0] = entryList[entryList.Count() - 1].EntrantDriver.DriverName.Length;
                    }
                    if (entryList[entryList.Count() - 1].CarNumber.Length > spacers[1])
                    {
                        spacers[1] = entryList[entryList.Count() - 1].CarNumber.Length;
                    }
                    if (entryList[entryList.Count() - 1].EntrantTeam.TeamCode.Length > spacers[2])
                    {
                        spacers[2] = entryList[entryList.Count() - 1].EntrantTeam.TeamCode.Length;
                    }
                    if (Convert.ToString(entryList[entryList.Count() - 1].EntrantTeam.Tyres.TyreScore).Length > spacers[3])
                    {
                        spacers[3] = Convert.ToString(entryList[entryList.Count() - 1].EntrantTeam.Tyres.TyreScore).Length;
                    }
                }
            }
        }

        public void LoadCalendar()
        {
            calendar = new List<Event>();

            List<string> calendarData = CommonData.ReadFile(CommonData.CalendarFile);

            foreach (string round in calendarData)
            {
                calendar.Add(new Event(round.Split(',')));

                if (calendarSpacers.Count() == 0)
                {
                    calendarSpacers.Add(calendar[calendar.Count() - 1].RoundTitle.Length);
                    calendarSpacers.Add(calendar[calendar.Count() - 1].TrackName.Length);
                    calendarSpacers.Add(calendar[calendar.Count() - 1].Country.Length);
                }
                else
                {
                    if (calendar[calendar.Count() - 1].RoundTitle.Length > calendarSpacers[0])
                    {
                        calendarSpacers[0] = calendar[calendar.Count() - 1].RoundTitle.Length;
                    }
                    if (calendar[calendar.Count() - 1].TrackName.Length > calendarSpacers[1])
                    {
                        calendarSpacers[1] = calendar[calendar.Count() - 1].TrackName.Length;
                    }
                    if (calendar[calendar.Count() - 1].Country.Length > calendarSpacers[2])
                    {
                        calendarSpacers[2] = calendar[calendar.Count() - 1].Country.Length;
                    }
                }
            }
        }

        public void SimulateEvent()
        {
            saveFolder = Path.Combine(CommonData.ResultsFolder, seriesName, seasonNumber, String.Format("Round {0} - {1}", roundNumber, currentRound.RoundTitle));
            Directory.CreateDirectory(saveFolder);

            fileNumber = 1;

            LoadEntrants();

            // Practice
            SimulatePractice();
            SortEntrants(0, entryList.Count(), false);
            Console.WriteLine("\n\nPractice Results:");
            DisplayEntrants(0, entryList.Count(), "DSQ");
            SaveEntrants(0, entryList.Count(), "DSQ", Path.Combine(saveFolder, String.Format("{0} - Practice Results.csv", fileNumber))); fileNumber++;
            Console.ReadLine();

            QualifyingOrderDraw();
            Console.WriteLine("\nQualifying Running Order:");
            DisplayEntrants(0, entryList.Count(), "DSQ");
            SaveEntrants(0, entryList.Count(), "DSQ", Path.Combine(saveFolder, String.Format("{0} - Qualifying Running Order.csv", fileNumber))); fileNumber++;
            Console.ReadLine();

            SimulateQualifying1();
            Console.WriteLine("\nQualifying 1 Results:");
            DisplayEntrants(0, entryList.Count(), "EXC");
            SaveEntrants(0, entryList.Count(), "EXC", Path.Combine(saveFolder, String.Format("{0} - Qualifying 1 Results.csv", fileNumber))); fileNumber++;
            Console.ReadLine();
            SetRace2Grid();

            SimulateQualifying2();
            Console.WriteLine("\nQualifying 2 Results:");
            DisplayEntrants(0, q2Entries, "EXC");
            SaveEntrants(0, q2Entries, "EXC", Path.Combine(saveFolder, String.Format("{0} - Qualifying 2 Results.csv", fileNumber))); fileNumber++;
            Console.ReadLine();
            SetRace1Grid();

            Scrutineering(true);
            Console.WriteLine("\nFinal Qualifying Results:");
            DisplayEntrants(0, entryList.Count(), "EXC");
            SaveEntrants(0, entryList.Count(), "EXC", Path.Combine(saveFolder, String.Format("{0} - Final Qualifying Results.csv", fileNumber))); fileNumber++;
            Console.ReadLine();

            DropEntrants();
            SetGrid("Race 1");
            Console.WriteLine("\nRace 1 Grid:");
            DisplayEntrants(0, entryList.Count(), "DSQ");
            SaveEntrants(0, entryList.Count(), "DSQ", Path.Combine(saveFolder, String.Format("{0} - Race 1 Grid.csv", fileNumber))); fileNumber++;
            Console.ReadLine();

            SimulateRace("Race 1", currentRound.R1Distance);
            Console.ReadLine();

            SetGrid("Race 2");
            Console.WriteLine("\nRace 2 Grid:");
            DisplayEntrants(0, entryList.Count(), "DSQ");
            SaveEntrants(0, entryList.Count(), "DSQ", Path.Combine(saveFolder, String.Format("{0} - Race 2 Grid.csv", fileNumber))); fileNumber++;
            Console.ReadLine();

            SimulateRace("Race 2", currentRound.R2Distance);
            Scrutineering(false);

            Console.ReadLine();
        }

        
        // Lap Simulations

        public (int, int) SimulateLap(Entrant entrant, string session)
        {
            minStint = baseMinStint + entrant.TyreScore;
            maxStint = baseMaxStint + entrant.TyreScore;
            minIncident = baseMinIncident + entrant.EntrantDriver.CrashScore;
            maxIncident = entrant.Reliability + currentRound.IncidentModifier; // baseMaxIncident + currentRound.IncidentModifier + entrant.EntrantDriver.CrashScore;
            maxDNF = baseMaxDNF + currentRound.DNF + entrant.EntrantDriver.CrashScore;

            minTyreWear = entrant.EntrantDriver.Degredation;
            maxTyreWear = entrant.EntrantTeam.Tyres.Degredation + entrant.EntrantDriver.Degredation;

            if (session == "Practice")
            {
                maxIncident += currentRound.IncidentModifier + entrant.EntrantDriver.CrashScore;
            }

            if (session == "Qualifying")
            {
                minStint += entrant.PracticeBonus;
                maxStint += entrant.PracticeBonus;
            }

            stintScore = randomiser.Next(minStint, maxStint);
            tyreWearScore = randomiser.Next(minTyreWear, maxTyreWear) / 3;
            incidentScore = randomiser.Next(minIncident, maxIncident);

            if (incidentScore <= minIncident + (2 * entrant.EntrantDriver.CrashScore))
            {
                dnfScore = randomiser.Next(minDNF, maxDNF);

                if (dnfScore == minDNF)
                {
                    return (1, 0);
                }

                else
                {
                    stintScore -= dnfScore;

                    if (stintScore < minStint)
                    {
                        stintScore = minStint;
                    }

                    return (stintScore, tyreWearScore + (dnfScore / 3));
                }
            }

            return (stintScore, tyreWearScore);
        }

        public (int, int, int) SimulateRaceLap(Entrant entrant) // Stint , Pit , Tyre Wear
        {
            minStint = baseMinStint + entrant.TyreScore + entrant.PracticeBonus;
            maxStint = baseMaxStint + entrant.TyreScore + entrant.PracticeBonus;
            minIncident = baseMinIncident + entrant.EntrantDriver.CrashScore;
            maxIncident = baseMaxIncident + currentRound.IncidentModifier + entrant.EntrantDriver.CrashScore;
            maxDNF = baseMaxDNF + currentRound.DNF + entrant.EntrantDriver.CrashScore;

            minTyreWear = entrant.EntrantTeam.Tyres.Degredation / 2;
            maxTyreWear = entrant.EntrantTeam.Tyres.Degredation + entrant.EntrantDriver.Degredation;

            if (minTyreWear < 1)
            {
                minTyreWear = 1;
            }

            stintScore = randomiser.Next(minStint, maxStint);
            tyreWearScore = randomiser.Next(minTyreWear, maxTyreWear);// / 3;
            incidentScore = randomiser.Next(minIncident, maxIncident);
            pitScore = 0;

            if (entrant.TyreScore < randomiser.Next(entrant.EntrantTeam.Tyres.TyreScore - 40, entrant.EntrantTeam.Tyres.TyreScore - 29))
            {
                pitScore = randomiser.Next(25, 35);
                tyreWearScore -= 2;

                if (tyreWearScore <= 0)
                {
                    tyreWearScore = 1;
                }
            }

            if (incidentScore <= minIncident + (2 * entrant.EntrantDriver.CrashScore))
            {
                dnfScore = randomiser.Next(minDNF, maxDNF);

                if (dnfScore == minDNF)
                {
                    return (1, 0, 0);
                }

                else
                {
                    stintScore -= dnfScore;

                    if (stintScore < minStint)
                    {
                        stintScore = minStint;
                    }

                    return (stintScore, pitScore, tyreWearScore + (dnfScore / 2));
                }
            }

            return (stintScore, pitScore, tyreWearScore);
        }


        // Session Simulations

        public void SimulatePractice()
        {
            int totalLaps, lapsCompleted, stintScore, tyreWear, stintTotal, stintAverage;
            int newScore;

            foreach (Entrant entrant in entryList)
            {
                totalLaps = randomiser.Next(15, 21);
                lapsCompleted = 0;
                stintTotal = 0;

                while (lapsCompleted < totalLaps)
                {
                    (stintScore, tyreWear) = SimulateLap(entrant, "Practice");
                    lapsCompleted++;

                    if (stintScore == 1)
                    {
                        entrant.EntrantState = RunningState.CRASHED;

                        SetDNFReason(entrant, "Practice");
                    }

                    else
                    {
                        newScore = entrant.BackupOVR + stintScore;
                        stintTotal += stintScore;

                        if (newScore > entrant.OVR)
                        {
                            entrant.OVR = newScore;
                        }

                        entrant.TyreScore -= tyreWear;
                    }
                }

                stintAverage = stintTotal / lapsCompleted;
                entrant.PracticeBonus = stintAverage / 10;
            }
        }

        public void QualifyingOrderDraw()
        {
            string reason;
            bool doWithdraw;

            int orderPlace;
            List<int> orderPlaces = new List<int>();

            for (int i = 1; i <= entryList.Count(); i++)
            {
                orderPlaces.Add(i);
            }

            foreach (Entrant entrant in entryList)
            {
                orderPlace = randomiser.Next(0, orderPlaces.Count());
                entrant.QualifyingRunningPosition = orderPlaces[orderPlace];
                orderPlaces.RemoveAt(orderPlace);

                (doWithdraw, reason) = DoWithdrawEntrant(entrant);

                if (doWithdraw)
                {
                    entrant.EntrantState = RunningState.WITHDRAWN;
                    entrant.RetirementReason = reason;
                    entrant.OVR = entrant.BackupOVR;
                }
                else
                {
                    entrant.EntrantState = RunningState.RUNNING;

                    entrant.OVR = entrant.BackupOVR;
                    entrant.TyreScore = entrant.EntrantTeam.Tyres.TyreScore;
                }

            }

            bool swap;

            for (int i = 0; i < entryList.Count() - 1; i++)
            {
                swap = false;

                for (int j = 0; j < entryList.Count() - i - 1; j++)
                {
                    if (entryList[j].QualifyingRunningPosition > entryList[j + 1].QualifyingRunningPosition)
                    {
                        swap = true;
                        (entryList[j], entryList[j + 1]) = (entryList[j + 1], entryList[j]);
                    }
                }

                if (!swap)
                {
                    break;
                }
            }
        }

        public void SimulateQualifying1()
        {
            int stintScore, tyreWear;

            foreach (Entrant entrant in entryList)
            {
                if (entrant.EntrantState == RunningState.RUNNING)
                {
                    (stintScore, tyreWear) = SimulateLap(entrant, "Qualifying");

                    if (stintScore == 1)
                    {
                        entrant.EntrantState = RunningState.CRASHED;

                        SetDNFReason(entrant, "Qualifying");
                    }

                    else
                    {
                        entrant.OVR = entrant.BackupOVR + stintScore;
                    }
                }
            }

            SortEntrants(0, entryList.Count(), false);
        }

        public void SimulateQualifying2()
        {
            int stintScore, tyreWear;
            Entrant entrant;

            for (int i = q2Entries - 1; i >= 0; i--)
            {
                entrant = entryList[i];

                if (entrant.EntrantState == RunningState.RUNNING)
                {
                    (stintScore, tyreWear) = SimulateLap(entrant, "Qualifying");

                    if (stintScore == 1)
                    {
                        entrant.EntrantState = RunningState.CRASHED;

                        SetDNFReason(entrant, "Qualifying");
                    }

                    else
                    {
                        entrant.OVR = entrant.BackupOVR + stintScore;
                    }
                }
            }

            SortEntrants(0, q2Entries, false);
        }

        public void SimulateRace(string raceNumber, int raceLength)
        {
            string raceFolder = Path.Combine(saveFolder, raceNumber);
            Directory.CreateDirectory(raceFolder);

            int stintScore, pitScore, tyreWear, raceFileNumber = 1;

            for (int lap = 1; lap <= raceLength; lap++)
            {
                foreach (Entrant entrant in entryList)
                {
                    if (entrant.EntrantState == RunningState.RUNNING)
                    {
                        (stintScore, pitScore, tyreWear) = SimulateRaceLap(entrant);

                        if (pitScore > 0)
                        {
                            entrant.TyreScore = entrant.EntrantTeam.Tyres.TyreScore;
                            entrant.TyreLife = 100 - tyreWear;

                            entrant.TyreScore = entrant.EntrantTeam.Tyres.TyreScore - ((100 - entrant.TyreLife) / 10) * ((entrant.EntrantTeam.Tyres.Degredation + 1) / 2);
                        }

                        if (stintScore > 1)
                        {
                            entrant.OVR += stintScore - pitScore;
                            entrant.TyreLife -= tyreWear;

                            entrant.TyreScore = entrant.EntrantTeam.Tyres.TyreScore - ((100 - entrant.TyreLife) / 10) * ((entrant.EntrantTeam.Tyres.Degredation + 1) / 2);
                        }
                        else
                        {
                            entrant.EntrantState = RunningState.RETIRED;

                            SetDNFReason(entrant, "Race");
                        }
                    }
                }

                SortEntrants(0, entryList.Count(), true);

                Console.WriteLine("\n{0} - Lap {1}/{2}:", raceNumber, lap, raceLength);
                DisplayEntrants(0, entryList.Count(), "DSQ");
                SaveEntrants(0, entryList.Count(), "DSQ", Path.Combine(raceFolder, String.Format("{0} - Lap {1}.csv", raceFileNumber, lap))); raceFileNumber++;

                Console.ReadLine();
            }

            Scrutineering(false);
            Console.WriteLine("\n{0} - Race Results:", raceNumber);
            DisplayEntrants(0, entryList.Count(), "DSQ");
            SaveEntrants(0, entryList.Count(), "DSQ", Path.Combine(raceFolder, String.Format("{0} - Race Results.csv", raceFileNumber))); ;
        }


        // Set Race Grids - Qualifying

        public void SetRace1Grid()
        {
            for (int i = 1; i <= entryList.Count(); i++)
            {
                entryList[i - 1].Race1GridPosition = i;
            }
        }

        public void SetRace2Grid()
        {
            for (int i = 1; i <= entryList.Count(); i++)
            {
                entryList[i - 1].Race2GridPosition = i;
            }
        }


        // Set Race Grids - Race

        public void SetGrid(string race)
        {
            bool doWithdraw;
            string reason;

            switch (race)
            {
                case "Race 1":
                    SortRace1Grid();
                    break;
                case "Race 2":
                    SortRace2Grid();
                    break;
            }

            for (int i = 0; i < entryList.Count(); i++)
            {
                if (entryList[i].EntrantState != RunningState.WITHDRAWN)
                {
                    (doWithdraw, reason) = DoWithdrawEntrant(entryList[i]);

                    if (doWithdraw)
                    {
                        entryList[i].EntrantState = RunningState.WITHDRAWN;
                        entryList[i].RetirementReason = reason;
                        entryList[i].OVR = entryList[i].BackupOVR;
                    }
                    else
                    {
                        entryList[i].EntrantState = RunningState.RUNNING;
                        entryList[i].OVR = entryList[i].BackupOVR + ((entryList.Count() - i) * gridSpacer);
                        entryList[i].TyreScore = entryList[i].EntrantTeam.Tyres.TyreScore;
                        entryList[i].TyreLife = 100;
                    }
                }
            }

            ReorderEntrants(0, entryList.Count());
        }

        public void SortRace1Grid()
        {
            bool swap;

            for (int i = 0; i < entryList.Count() - 1; i++)
            {
                swap = false;

                for (int j = 0; j < entryList.Count() - i - 1; j++)
                {
                    if (entryList[j].Race1GridPosition > entryList[j + 1].Race1GridPosition)
                    {
                        swap = true;
                        (entryList[j], entryList[j + 1]) = (entryList[j + 1], entryList[j]);
                    }
                }

                if (!swap)
                {
                    break;
                }
            }
        }

        public void SortRace2Grid()
        {
            bool swap;

            for (int i = 0; i < entryList.Count() - 1; i++)
            {
                swap = false;

                for (int j = 0; j < entryList.Count() - i - 1; j++)
                {
                    if (entryList[j].Race2GridPosition > entryList[j + 1].Race2GridPosition)
                    {
                        swap = true;
                        (entryList[j], entryList[j + 1]) = (entryList[j + 1], entryList[j]);
                    }
                }

                if (!swap)
                {
                    break;
                }
            }
        }


        // Entrant Withdrawal / Scrutineering + Reasons

        public void Scrutineering(bool dropGridPositions)
        {
            int scrutineeringResult;

            foreach (Entrant entrant in entryList)
            {
                scrutineeringResult = randomiser.Next(1, 151); // Was previously 1-100

                if (scrutineeringResult == 1)
                {
                    entrant.EntrantState = RunningState.DISQUALIFIED;

                    if (dropGridPositions)
                    {
                        entrant.Race1GridPosition = entryList.Count();
                        entrant.Race2GridPosition = entryList.Count();
                    }

                    SetDSQReason(entrant);
                }
            }

            ReorderEntrants(0, entryList.Count());
        }

        public void DropEntrants()
        {
            if (entryList.Count() > maxEntries)
            {
                for (int i = maxEntries; i < entryList.Count(); i++)
                {
                    entryList[i].EntrantState = RunningState.DID_NOT_QUALIFY;
                }
            }

            Console.WriteLine("\nFinal Qualifying Results:");
            DisplayEntrants(0, entryList.Count(), "EXC");
            SaveEntrants(0, entryList.Count(), "EXC", Path.Combine(saveFolder, String.Format("{0} - Full Qualifying Results.csv", fileNumber))); fileNumber++;

            Console.ReadLine();

            while (entryList.Count() > maxEntries)
            {
                entryList.RemoveAt(entryList.Count() - 1);
            }
        }

        public void SetDNFReason(Entrant entrant, string session)
        {
            int reason = randomiser.Next(1, 11);
            int withdraw = randomiser.Next(1, 50);

            /*
                Practice + Qualifying
                 - Collision
                 - Crash
                 - Failure
                Race
                 - Collision
                 - Crash
                 - Suspension
                 - Engine
                 - Gearbox
                Withdrawal
                 - Damage
                 - Injury
            */

            if (session != "Race" && withdraw == 0)
            {
                entrant.EntrantState = RunningState.WITHDRAWN;
                entrant.OVR = entrant.BackupOVR;
            }

            if (reason < 3)
            {
                entrant.RetirementReason = "Collision";
            }
            else if (reason < 5)
            {
                entrant.RetirementReason = "Crash";
            }
            else if (reason < 7)
            {
                entrant.RetirementReason = "Suspension";
            }
            else if (reason < 9)
            {
                entrant.RetirementReason = "Engine";
            }
            else
            {
                entrant.RetirementReason = "Gearbox";
            }
        }

        public (bool, string) DoWithdrawEntrant(Entrant entrant)
        {
            int withdrawalChance, maxWithdrawalChance, withdrawalReason;

            bool withdraw = false;
            string reason = "";

            if (entrant.EntrantState != RunningState.RUNNING && (entrant.RetirementReason == "Collision" || entrant.RetirementReason == "Crash"))
            {
                maxWithdrawalChance = 231; // Was previously 131
            }

            else
            {
                maxWithdrawalChance = 301; // Was previously 201
            }

            withdrawalChance = randomiser.Next(1, maxWithdrawalChance);

            if (withdrawalChance == 1)
            {
                withdraw = true;

                if (entrant.RetirementReason == "Collision" || entrant.RetirementReason == "Crash")
                {
                    withdrawalReason = randomiser.Next(1, 5);

                    if (withdrawalReason == 1)
                    {
                        reason = "Illness";
                    }
                    else if (withdrawalReason == 2)
                    {
                        reason = "Injury";
                    }
                    else
                    {
                        reason = "Damage";
                    }
                }
                else
                {
                    withdrawalReason = randomiser.Next(1, 3);

                    if (withdrawalReason == 1)
                    {
                        reason = "Illness";
                    }
                    else
                    {
                        reason = "Injury";
                    }
                }
            }

            return (withdraw, reason);
        }

        public void SetDSQReason(Entrant entrant)//, string session)
        {
            int reason = randomiser.Next(1, 11);

            if (reason < 3)
            {
                entrant.RetirementReason = "Illegal Part";
            }
            else if (reason < 5)
            {
                entrant.RetirementReason = "Plank Wear";
            }
            else if (reason < 7)
            {
                entrant.RetirementReason = "Fuel Sample";
            }
            else if (reason < 9)
            {
                entrant.RetirementReason = "Wing Flex";
            }
            else
            {
                entrant.RetirementReason = "Underweight";
            }
        }


        // Sort Entrants

        public void SortEntrants(int startIndex, int endIndex, bool doReorder)
        {
            bool swap;

            for (int i = 0; i < endIndex - 1; i++)
            {
                swap = false;

                for (int j = startIndex; j < endIndex - i - 1; j++)
                {
                    if (entryList[j].OVR < entryList[j + 1].OVR)
                    {
                        swap = true;
                        (entryList[j], entryList[j + 1]) = (entryList[j + 1], entryList[j]);
                    }
                }

                if (!swap)
                {
                    break;
                }
            }

            if (doReorder)
            {
                ReorderEntrants(startIndex, endIndex);
            }
        }

        public void ReorderEntrants(int startIndex, int endIndex)
        {
            bool swap;

            for (int i = 0; i < endIndex - 1; i++)
            {
                swap = false;

                for (int j = startIndex; j < endIndex - i - 1; j++)
                {
                    if (entryList[j].EntrantState > entryList[j + 1].EntrantState)
                    {
                        swap = true;
                        (entryList[j], entryList[j + 1]) = (entryList[j + 1], entryList[j]);
                    }
                }

                if (!swap)
                {
                    break;
                }
            }
        }


        // Display, Save

        public void DisplayEntrants(int startIndex, int endIndex, string dsqString)
        {
            string positionString = "";
            Entrant currentEntrant;

            for (int i = startIndex; i < endIndex; i++)
            {
                currentEntrant = entryList[i];

                switch (currentEntrant.EntrantState)
                {
                    case RunningState.RUNNING:
                        positionString = "P" + (i + 1);
                        break;
                    case RunningState.CRASHED:
                        positionString = "INC";
                        break;
                    case RunningState.RETIRED:
                        positionString = "DNF";
                        break;
                    case RunningState.NOT_STARTING:
                        positionString = "DNS";
                        break;
                    case RunningState.DISQUALIFIED:
                        positionString = dsqString;
                        break;
                    case RunningState.WITHDRAWN:
                        positionString = "WDN";
                        break;
                    case RunningState.DID_NOT_QUALIFY:
                        positionString = "DNQ";
                        break;
                }

                Console.WriteLine("{0} - {1}", positionString.PadRight(3, ' '), currentEntrant.GetOutputString(spacers));
            }
        }

        public void SaveEntrants(int startIndex, int endIndex, string dsqString, string fileName)
        {
            string saveString = "", positionString = "";
            Entrant currentEntrant;

            for (int i = startIndex; i < endIndex; i++)
            {
                currentEntrant = entryList[i];

                switch (currentEntrant.EntrantState)
                {
                    case RunningState.RUNNING:
                        positionString = "P" + (i + 1);
                        break;
                    case RunningState.CRASHED:
                        positionString = "INC";
                        break;
                    case RunningState.RETIRED:
                        positionString = "DNF";
                        break;
                    case RunningState.NOT_STARTING:
                        positionString = "DNS";
                        break;
                    case RunningState.DISQUALIFIED:
                        positionString = dsqString;
                        break;
                    case RunningState.WITHDRAWN:
                        positionString = "WDN";
                        break;
                    case RunningState.DID_NOT_QUALIFY:
                        positionString = "DNQ";
                        break;
                }

                saveString += String.Format("{0},{1}", positionString, currentEntrant.GetResultsString());

                if (i != startIndex - 2)
                {
                    saveString += "\n";
                }
            }

            try
            {
                File.WriteAllText(fileName, saveString);
            }
            catch (Exception exception)
            {
                Console.WriteLine("File Write Error");
                Console.WriteLine(exception);
                SaveEntrants(startIndex, endIndex, dsqString, fileName);
            }
        }

    }
}
