using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimThingCodeV2
{
    public class EngineSupplier
    {
        // Fields

        private string idCode, supplierName, supplierCode;
        private int engineScore, hybridScore, engineOVR, reliability;


        // Properties

        public string ID { get { return supplierName; } }

        public string SupplierName { get { return supplierName; } }

        public string SupplierCode { get { return supplierCode; } }

        public int EngineScore { get { return engineScore; } }

        public int HybridScore { get { return hybridScore; } }

        public int EngineOVR { get { return engineOVR; } }

        public int Reliablity { get { return reliability; } }


        // Constructors

        public EngineSupplier(string[] supplierDetails)
        {
            idCode = supplierDetails[0];

            supplierName = supplierDetails[1];
            supplierCode = supplierDetails[2];

            engineScore = Convert.ToInt32(supplierDetails[3]);
            hybridScore = Convert.ToInt32(supplierDetails[4]);

            engineOVR = engineScore + hybridScore;

            reliability = Convert.ToInt32(supplierDetails[5]);
        }

        public EngineSupplier () { }


        // Other Functions

        public string GetDisplayString()
        {
            return String.Format("{0} - {1} - {2} - {3} - {4} - {5}", idCode, supplierName, supplierCode, engineScore, hybridScore, reliability);
        }

        public void DisplaySupplierDetails()
        {
            Console.WriteLine("Supplier ID:       {0}", idCode);
            Console.WriteLine("Supplier Name:     {0}", supplierName);
            Console.WriteLine("Supplier Code:     {0}", supplierCode);
            Console.WriteLine("Engine Score:      {0}", engineScore);
            Console.WriteLine("Hybrid Score:      {0}", hybridScore);
            Console.WriteLine("Reliability Score: {0}", reliability);
        }
    }
}
