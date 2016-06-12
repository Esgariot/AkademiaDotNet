using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PT_Lab5.Annotations;

namespace PT_Lab5 {
    public class WorkerViewModel : INotifyPropertyChanged {
        #region class_fields

        private MathWorker MathWorker;
        private ArchiveWorker ArchiveWorker;
        private string textBoxContentN;
        private string textBoxContentK;
        private int textBoxContentI;
        private int textBoxContentRand;
        private int fibonacciValue;
        private int textBoxContentFib;
        private string textBlockTasksOut;
        private string textBlockDelegatesOut;
        private string textBlockAsyncOut;
        private int BinomialSleepTime = 3000;
        private BackgroundWorker worker;

        public WorkerViewModel() {
            MathWorker = new MathWorker();
            ArchiveWorker = new ArchiveWorker();
        }

        public string TextBoxContentN {
            get { return textBoxContentN; }

            set {
                textBoxContentN = value;
                OnPropertyChanged(nameof(TextBoxContentN));
            }
        }


        public string TextBoxContentK {
            get { return textBoxContentK; }

            set {
                textBoxContentK = value;
                OnPropertyChanged(nameof(TextBoxContentK));
            }
        }

        public string TextBlockTasksOut {
            get { return textBlockTasksOut; }

            set {
                textBlockTasksOut = value;
                OnPropertyChanged(nameof(TextBlockTasksOut));
            }
        }

        public string TextBlockDelegatesOut {
            get { return textBlockDelegatesOut; }

            set {
                textBlockDelegatesOut = value;
                OnPropertyChanged(nameof(TextBlockDelegatesOut));
            }
        }

        public string TextBlockAsyncOut {
            get { return textBlockAsyncOut; }

            set {
                textBlockAsyncOut = value;
                OnPropertyChanged(nameof(TextBlockAsyncOut));
            }
        }

        public int TextBoxContentI {
            get { return textBoxContentI; }

            set {
                textBoxContentI = value;
                OnPropertyChanged(nameof(TextBoxContentI));
            }
        }

        public int FibonacciValue {
            get { return fibonacciValue; }

            set {
                fibonacciValue = value;
                OnPropertyChanged(nameof(FibonacciValue));
            }
        }

        public int TextBoxContentRand {
            get { return textBoxContentRand; }

            set {
                textBoxContentRand = value;
                OnPropertyChanged(nameof(TextBoxContentRand));
            }
        }

        public int TextBoxContentFib {
            get { return textBoxContentFib; }

            set { textBoxContentFib = value; }
        }

        #endregion

        #region Zadanie 1 - Dwumian

        public Task BinomialTaskMethod() {
            var numerator = int.Parse(TextBoxContentN) - int.Parse(TextBoxContentK) + 1;

            var numeratorTask = new Task<ulong>(() => MathWorker.PartialFactorial(int.Parse(TextBoxContentN), numerator));
            var denominatorTask = new Task<ulong>(() => MathWorker.PartialFactorial(int.Parse(TextBoxContentK)));
            var outputTask = Task.Run(() => {
                numeratorTask.Start();
                denominatorTask.Start();
                Task.WaitAll(numeratorTask, denominatorTask);
                Thread.Sleep(BinomialSleepTime);
                TextBlockTasksOut = (numeratorTask.Result/denominatorTask.Result).ToString();
            });
            return outputTask;
        }

        public Func<ulong> BinomialDelegateMethod() {
            var numerator = int.Parse(TextBoxContentN) - int.Parse(TextBoxContentK) + 1;
            ulong numeratorOutput = 0;
            ulong denominatorOutput = 0;
            var waitHandles = new List<WaitHandle>();
            Func<ulong> numeratorDelegate = () => MathWorker.PartialFactorial(int.Parse(TextBoxContentN), numerator);
            Func<ulong> denominatorDelegate = () => MathWorker.PartialFactorial(int.Parse(TextBoxContentK));
            Func<ulong> outputDelegate = () => {
                numeratorDelegate.BeginInvoke(
                    numeratorResult => {
                        numeratorOutput = numeratorDelegate.EndInvoke(numeratorResult);
                        waitHandles.Add(numeratorResult.AsyncWaitHandle);
                    },
                    numeratorDelegate);
                denominatorDelegate.BeginInvoke(
                    denominatorResult => {
                        denominatorOutput = denominatorDelegate.EndInvoke(denominatorResult);
                        waitHandles.Add(denominatorResult.AsyncWaitHandle);
                    },
                    denominatorDelegate);
                Thread.Sleep(BinomialSleepTime);
                WaitHandle.WaitAll(waitHandles.ToArray());
                TextBlockDelegatesOut = (numeratorOutput/denominatorOutput).ToString();
                return numeratorOutput/denominatorOutput;
            };

            return outputDelegate;
        }

        public async void BinomialAsyncMethod() {
            var numerator = int.Parse(TextBoxContentN) - int.Parse(TextBoxContentK) + 1;

            var numeratorTask = new Task<ulong>(() => MathWorker.PartialFactorial(int.Parse(TextBoxContentN), numerator));
            var denominatorTask = new Task<ulong>(() => MathWorker.PartialFactorial(int.Parse(TextBoxContentK)));
            await Task.Run(() => {
                numeratorTask.Start();
                denominatorTask.Start();
                Task.WaitAll(numeratorTask, denominatorTask);
                Thread.Sleep(BinomialSleepTime);
                TextBlockAsyncOut = (numeratorTask.Result/denominatorTask.Result).ToString();
            });
        }

        public void NextRand() {
            TextBoxContentRand = MathWorker.NextRand();
        }

        #endregion

        public ulong FibonacciBackgroudMethod(ulong current, ulong previous) {
            return MathWorker.NextFibonacci(current, previous);
        }

        #region Zadanie 3 - Kompresja

        public void Compress() {
            ArchiveWorker.Compress();
        }

        public void Decompress() {
            ArchiveWorker.Decompress();
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}