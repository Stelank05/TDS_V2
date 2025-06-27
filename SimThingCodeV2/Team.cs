using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimThingCodeV2
{
    public class Team
    {
        // Fields
        private string idCode, teamName, teamCode;
        private int teamScore, aeroBonus, mechBonus, teamReliability;

        private ChassisSupplier chassis;
        private EngineSupplier engine;
        private TyreSupplier tyres;

        
        // Properties

        public string ID { get { return idCode; } }

        public string TeamName { get { return teamName; } }

        public string TeamCode { get { return teamCode; } }

        public int TeamScore { get { return teamScore; } }

        public int AeroBonus { get { return aeroBonus; } }

        public int MechBonus { get { return mechBonus; } }

        public int Reliability { get { return teamReliability; } }

        public ChassisSupplier Chassis { get { return chassis; } }

        public EngineSupplier Engine { get { return engine; } }

        public TyreSupplier Tyres { get { return tyres; } }


        // Constructors

        public Team(string[] teamDetails)
        {
            idCode = teamDetails[0];

            teamName = teamDetails[1];
            teamCode = teamDetails[2];

            teamScore = Convert.ToInt32(teamDetails[3]);
            aeroBonus = Convert.ToInt32(teamDetails[4]);
            mechBonus = Convert.ToInt32(teamDetails[5]);
            teamReliability = Convert.ToInt32(teamDetails[6]);

            chassis = CommonData.GetChassis(teamDetails[7]);
            engine = CommonData.GetEngine(teamDetails[8]);
            tyres = CommonData.GetTyres(teamDetails[9]);
        }

        public Team() { }


        // Other Functions

        public string GetDisplayString()
        {
            return String.Format("{0} - {1} - {2} - {3} - {4} - {5} - {6} - {7} - {8}", idCode, teamName, teamCode, teamScore, aeroBonus, teamReliability, chassis.SupplierName, engine.SupplierName, tyres.SupplierName);
        }
    }
}
