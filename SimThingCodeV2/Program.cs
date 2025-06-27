using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimThingCodeV2
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonData.Setup();
            // DisplayDetails();
            // Console.ReadLine();
            Simulator simulator = new Simulator();
            simulator.Start();
        }

        public static void DisplayDetails()
        {
            Console.WriteLine("Main Folder: {0}", CommonData.MainFolder);
            Console.WriteLine("Setup Folder: {0}", CommonData.SetupFolder);
            Console.WriteLine("Results Folder: {0}", CommonData.ResultsFolder);
            Console.WriteLine("\nChassis File: {0}", CommonData.ChassisFile);
            Console.WriteLine("Engine File: {0}", CommonData.EngineFile);
            Console.WriteLine("Tyre File: {0}", CommonData.TyreFile);
            Console.WriteLine("Driver File: {0}", CommonData.DriverFile);
            Console.WriteLine("Team File: {0}", CommonData.TeamFile);
            Console.WriteLine("\nEntry List File: {0}", CommonData.EntryListFile);
            Console.WriteLine("Calendar File: {0}", CommonData.CalendarFile);

            Console.WriteLine("\n\nChassis Details");
            foreach (ChassisSupplier Chassis in CommonData.ChassisList) { Console.WriteLine(Chassis.GetDisplayString()); }

            Console.WriteLine("\n\nEngine Details");
            foreach (EngineSupplier Engine in CommonData.EngineList) { Console.WriteLine(Engine.GetDisplayString()); }

            Console.WriteLine("\n\nTyresDetails");
            foreach (TyreSupplier Tyre in CommonData.TyreList) { Console.WriteLine(Tyre.GetDisplayString()); }

            Console.WriteLine("\n\nDriver Details");
            foreach (Driver driver in CommonData.DriverList) { Console.WriteLine(driver.GetDisplayString()); }

            Console.WriteLine("\n\nTeam Details");
            foreach (Team team in CommonData.TeamList) { Console.WriteLine(team.GetDisplayString()); }
        }
    }
}
