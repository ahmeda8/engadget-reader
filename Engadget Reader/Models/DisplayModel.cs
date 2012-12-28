using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engadget_Reader
{
    public class DisplayModel : BaseModel
    {
        private string _Title;

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                NotifyPropertyChanged("Title", this);
            }
        }

        private string _Desc;

        public string Desc
        {
            get
            { return _Desc; }
            set
            {
                _Desc = value;
                NotifyPropertyChanged("Desc", this);
            }
        }

        private string _Date;
        public string Date
        {
            get
            { return _Date; }
            set
            {
                _Date = value;
                NotifyPropertyChanged("Date", this);
            }
        }

        private string _Link;
        public string Link
        {
            get
            { return _Link; }
            set
            {
                _Link = value;
                NotifyPropertyChanged("Link", this);
            }
        }

        private string _Image;
        public string Image
        {
            get
            { return _Image; }
            set
            {
                _Image = value;
                NotifyPropertyChanged("Image", this);
            }
        }

    }
}
