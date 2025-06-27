using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimThingCodeV2
{
    public class TyreSupplier
    {
        // Fields

        private string idCode, supplierName, supplierCode;
        private int tyreScore, degScore;


        // Properties

        public string ID { get { return supplierName; } }

        public string SupplierName { get { return supplierName; } }

        public string SupplierCode { get { return supplierCode; } }

        public int TyreScore { get { return tyreScore; } }

        public int Degredation { get { return degScore; } }


        // Constructors

        public TyreSupplier(string[] supplierDetails)
        {
            idCode = supplierDetails[0];

            supplierName = supplierDetails[1];
            supplierCode = supplierDetails[2];
            
            tyreScore = Convert.ToInt32(supplierDetails[3]);
            degScore = Convert.ToInt32(supplierDetails[4]);
        }

        public TyreSupplier() { }


        // Other Functions

        public string GetDisplayString()
        {
            return String.Format("{0} - {1} - {2} - {3} - {4}", idCode, supplierName, supplierCode, tyreScore, degScore);
        }

        public void DisplaySupplierDetails()
        {
            Console.WriteLine("Supplier ID:    {0}", idCode);
            Console.WriteLine("Supplier Name:  {0}", supplierName);
            Console.WriteLine("Supplier Code:  {0}", supplierCode);
            Console.WriteLine("Tyre Score: {0}", tyreScore);
        }
    }
}
