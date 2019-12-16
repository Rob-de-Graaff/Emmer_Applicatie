using System;

namespace MCSA_Emmer_Applicatie
{
    internal class Bucket : Container
    {
        public Bucket()
        {
            this.Content = 12;
            this.ContentMin = 10;
            this.ContentCurrent = 0;
        }

        public Bucket(int content, int contentMin, int contentCurrent) : base(content, contentMin, contentCurrent)
        {
            this.Content = content;
            this.ContentMin = contentMin;
            this.ContentCurrent = contentCurrent;
        }

        public override int EmptyBucket(int input, int currentContent, bool partially)
        {
            int result = 0;
            if (partially)
            {
                result = 0 > currentContent - input ? 0 : currentContent - input;
            }
            return result;
        }

        public override int FillBucket(int input, bool warning)
        {
            throw new NotImplementedException();
        }
    }
}