using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSA_Emmer_Applicatie
{
    class Scenario
    {
        internal void ScenarioOne()
        {
            #region exercise 1
            // min value bucket size
            Bucket bucket1 = new Bucket(9, 9, true, true, false);
            Console.WriteLine(bucket1.ToString());
            // checks input neg digits
            bucket1.FillContainer(-1);
            Console.WriteLine(bucket1.ToString());
            // checks input negative digits
            bucket1.EmptyContainer(-1);
            Console.WriteLine(bucket1.ToString());
            // input overflow
            bucket1.EmptyContainer(9);
            Console.WriteLine(bucket1.ToString());
            // empty bucket partially
            bucket1.EmptyContainer(4);
            Console.WriteLine(bucket1.ToString());
            // empty bucket fully
            bucket1.EmptyContainer(5);
            Console.WriteLine(bucket1.ToString());
            // raises full bucket warning
            bucket1.FillContainer(10);
            Console.WriteLine(bucket1.ToString());

                #region exercise 1 & 4
                // Transfers bucket1 content (8L) to bucket2 (3L)
                // raises overlow warning + amount spilled
                Bucket bucket2 = new Bucket(10, 3, true, true, false);
                Console.WriteLine(bucket2.ToString());
                bucket1.TransferBucketContent(8, bucket2);
                #endregion
            #endregion

            #region exercise 2/3
            // Only buckets can transfer content
            Oil_barrel oilBarrel1 = new Oil_barrel();
            Console.WriteLine(oilBarrel1.ToString());
            Bucket bucket3 = new Bucket();
            Console.WriteLine(bucket3.ToString());
            bucket3.TransferBucketContent(8, oilBarrel1);
            Console.WriteLine(bucket1.ToString());
            Console.WriteLine(bucket2.ToString());
            #endregion

            #region exercise 3
            // Creates new rain barrel
            Rain_barrel rainBarrel1 = new Rain_barrel(81, 40);
            Console.WriteLine(rainBarrel1.ToString());
            #endregion

            #region exercise 5 & 6
            // stops overflowing 
            Bucket bucket4 = new Bucket(10, 10, true, true, true);
            Console.WriteLine(bucket4.ToString());
            bucket4.FillContainer(2);
            #endregion

            //Console.WriteLine("Scenario 1 ended");
        }

        private void ScenarioTwo()
        {
        }

        private void ScenarioThree()
        {
        }

        #region Example code
        //private void HandleProtectedEvent(object sender, ContainerEventArgs e)
        //{
        //    Bucket bucket = (Bucket)sender;
        //    Console.WriteLine($"{bucket.ToString()} is full");
        //}
        #endregion

        #region Example code
        //public void OnContainerFilled(object sender, ContainerEventArgs e)
        //{
        //    if (sender is Bucket bucket)
        //    {
        //        Console.WriteLine($"{bucket.ToString()} is full");
        //    }

        //    #region Example code
        //    //Bucket bucket = sender as Bucket;
        //    //if (bucket != null)
        //    //{
        //    //    Console.WriteLine($"{bucket.ToString()} is full");
        //    //}
        //    #endregion
        //}
        #endregion

    }
}
