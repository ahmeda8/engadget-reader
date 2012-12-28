using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;

namespace Engadget_Reader
{
    class ISO
    {
        public static bool WriteFile(string filename,string data)
        {
            bool success = false;
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                IsolatedStorageFileStream filestream = new IsolatedStorageFileStream(filename, System.IO.FileMode.Create, isf);
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                filestream.Write(buffer, 0, buffer.Length);
                filestream.Close();
                success = true;
            }
            return success;
        }

        public static string ReadFile(string filename)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    IsolatedStorageFileStream filestream = new IsolatedStorageFileStream(filename, System.IO.FileMode.Open, isf);
                    byte[] buffer = new byte[filestream.Length];
                    filestream.Read(buffer,0,buffer.Length);
                    string data = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    return data;
                }
                catch (System.IO.FileNotFoundException)
                {
                    return string.Empty;
                }
            }
        }
    }
}
