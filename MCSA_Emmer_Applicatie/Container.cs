using System;
using System.Threading;
using System.Threading.Tasks;

namespace MCSA_Emmer_Applicatie
{
    public abstract class Container
    {
        #region Properties
        public int Content { get; protected set; }
        public int ContentCurrent { get; set; }
        #endregion

        #region Constructors
        public Container()
        {

        }

        public Container(int content, int contentCurrent)
        {
            Content = content;
            ContentCurrent = contentCurrent;
        }
        #endregion

        #region Delegates/ events
        public delegate void ContainerEventHandler(object sender, ContainerEventArgs e);
        public event ContainerEventHandler ContainerFilled;
        public event ContainerEventHandler ContainerOverflow;
        #endregion

        #region Methods
        public abstract Task<int> FillContainer(int input);

        public abstract bool CheckContainerIfFull();

        public abstract int EmptyContainer(int input);

        public abstract bool CheckContainerIfEmpty();

        public abstract override string ToString();

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
        #endregion
    }
}