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
        #region Fields
        private const int ContentValue = 159;
        #endregion

        #region Constructors
        public Oil_barrel()
        {
            Content = ContentValue;
            ContentCurrent = 0;
        }

        public Oil_barrel(int contentCurrent) : base(ContentValue, contentCurrent)
        {
            Content = ContentValue;
            ContentCurrent = contentCurrent;
        }
        #endregion

        #region Methods
        public override int EmptyContainer(int input)
        {
            int result = input;
            int MinVal = ContentCurrent - input;

            if (input > 0)
            {
                if (input <= ContentCurrent)
                {
                    while (input >= MinVal && !CheckContainerIfEmpty())
                    {
                        ContentCurrent--;
                        input--;
                    }
                }
                else
                {
                    result = 0;
                    Console.WriteLine($"Input: {input} is not allowed, it exceeds the content");
                }
            }
            else
            {
                result = 0;
                Console.WriteLine($"Input: {input} is not allowed, only positive digits");
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

        public override Task<int> FillContainer(int input)
        {
            var task = Task.Run(() =>
            {
                int result = 0;

                if (input > 0)
                {
                    for (int i = 0; i < input; i++)
                    {
                        if (CheckContainerIfFull())
                        {
                            // Raises Filled event
                            OnContainerFilled(0);

                            result = input - i;
                            if (result >= 1)
                            {
                                // Raises overflow event
                                OnContainerOverflow(result);
                                break;
                                #region Example code
                                //if (OverflowStop)
                                //{
                                //    break;
                                //}
                                #endregion
                            }
                        }
                        else
                        {
                            ContentCurrent++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Input: {input} is not allowed, only positive digits");
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
        #endregion
    }
}
