using System;
using System.Threading;
using System.Threading.Tasks;

namespace MCSA_Emmer_Applicatie
{
    public class Bucket : Container
    {
        #region fields/ properties
        public int ContentMin { get; private set; }
        public bool FullWarning { get; private set; }
        public bool OverflowWarning { get; private set; }
        public bool OverflowStop { get; private set; }
        private const int ContentMinValue = 10;
        private const int ContentDefault = 12;
        #endregion

        #region Constructors
        public Bucket() : this(ContentDefault, 0, false, false, false) { }

        public Bucket(int content, int contentCurrent, bool fullWarning, bool overflowWarning, bool overflowStop) : base(content, contentCurrent)
        {
            if (content >= ContentMinValue)
            {
                Content = content;
            }
            else
            {
                Content = ContentMinValue;
            }
            ContentCurrent = contentCurrent;
            FullWarning = fullWarning;
            OverflowWarning = overflowWarning;
            OverflowStop = overflowStop;

            // every bucket Listens for containerfull event
            ContainerFilled += (sender, e) => OnContainerFilled(sender, e);
            // every bucket Listens for containerOverflow event
            ContainerOverflow += (sender, e) => ContainerOverflowHandler(e);
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
                    // example input = 2 currentcontent = 1. 1-2=-1
                    //result = ContentCurrent;
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
                            if (result >= 1 && OverflowWarning)
                            {
                                // Raises overflow event
                                OnContainerOverflow(result);

                                if (OverflowStop)
                                {
                                    break;
                                }
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
            return (ContentCurrent == Content);
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

        public async void TransferBucketContent(int input, Container containerOut)
        {
            var containerTypeOut = this as Bucket;
            var containerTypeIn = containerOut as Bucket;
            if (containerTypeOut != null && containerTypeIn != null)
            {
                int TranferAmount = EmptyContainer(input);
                if (TranferAmount > 0)
                {
                    ShowLeftoverContent(input, containerOut);
                    // FillContainer method runs asynchronously.
                    var FillTask = containerOut.FillContainer(TranferAmount);
                    ContentCurrent += await FillTask;
                }
                else
                {
                    Console.WriteLine($"Transferring bucket content failed");
                }
                #region Example code
                //try
                //{
                //    int TranferAmount = EmptyContainer(input);
                //    ShowLeftoverContent(input, containerOut);
                //    // FillContainer method runs asynchronously.
                //    var FillTask = containerOut.FillContainer(TranferAmount);
                //    ContentCurrent += await FillTask;
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //    //Console.WriteLine("Task was cancelled");
                //}
                #endregion
            }
            else
            {
                Console.WriteLine($"this container is not a bucket but a {GetType().Name}/{containerOut.GetType().Name}");
            }
        }

        public void ShowLeftoverContent(int input, Container containerOut)
        {
            int result = 0;
            if (input + containerOut.ContentCurrent > containerOut.Content)
            {
                result = containerOut.Content - containerOut.ContentCurrent;
                Console.WriteLine($"the bucket may overflow, please pour {result}L to prevent this from happening");
            }
        }

        public override string ToString()
        {
            return $"size:{Content}, Content:{ContentCurrent}, min:{ContentMin}";
        }

        public void OnContainerFilled(object sender, ContainerEventArgs e)
        {
            if (FullWarning)
            {
                Console.WriteLine($"bucket is full");
            }
        }

        public void ContainerOverflowHandler(ContainerEventArgs e)
        {
            if (OverflowWarning)
            {
                Console.WriteLine($"bucket is overflowing {e.Overflow}L");
            }
        }
        #endregion
    }
}