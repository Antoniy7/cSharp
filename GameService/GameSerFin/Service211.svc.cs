using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GameSerFin
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service211" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service211.svc or Service211.svc.cs at the Solution Explorer and start debugging.
    public class Service211 : IService211
    {
        public int MinMax(List<int> allData)
        {
            int add = 0;
            if (allData.Count == 1)
            { add = 0; }
            //return allData.ElementAt(((allData.Count+add) / 2 ));

            int val = 0;
            for (int i = 0; i < allData.Count; ++i)
            {
                val += allData.ElementAt(i);
            }

            if (allData.Count == 0) return -1;
            else if (allData.Count / 2 == 0 && allData.Count == 1) return allData.ElementAt(1);
            else if (allData.Count / 2 != 0)
                return allData.ElementAt(allData.Count / 2);
            else return allData.ElementAt((allData.Count + 1) / 2);
        }
        public int Position(List<int> data)
        {
            return MinMax(data);
        }
    }
}
