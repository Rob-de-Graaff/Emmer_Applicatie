using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCSA_Emmer_Applicatie
{
    class Oil_barrel : Container
    {
        private const int ContentValue = 159;

        public Oil_barrel()
        {
            Content = ContentValue;
            ContentCurrent = 0;
        }

        public Oil_barrel(int content, int contentCurrent) : base(content, contentCurrent)
        {
            Content = ContentValue;
            ContentCurrent = contentCurrent;
        }

        public override int EmptyContainer(int input)
        {
            int result = input;
            int MinVal = ContentCurrent - input;

            if (input <= ContentCurrent && input > 0)
            {
                while (input >= MinVal && !CheckContainerIfEmpty())
                {
                    ContentCurrent--;
                    input--;
                }
            }
            else
            {
                result = ContentCurrent;
            }
            return result;
        }

        public override bool CheckContainerIfEmpty()
        {
            bool result = false;
            if (ContentCurrent == 0)
            {
                result = true;
            }
            return result;
        }

        public override Task<int> FillContainer(int input, CancellationToken cancellationToken)
        {
            Task<int> task = null;

            task = Task.Run(() =>
            {
                int result = 0;

                if (input > 0)
                {
                    for (int i = 0; i < input; i++)
                    {
                        if (CheckContainerIfFull())
                        {
                            // error Container class
                            OnContainerFilled(0);
                            result = input - i;
                            if (result >= 1)
                            {
                                OnContainerOverflow(result);
                            }
                        }
                        else
                        {
                            ContentCurrent++;
                        }
                    }
                }

                return result;
            });

            return task;
        }

        public override bool CheckContainerIfFull()
        {
            bool result = false;
            if (ContentCurrent == Content)
            {
                result = true;
            }
            return result;
        }

        public override string ToString()
        {
            return $"size:{Content}, Content:{ContentCurrent}, fixed size:{ContentValue}";
        }
    }
}
