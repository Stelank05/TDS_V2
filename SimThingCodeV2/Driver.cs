using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimThingCodeV2
{
    public class Driver
    {
        // Fields

        private string idCode, driverName;
        private int driverScore, crashScore, degScore;


        // Properties
        
        public string ID { get { return driverName; } }

        public string DriverName { get { return driverName; } }

        public int DriverScore { get { return driverScore; } }

        public int CrashScore { get { return crashScore; } }

        public int Degredation { get { return degScore; } }


        // Constructors

        public Driver(string[] driverDetails)
        {
            idCode = driverDetails[0];

            driverName = driverDetails[1];

            driverScore = Convert.ToInt32(driverDetails[2]);
            crashScore = Convert.ToInt32(driverDetails[3]);
            degScore = Convert.ToInt32(driverDetails[4]);
        }

        public Driver() {  }


        // Other Functions

        public string GetDisplayString()
        {
            return String.Format("{0} - {1} - {2} - {3} - {4}", idCode, driverName, driverScore, crashScore, degScore);
        }
    }
}
