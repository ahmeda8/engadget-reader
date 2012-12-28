using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Phone.Shell;

namespace Engadget_Reader
{
    public partial class MainPage : PhoneApplicationPage
    {
        private BackgroundWorker loader;
        private Engadget engadget;
        private ObservableCollection<PivotModel> DataCollection;
        private ProgressIndicator PI;
        // Constructor
        
        public MainPage()
        {
            InitializeComponent();
            loader = new BackgroundWorker();
            DataCollection = new ObservableCollection<PivotModel>();
            PI = new ProgressIndicator();
            SystemTray.SetProgressIndicator(this, PI);

            if (!Network.IsOnline())
                MessageBox.Show("No internet connectivity.");
            PI.IsIndeterminate = true;
            PI.IsVisible = true;

            pvtcontrol.ItemsSource = DataCollection;
            loader.RunWorkerCompleted += loader_RunWorkerCompleted;
            loader.DoWork += loader_DoWork;
            UpdateData();
            loader.RunWorkerAsync();
        }

        void loader_DoWork(object sender, DoWorkEventArgs e)
        {
            //UpdateData();
            
            while (!engadget.Completed)
            {
                System.Threading.Thread.Sleep(2000);
            }
        }

        void loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => {
                PI.IsVisible = false;
                PI.IsIndeterminate = false;
            });
            
        }

        public void UpdateData()
        {
            engadget = new Engadget(DataCollection);
            engadget.Start();
        }

        private void itemSelected(object sender, SelectionChangedEventArgs e)
        {
            var obj = sender as ListBox;
            if (obj.SelectedIndex > -1)
            {
                ObjectSaver.GetInstance().Set(obj.SelectedItem);
                NavigationService.Navigate(new Uri("/Details.xaml", UriKind.Relative));
            }
        }
    }
}