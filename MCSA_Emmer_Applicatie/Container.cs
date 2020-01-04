using System;
using System.Threading;
using System.Threading.Tasks;

namespace MCSA_Emmer_Applicatie
{
    public abstract class Container
    {
        public int Content { get; protected set; }
        public int ContentCurrent { get; set; }

        public Container()
        {
        }

        public Container(int content, int contentCurrent)
        {
            Content = content;
            ContentCurrent = contentCurrent;
        }

        public abstract Task<int> FillContainer(int input, CancellationToken cancellationToken);

        public abstract bool CheckContainerIfFull();

        public abstract int EmptyContainer(int input);

        public abstract bool CheckContainerIfEmpty();

        public abstract override string ToString();

        public delegate void ContainerEventHandler(object sender, ContainerEventArgs e);
        public event ContainerEventHandler ContainerFilled;
        public event ContainerEventHandler ContainerOverflow;

        public virtual void OnContainerFilled(int over)
        {
            #region Example code
            //if (onContainerFilled != null)
            //{
            //    onContainerFilled(this, new ContainerEventArgs());
            //}
            #endregion

            //does not invoke ContainerEventArgs() default constructor
            ContainerFilled?.Invoke(this, new ContainerEventArgs(over));
        }

        public virtual void OnContainerOverflow(int over)
        {
            ContainerOverflow?.Invoke(this, new ContainerEventArgs(over));
        }
    }
}