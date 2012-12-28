using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Engadget_Reader
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        public void NotifyPropertyChanged(string propertyName, Object obj)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(obj, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
