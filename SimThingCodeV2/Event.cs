using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimThingCodeV2
{
    class Event
    {
        // Fields

        private string idCode, roundTitle, trackName, country;
        private int race1Distance, race2Distance;
        private int incidentModifier, dnfModifier;


        // Properties

        public string ID { get { return idCode; } }

        public string RoundTitle { get { return roundTitle; } }

        public string TrackName { get { return trackName; } }

        public string Country { get { return country; } }

        public int R1Distance { get { return race1Distance; } }

        public int R2Distance { get { return race2Distance; } }

        public int IncidentModifier { get { return incidentModifier; } }

        public int DNF { get { return dnfModifier; } }


        // Constructors

        public Event(string[] eventDetails)
        {
            idCode = eventDetails[0];

            roundTitle = eventDetails[1];
            trackName = eventDetails[2];
            country = eventDetails[3];

            race1Distance = Convert.ToInt32(eventDetails[4]);
            race2Distance = Convert.ToInt32(eventDetails[5]);

            incidentModifier = Convert.ToInt32(eventDetails[6]);
            dnfModifier = Convert.ToInt32(eventDetails[7]);
        }

        public Event() { }


        // Other Functions

        public string GetDisplayString()
        {
            return String.Format("{0} - {1} - {2} - {3} - {4} - {5} - {6} - {7}", idCode, roundTitle, trackName, country, race1Distance, race2Distance, incidentModifier, dnfModifier);
        }
    }
}
