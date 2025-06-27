using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimThingCodeV2
{
    public class ChassisSupplier
    {
        // Fields

        private string idCode, supplierName, supplierCode;
        private int frontWing, rearWing, floor, sidepods;
        private int chassis, suspension, chassisOVR, reliability;


        // Properties

        public string ID { get { return supplierName; } }

        public string SupplierName { get { return supplierName; } }

        public string SupplierCode { get { return supplierCode; } }

        public int FrontWing { get { return frontWing; } }

        public int RearWing { get { return rearWing; } }

        public int Floor { get { return floor; } }

        public int Sidepods { get { return sidepods; } }

        public int Chassis { get { return chassis; } }

        public int Suspension { get { return suspension; } }

        public int ChassisOVR { get { return chassisOVR; } }

        public int Reliability { get { return reliability; } }


        // Constructors

        public ChassisSupplier(string[] supplierDetails)
        {
            idCode = supplierDetails[0];

            supplierName = supplierDetails[1];
            supplierCode = supplierDetails[2];

            frontWing = Convert.ToInt32(supplierDetails[3]);
            rearWing = Convert.ToInt32(supplierDetails[4]);
            floor = Convert.ToInt32(supplierDetails[5]);
            sidepods = Convert.ToInt32(supplierDetails[6]);
            
            chassis = Convert.ToInt32(supplierDetails[7]);
            suspension = Convert.ToInt32(supplierDetails[8]);

            chassisOVR = frontWing + rearWing + floor + sidepods + chassis + suspension;

            reliability = Convert.ToInt32(supplierDetails[9]);
        }

        public ChassisSupplier() { }


        // Other Functions

        public string GetDisplayString()
        {
            return String.Format("{0} - {1} - {2} - {3} - {4} - {5} - {6} - {7} - {8} - {9}", idCode, supplierName, supplierCode, frontWing, rearWing, floor, sidepods, chassis, suspension, reliability);
        }

        public void DisplaySupplierDetails()
        {
            Console.WriteLine("Supplier ID:       {0}", idCode);
            Console.WriteLine("Supplier Name:     {0}", supplierName);
            Console.WriteLine("Supplier Code:     {0}", supplierCode);
            Console.WriteLine("Front Wing Score:  {0}", frontWing);
            Console.WriteLine("Rear Wing Score:   {0}", rearWing);
            Console.WriteLine("Floor Score:       {0}", floor);
            Console.WriteLine("Sidepods Score:    {0}", sidepods);
            Console.WriteLine("Chassis Score:     {0}", chassis);
            Console.WriteLine("Suspension Score:  {0}", suspension);
            Console.WriteLine("Reliability Score: {0}", reliability);
        }
    }
}
