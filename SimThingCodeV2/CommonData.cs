using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimThingCodeV2
{
    public static class CommonData
    {
        // Fields

        private static string mainFolder, setupFolder, resultsFolder;
        private static string chassisFile, engineFile, tyreFile, teamFile, driverFile;
        private static string entryListFile, calendarFile, seriesDetailsFile;

        private static List<ChassisSupplier> chassisList;
        private static List<EngineSupplier> engineList;
        private static List<TyreSupplier> tyreList;

        private static List<Driver> driverList;
        private static List<Team> teamList;


        // Properties

        public static string MainFolder { get { return mainFolder; } }

        public static string SetupFolder { get { return setupFolder; } }

        public static string ResultsFolder { get { return resultsFolder; } }

        public static string ChassisFile { get { return chassisFile; } }

        public static string EngineFile { get { return engineFile; } }

        public static string TyreFile { get { return tyreFile; } }

        public static string TeamFile { get { return teamFile; } }

        public static string DriverFile { get { return driverFile; } }

        public static string EntryListFile { get { return entryListFile; } }

        public static string CalendarFile { get { return calendarFile; } }

        public static string SeriesDetailsFile { get { return seriesDetailsFile; } }

        public static List<ChassisSupplier> ChassisList { get { return chassisList; } }

        public static List<EngineSupplier> EngineList { get { return engineList; } }

        public static List<TyreSupplier> TyreList { get { return tyreList; } }

        public static List<Driver> DriverList { get { return driverList; } }

        public static List<Team> TeamList { get {return teamList;} }


        // Setup Functions

        public static void Setup()
        {
            LoadFileData();

            LoadChassis();
            LoadEngines();
            LoadTyres();
            LoadDrivers();
            LoadTeams();
        }

        private static void LoadFileData()
        {
            mainFolder = Directory.GetCurrentDirectory().Replace("\\SimThingCodeV2\\bin\\Debug", "");
            setupFolder = Path.Combine(mainFolder, "V2 Setup Folder");
            resultsFolder = Path.Combine(mainFolder, "Results");

            chassisFile = Path.Combine(setupFolder, "Components\\Chassis.csv");
            engineFile = Path.Combine(setupFolder, "Components\\Engine.csv");
            tyreFile = Path.Combine(setupFolder, "Components\\Tyres.csv");
            teamFile = Path.Combine(setupFolder, "Components\\Teams.csv");
            driverFile = Path.Combine(setupFolder, "Components\\Drivers.csv");

            entryListFile = Path.Combine(setupFolder, "Series\\Entry List.csv");
            calendarFile = Path.Combine(setupFolder, "Series\\Calendar.csv");
            seriesDetailsFile = Path.Combine(setupFolder, "Series\\Season Details.csv");
        }

        private static void LoadChassis()
        {
            chassisList = new List<ChassisSupplier>();

            List<string> chassisData = ReadFile(chassisFile);

            foreach (string data in chassisData)
            {
                chassisList.Add(new ChassisSupplier(data.Split(',')));
            }
        }

        private static void LoadEngines()
        {
            engineList = new List<EngineSupplier>();

            List<string> engineData = ReadFile(engineFile);

            foreach (string data in engineData)
            {
                engineList.Add(new EngineSupplier(data.Split(',')));
            }
        }

        public static void LoadTyres()
        {
            tyreList = new List<TyreSupplier>();

            List<string> tyreData = ReadFile(tyreFile);

            foreach (string data in tyreData)
            {
                tyreList.Add(new TyreSupplier(data.Split(',')));
            }
        }

        public static void LoadDrivers()
        {
            driverList = new List<Driver>();

            List<string> driverData = ReadFile(driverFile);

            foreach (string data in driverData)
            {
                driverList.Add(new Driver(data.Split(',')));
            }
        }

        public static void LoadTeams()
        {
            teamList = new List<Team>();

            List<string> teamData = ReadFile(teamFile);

            foreach (string data in teamData)
            {
                teamList.Add(new Team(data.Split(',')));
            }
        }


        // Other Functions

        public static ChassisSupplier GetChassis(string getChassis)
        {
            foreach (ChassisSupplier component in chassisList)
            {
                if (component.ID == getChassis) { return component; }
            }

            return new ChassisSupplier();
        }

        public static EngineSupplier GetEngine(string getEngine)
        {
            foreach (EngineSupplier component in engineList)
            {
                if (component.ID == getEngine) { return component; }
            }

            return new EngineSupplier();
        }

        public static TyreSupplier GetTyres(string getTyre)
        {
            foreach (TyreSupplier component in tyreList)
            {
                if (component.ID == getTyre) { return component; }
            }

            return new TyreSupplier();
        }

        public static Driver GetDriver(string getDriver)
        {
            foreach (Driver component in driverList)
            {
                if (component.ID == getDriver) { return component; }
            }

            return new Driver();
        }

        public static Team GetTeam(string getTeam)
        {
            foreach (Team component in teamList)
            {
                if (component.ID == getTeam) { return component; }
            }

            return new Team();
        }


        // File Functions

        public static List<string> ReadFile(string filePath)
        {
            List<string> returnList = new List<string>();

            int i = 0;

            try
            {
                string[] fileData = File.ReadAllLines(filePath);

                foreach (string data in fileData)
                {
                    if (i != 0) { returnList.Add(data); }
                    else { i++; }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("File Read Error");
                Console.WriteLine(exception.ToString());
                Console.ReadLine();
            }

            return returnList;
        }
    }
}
