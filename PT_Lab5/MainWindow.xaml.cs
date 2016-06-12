using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PT_Lab5 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private BackgroundWorker worker;

        public MainWindow() {
            InitializeComponent();
            worker = this.FindResource("BackgroundWorker") as BackgroundWorker;
            worker.DoWork += BackgroundWorker_OnDoWork;
        }

        private void TasksButton_Click(object sender, RoutedEventArgs e) {
            ((WorkerViewModel) DataContext).BinomialTaskMethod();
        }

        private void DelegatesButton_Click(object sender, RoutedEventArgs e) {
            ((WorkerViewModel) DataContext).BinomialDelegateMethod().BeginInvoke(null, null);
        }

        private void AsyncButton_Click(object sender, RoutedEventArgs e) {
            ((WorkerViewModel) DataContext).BinomialAsyncMethod();
        }

        private void NTextInput_OnPreviewTextInput(object sender, TextCompositionEventArgs e) {}
        private void KTextInput_OnPreviewTextInput(object sender, TextCompositionEventArgs e) {}

        private void GetFibonacci_Click(object sender, RoutedEventArgs e) {}

        private void BackgroundWorker_OnDoWork(object sender, DoWorkEventArgs e) {
            int fibN = ((WorkerViewModel) DataContext).TextBoxContentFib;
        }

        private void BackgroundWorker_OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {}

        private void BackgroundWorker_OnProgressChanged(object sender, ProgressChangedEventArgs e) {}

        private void NextRandButton_OnClick_Click(object sender, RoutedEventArgs e) {
            ((WorkerViewModel) DataContext).NextRand();
        }

        private void CompressButton_Click(object sender, RoutedEventArgs e) {
            ((WorkerViewModel)DataContext).Compress();
        }

        private void DecompressButton_Click(object sender, RoutedEventArgs e) {
            ((WorkerViewModel)DataContext).Decompress();
        }
    }
}