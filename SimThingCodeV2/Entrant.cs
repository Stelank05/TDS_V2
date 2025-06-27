using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimThingCodeV2
{
    public enum RunningState
    {
        RUNNING = 1,
        CRASHED = 2,
        RETIRED = 3,
        NOT_STARTING = 4,
        DISQUALIFIED = 5,
        WITHDRAWN = 6,
        DID_NOT_QUALIFY = 7
    }

    public class Entrant
    {
        // Fields

        private string idCode, carNo, dnfReason = "";
        private int mainOVR, backupOVR, reliability, practiceBonus, tyreScore, tyreLife;
        private int qualifyingRunningPosition, race1Grid, race2Grid;

        private RunningState runningState;

        private Driver driver;
        private Team team;


        // Properties

        public string ID { get { return idCode; } }

        public string CarNumber { get { return carNo; } }

        public string RetirementReason
        {
            get { return dnfReason; }
            set { dnfReason = value; }
        }

        public int OVR
        {
            get { return mainOVR; }
            set { mainOVR = value; }
        }

        public int BackupOVR { get { return backupOVR; } }

        public int Reliability { get { return reliability; } }

        public int PracticeBonus
        {
            get { return practiceBonus; }
            set { practiceBonus = value; }
        }

        public int TyreScore
        {
            get { return tyreScore; }
            set { tyreScore = value; }
        }

        public int TyreLife
        {
            get { return tyreLife; }
            set { tyreLife = value; }
        }

        public int QualifyingRunningPosition
        {
            get { return qualifyingRunningPosition; }
            set { qualifyingRunningPosition = value; }
        }

        public int Race1GridPosition
        {
            get { return race1Grid; }
            set { race1Grid = value; }
        }

        public int Race2GridPosition
        {
            get { return race2Grid; }
            set { race2Grid = value; }
        }

        public RunningState EntrantState
        {
            get { return runningState; }
            set { runningState = value; }
        }

        public Driver EntrantDriver { get { return driver; } }

        public Team EntrantTeam { get { return team; } }


        // Constructors

        public Entrant(string[] entrantData)
        {
            idCode = entrantData[0];

            carNo = entrantData[1];

            driver = CommonData.GetDriver(entrantData[2]);
            team = CommonData.GetTeam(entrantData[3]);

            mainOVR = driver.DriverScore + team.TeamScore + team.AeroBonus + team.MechBonus + team.Chassis.ChassisOVR + team.Engine.EngineOVR + team.Tyres.TyreScore;
            backupOVR = mainOVR;

            reliability = driver.CrashScore + team.Reliability + team.Chassis.Reliability + team.Engine.Reliablity;

            tyreScore = team.Tyres.TyreScore;

            runningState = RunningState.RUNNING;
        }

        public Entrant() { }


        // Other Functions

        public string GetDisplayString()
        {
            return String.Format("{0} - {1} - {2} - {3} - {4}", idCode, carNo, driver.DriverName, team.TeamName, mainOVR);
        }

        public string GetOutputString(List<int> spacers)
        {
            string returnString = String.Format("{0} - {1} - {2} - {3} - {4} - {5} - {6}", driver.DriverName.PadRight(spacers[0], ' '), carNo.PadRight(spacers[1], ' '), team.TeamCode.PadRight(spacers[2], ' '), team.Chassis.SupplierCode, team.Engine.SupplierCode, team.Tyres.SupplierCode, Convert.ToString(tyreScore).PadRight(spacers[3], ' '));

            if (runningState == RunningState.RUNNING)
            {
                returnString += " - " + mainOVR;
            }

            else
            {
                returnString += " - " + RetirementReason;
            }

            return returnString;
        }

        public string GetResultsString()
        {
            if (runningState == RunningState.RUNNING)
            {
                return String.Format("{0},{1},{2},{3},{4},{5},{6}", driver.DriverName, carNo, team.TeamName, team.Chassis.SupplierCode, team.Engine.SupplierCode, team.Tyres.SupplierCode, mainOVR);
            }

            return String.Format("{0},{1},{2},{3},{4},{5},{6}", driver.DriverName, carNo, team.TeamName, team.Chassis.SupplierCode, team.Engine.SupplierCode, team.Tyres.SupplierCode, dnfReason);
        }
    }
}
