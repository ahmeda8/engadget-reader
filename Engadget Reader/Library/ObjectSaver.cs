using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engadget_Reader
{
    class ObjectSaver
    {
        private Object obj;
        private static ObjectSaver instance;

        public static ObjectSaver GetInstance()
        {
            if (instance == null)
                instance = new ObjectSaver();
            return instance;
        }

        public void Set(object o)
        {
            obj = o;
        }

        public object Get()
        {
            return obj;
        }
    }
}
