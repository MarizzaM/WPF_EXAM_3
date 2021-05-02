using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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

namespace WPF_EXAM_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            this.vm = new MainWindowViewModel();
            this.DataContext = this.vm;
        }

        public void SafeInvoke(Action action)
        {
            if (Dispatcher.CheckAccess())
            {
                action.Invoke();
                return;
            }
            else
            {
                this.Dispatcher.BeginInvoke(action);
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Task.Run(() =>
            {
                    Action uiwork = () => sizeText.Text = vm.ReadUrl(vm.URL);
                    SafeInvoke(uiwork);
                    Thread.Sleep(1000);
            });
           lbl.Content = $"The total time elapsed is: {stopwatch.Elapsed.TotalMilliseconds} ms";
            stopwatch.Stop();
        }
    }
    
}
