using System;
using System.Collections.Generic;

namespace MCSA_Emmer_Applicatie
{
    internal class Storage
    {
        private Container container;
        private List<Container> containerList = new List<Container>();
        //private List<Bucket> bucketList = new List<Bucket>();
        private const int minContent = 10;
        private const int maxContent = 15;
        private Random randomNumber;

        public void FillList()
        {
            randomNumber = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < 5; i++)
            {
                if (i % 3 == 0)
                {
                    container = new Bucket();
                }
                else
                {
                    int Content = GetRandomNumber(minContent, maxContent);
                    int ContentMin = minContent;
                    int ContentCurrent = GetRandomNumber(minContent, Content);
                    container = new Bucket(Content, ContentMin, true, true, ContentCurrent);
                }
                containerList.Add(container);
                //bucketList.Add((Bucket)container);
            }
        }

        private int GetRandomNumber(int min, int max)
        {
            return randomNumber.Next(min, max);
        }

        public List<Container> GetContainerList()
        {
            return containerList;
        }

        public void Print()
        {
            foreach (var container in containerList)
            {
                Console.WriteLine(container.ToString());
            }
        }
    }
}