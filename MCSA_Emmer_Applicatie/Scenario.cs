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
            Bucket bucket1 = new Bucket(10, 9, true, true);
            // bucket 1 Listens for containerfull event
            bucket1.ContainerFilled += OnContainerFilled;
            //ContainerFilled += new ContainerEventHandler(HandleProtectedEvent);
            Console.WriteLine(bucket1.ToString());
            //bucket1.FillContainer(12);
            //Console.WriteLine(bucket1.ToString());
            //bucket1.EmptyContainer(13);
            //Console.WriteLine(bucket1.ToString());
            //bucket1.EmptyContainer(2);
            //Console.WriteLine(bucket1.ToString());
            Bucket bucket2 = new Bucket(10, 3, true, true);
            // bucket 2 Listens for containerOverflow event
            bucket2.ContainerOverflow += OnContainerOverflow;
            Console.WriteLine(bucket2.ToString());
            bucket1.TransferBucketContent(8, bucket1, bucket2);
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

        public void OnContainerFilled(object sender, ContainerEventArgs e)
        {
            if (sender is Bucket bucket)
            {
                Console.WriteLine($"{bucket.ToString()} is full");
            }

            #region Example code
            //Bucket bucket = sender as Bucket;
            //if (bucket != null)
            //{
            //    Console.WriteLine($"{bucket.ToString()} is full");
            //}
            #endregion
        }

        public void OnContainerOverflow(object sender, ContainerEventArgs e)
        {
            if (sender is Bucket bucket)
            {
                Console.WriteLine($"{bucket.ToString()} is overflowing, {e.Overflow}L");
            }
        }
    }
}
