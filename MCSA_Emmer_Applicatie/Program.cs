using System;
using System.Collections.Generic;

namespace MCSA_Emmer_Applicatie
{
    internal class Program
    {
        private static Storage storage = new Storage();
        private static List<Container> list = new List<Container>();

        private static void Main(string[] args)
        {
            storage.FillList();
            list = storage.GetContainerList();
            foreach (var container in list)
            {
                Console.WriteLine($"size:{container.Content}, min:{container.ContentMin}, Content:{container.ContentCurrent}");
            }
            foreach (var container in list)
            {
                container.ContentCurrent = container.EmptyBucket(10, container.ContentCurrent, true);
            }
            //typeof(Storage).GetMethod("FillList", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(new Storage(), null);
            Console.ReadLine();
        }
    }
}