using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WPF_EXAM_3
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        
        public string URL { get; set; }
        public DelegateCommand MyDelegate { get; set; }

        private string m_size;
        private bool m_flag;

        public string SizeValue
        {
            get
            {
                return this.m_size;
            }
            set
            {
                this.m_size = value;
                OnPropertyChanged("SizeValue");
            }
        }

        public bool FlagIsEnabled
        {
            get
            {
                return this.m_flag;
            }
            set
            {
                this.m_flag = value;
                OnPropertyChanged("FlagIsEnabled");
            }
        }
        public MainWindowViewModel()
        {
            MyDelegate = new DelegateCommand(ExecuteCommand, CanExecuteMethod);
            SizeValue = "Please Wait...";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        private bool CanExecuteMethod() { return true; }

        private void ExecuteCommand()
        {
            FlagIsEnabled = false;
        }
        public string ReadUrl(string url)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                WebResponse response = webRequest.GetResponseAsync().Result;

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string text = reader.ReadToEndAsync().Result;
                    SizeValue = text.Length.ToString();
                }
                FlagIsEnabled = true;
            }
            catch (IOException ioex)
            {
                throw new IOException("An error occurred while processing the link.", ioex);
            }
            catch (Exception e)
            {
                throw new Exception("An generic error ocurred.");
            }
            return SizeValue;
        }
    }
}
