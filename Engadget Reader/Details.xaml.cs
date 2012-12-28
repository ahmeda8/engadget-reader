using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Engadget_Reader
{
    public partial class Details : PhoneApplicationPage
    {
        public Details()
        {
            InitializeComponent();
            DataContext = ObjectSaver.GetInstance().Get();
        }
    }
}