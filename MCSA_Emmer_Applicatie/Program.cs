using System;
using System.Collections.Generic;

namespace MCSA_Emmer_Applicatie
{
    internal class Program
    {
        //private static Storage storage = new Storage();
        private static Scenario scenario = new Scenario();
        //private static List<Container> list = new List<Container>();

        private static void Main(string[] args)
        {
            scenario.ScenarioOne();
            //storage.FillList();
            //list = storage.GetContainerList();
            //storage.Print();
            //typeof(Storage).GetMethod("FillList", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(new Storage(), null);
            Console.ReadLine();
        }
    }
}