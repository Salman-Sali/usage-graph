using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PerformanceCounter theCPUCounter = new PerformanceCounter("Processor", "% Idle Time", "_Total");
        private PerformanceCounter theMemCounter = new PerformanceCounter("Memory", "Available MBytes");
        public MainWindow()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += delegate { DragMove(); };
            Topmost = true;

            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    Dispatcher.Invoke(() =>
                    {
                        cpuText.Text = ( decimal.Round(100- (decimal)theCPUCounter.NextValue(), 2, MidpointRounding.ToPositiveInfinity)).ToString() + "%";

                        memText.Text = (theMemCounter.NextValue()).ToString() + " mb free";
                    });
                }

            }).Start();

            var aaaa = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            var aaa = aaaa.RawValue;
        }
    }
}
