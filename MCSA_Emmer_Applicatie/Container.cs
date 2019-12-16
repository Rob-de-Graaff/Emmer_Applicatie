using System;

namespace MCSA_Emmer_Applicatie
{
    public abstract class Container
    {
        protected int content;
        protected int contentMin;
        protected int contentCurrent;

        public int Content { get; set; }
        public int ContentMin { get; protected set; }
        public int ContentCurrent { get; set; }

        public Container()
        {
        }

        public Container(int content, int contentMin, int contentCurrent)
        {
            this.Content = content;
            this.ContentMin = contentMin;
            this.ContentCurrent = contentCurrent;
        }

        public abstract int FillBucket(int input, bool warning);

        public abstract int EmptyBucket(int input, int currentContent, bool partially);

        public event EventHandler BucketFilled;

        protected virtual void OnBucketFilled()
        {
            // BucketFilled?.Invoke(this, new EventArgs());
            if (BucketFilled != null)
            {
                BucketFilled(this, new EventArgs());
            }
        }
    }
}