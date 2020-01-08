using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSA_Emmer_Applicatie
{
    public class ContainerEventArgs : EventArgs
    {
        public ContainerEventArgs(int over)
        {
            Overflow = over;
        }
        public int Overflow { get; } = 0;

        #region Example code
        //public ContainerEventArgs(int over)
        //{
        //    //Overflow = over;
        //}

        //public int Overflow { get; } = 0;
        #endregion

    }
}
