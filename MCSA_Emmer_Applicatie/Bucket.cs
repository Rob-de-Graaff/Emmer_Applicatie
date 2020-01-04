using System;
using System.Threading;
using System.Threading.Tasks;

namespace MCSA_Emmer_Applicatie
{
    public class Bucket : Container
    {
        public int ContentMin { get; private set; }
        public bool FullWarning { get; private set; }
        public bool PartiallyEmptied { get; private set; }
        private const int ContentMinValue = 10;
        private const int ContentDefault = 12;

        public Bucket()
        {
            Content = ContentDefault;
            ContentMin = ContentMinValue;
            ContentCurrent = 0;
            FullWarning = false;
            PartiallyEmptied = false;
        }

        public Bucket(int content, int contentCurrent, bool fullWarning, bool partiallyEmptied, int contentMin = ContentMinValue) : base(content, contentCurrent)
        {
            Content = content;
            ContentMin = contentMin;
            ContentCurrent = contentCurrent;
            FullWarning = fullWarning;
            PartiallyEmptied = partiallyEmptied;
        }

        public override int EmptyContainer(int input)
        {
            int result = input;
            int MinVal = ContentCurrent - input;

            if (PartiallyEmptied && input <= ContentCurrent && input > 0)
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
                result = ContentCurrent;
            }
            return result;
        }

        public override Task<int> FillContainer(int input, CancellationToken cancellationToken)
        {
            Task<int> task = null;

            task = Task.Run(() =>
            {
                int result = 0;

                if (!cancellationToken.IsCancellationRequested)
                {
                    if (input > 0)
                    {
                        for (int i = 0; i < input; i++)
                        {
                            if (CheckContainerIfFull())
                            {
                                // Raises Filled event
                                // TODO: Check why event is not raised
                                OnContainerFilled(0);

                                result = input - i;
                                if (result >= 1)
                                {
                                    // Raises overflow event
                                    OnContainerOverflow(result);

                                    //Console.WriteLine($"this bucket will overflow, do you wish to continue? Y/N");
                                    //var tempKey = Console.ReadKey();
                                    //switch (tempKey.Key)
                                    //{
                                    //    case ConsoleKey.Y:
                                    //        // Cancel the task
                                    //        //cancellationToken.Cancel();
                                    //        //if (cancellationToken.IsCancellationRequested)
                                    //        //{ throw new TaskCanceledException(task); }
                                    //        break;
                                    //    default:
                                    //        break;
                                    //}
                                }
                            }
                            else
                            {
                                ContentCurrent++;
                            }
                        }
                    }
                }
                else
                {
                    throw new TaskCanceledException(task);
                }

                return result;
            });

            return task;
        }

        public override bool CheckContainerIfFull()
        {
            bool result = false;
            if (ContentCurrent == Content && FullWarning)
            {
                result = true;
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
        
        public async Task TransferBucketContent(int input, Container containerIn, Container containerOut)
        {
            if (containerIn is Bucket && containerOut is Bucket)
            {
                using (var cancellationTokenSource = new CancellationTokenSource())
                {
                    // TODO: Check why keyBoardTask is skipped (example: AsyncTips)
                    var keyBoardTask = Task.Run(() =>
                    {
                        Console.WriteLine($"this bucket will overflow, do you wish to continue? Y/N");
                        var tempkey = Console.ReadKey();
                        switch (tempkey.Key)
                        {
                            case ConsoleKey.Y:
                                // Cancel the task
                                cancellationTokenSource.Cancel();
                                break;
                            default:
                                break;
                        }
                    });

                    try
                    {
                        int TranferAmount = containerIn.EmptyContainer(input);
                        // FillContainer method runs asynchronously.
                        containerIn.ContentCurrent += await containerOut.FillContainer(TranferAmount, cancellationTokenSource.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        Console.WriteLine("Task was cancelled");
                        
                    }

                    await keyBoardTask;
                }
            }
        }

        public override string ToString()
        {
            return $"size:{Content}, Content:{ContentCurrent}, min:{ContentMin}";
        }
    }
}