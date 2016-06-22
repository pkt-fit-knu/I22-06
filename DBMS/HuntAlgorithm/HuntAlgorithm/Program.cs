using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
namespace HuntAlgorithm
{
    [DelimitedRecord(",")]
    public class DataCheatDecision
    {
        //This class is created for mapping csv file with  attributes: Tid,Refund,MaritalStatus,TaxableIncome,Cheat

        public int Tid;
        public string Refund;
        public string MaritalStatus;
        public double TaxableIncome;
        public string Cheat;
    }
    class Program
    {
        static void Main(string[] args)
        {
            // fetching data from txt and mapping it to DataCheatDecision class using FileHelpers library 
            var engine = new FileHelperEngine<DataCheatDecision>();
            var records = engine.ReadFile("dataCheatDecision.txt");
            IEnumerable<DataCheatDecision> query = records.OrderBy(dataCheatDecision =>dataCheatDecision.TaxableIncome);
            foreach (var record in query)
            {
                Console.WriteLine(record.Tid+" "+ record.Refund+" "+record.MaritalStatus+" "+record.TaxableIncome+" "+record.Cheat);
            }

            List<string> a = new List<string>();
            a.Add("a");
            a.Add("abb");
            Console.WriteLine(a.Contains());
            Console.ReadLine();
        }
    }
}
