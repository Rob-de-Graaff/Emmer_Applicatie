using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCSA_Emmer_Applicatie
{
    class Rain_barrel : Container
    {
        #region fields/ properties
        public Sizes ContentSize { get; private set; }
        private const Sizes ContentSizeDefault = Sizes.Medium;
        private const int ContentDefault = 120;

        public enum Sizes
        {
            Small,
            Medium,
            Large
        }
        #endregion

        #region Constructors
        public Rain_barrel()
        {
            Content = ContentDefault;
            ContentCurrent = 0;
            ContentSize = ContentSizeDefault;
        }

        public Rain_barrel(int content, int contentCurrent) : base(content, contentCurrent)
        {
            Content = SetRainBarrelContent(content);
            ContentCurrent = contentCurrent;
            ContentSize = SetRainBarrelSize(content);
        }
        #endregion

        #region Methods

        private int SetRainBarrelContent(int input)
        {
            int selectedContent = 0;
            switch (input)
            {
                case int c when (c >= 160):
                    selectedContent = 160;
                    break;
                case int c when (c < 160 && c >= 120):
                    selectedContent = 120;
                    break;
                case int c when (c < 120 && c >= 80):
                    selectedContent = 80;
                    break;
            }
            return selectedContent;
        }

        private Sizes SetRainBarrelSize(int input)
        {
            Sizes selectedSize = Sizes.Medium;
            switch (input)
            {
                case int s when (s >= 160):
                    selectedSize = Sizes.Large;
                    break;
                case int s when (s < 160 && s >= 120):
                    selectedSize = Sizes.Medium;
                    break;
                case int s when (s < 120 && s >= 80):
                    selectedSize = Sizes.Small;
                    break;
            }
            return selectedSize;
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

        public override bool CheckContainerIfFull()
        {
            bool result = false;
            if (ContentCurrent == Content)
            {
                result = true;
            }
            return result;
        }

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

        public override string ToString()
        {
            return $"size:{Content}, Content:{ContentCurrent}, fixed size:{ContentSize}";
        }
        #endregion
    }
}
