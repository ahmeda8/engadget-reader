using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Engadget_Reader
{
    class Engadget : BaseWebGet
    {
        private ObservableCollection<PivotModel> _DataCollection;
        public bool Completed = false;
        private int CompletedCount = 0;

        public Engadget(ObservableCollection<PivotModel> DataContextUpdateable)
        {
            _DataCollection = DataContextUpdateable;
        }

        public void Start()
        {
            if (!Completed)
            {
                _DataCollection.Clear();
                CompletedCount = 0;
                _DataCollection.Add(new PivotModel { Header = "news", Url = "http://www.engadget.com/rss.xml", DataCollection = new ObservableCollection<DisplayModel>() });
                //_DataCollection.Add(new PivotModel { Header = "tech deals", Url = "http://www.deals2buy.com/rssgen/tech.xml", DataCollection = new ObservableCollection<DisplayModel>() });
                //_DataCollection.Add(new PivotModel { Header = "non-tech deals", Url = "http://www.deals2buy.com/rssgen/ntech.xml", DataCollection = new ObservableCollection<DisplayModel>() });
                //_DataCollection.Add(new PivotModel { Header = "laptop deals", Url = "http://www.deals2buy.com/rssgen/laptop.xml", DataCollection = new ObservableCollection<DisplayModel>() });
                //_DataCollection.Add(new PivotModel { Header = "hot deals", Url = "http://www.deals2buy.com/rssgen/hotdeals.xml", DataCollection = new ObservableCollection<DisplayModel>() });

                for (int i = 0; i < _DataCollection.Count; i++)
                {
                    if (Network.IsOnline())
                    {
                        this.MakeGetRequest(_DataCollection.ElementAt(i).Url);
                    }
                    else
                    {
                        string offline_str = ISO.ReadFile(System.IO.Path.GetFileName(_DataCollection.ElementAt(i).Url));
                        Populate(offline_str, i);
                        
                    }

                }
            }
        }

        public void Refresh()
        {
            Completed = false;
            Start();
        }

        public override void GetResponse(IAsyncResult WebResult)
        {
            HttpWebRequest wreq = (HttpWebRequest)WebResult.AsyncState;

            int index = 0;
            foreach (PivotModel pm in _DataCollection)
            {
                if (pm.Url == wreq.RequestUri.OriginalString)
                    break;
                index++;
            }
            HttpWebResponse wresp = (HttpWebResponse)wreq.EndGetResponse(WebResult);
            StreamReader strmrdr = new StreamReader(wresp.GetResponseStream());
            string str = strmrdr.ReadToEnd();
            ISO.WriteFile(System.IO.Path.GetFileName(wreq.RequestUri.OriginalString), str);
            Populate(str, index);
            
        }

        private void Populate(string str,int index)
        {
            XDocument xdoc = XDocument.Parse(str);
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                foreach (XElement item in xdoc.Root.Element("channel").Elements("item"))
                {
                    DisplayModel dp = new DisplayModel();
                    _DataCollection.ElementAt(index).DataCollection.Add(dp);
                    dp.Title = StringExtensions.Cleanup((string)item.Element("title"));
                    
                    dp.Date = StringExtensions.Cleanup((string)item.Element("pubDate"));
                    dp.Date = dp.Date.Substring(0, dp.Date.Length - 1);
                    dp.Desc = StringExtensions.Cleanup((string)item.Element("description"));
                    dp.Image = StringExtensions.ExtractImageLocation((string)item.Element("description"));//"http://www.blogcdn.com/www.engadget.com/media/2012/12/12-27-2012bluestacksmac.jpg";
                    dp.Link = StringExtensions.Cleanup((string)item.Element("link"));
                }
            });

            CompletedCount++;
            if (CompletedCount == _DataCollection.Count)
                Completed = true;
        }
    }
}
