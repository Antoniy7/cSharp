using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GameService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GameServ" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GameServ.svc or GameServ.svc.cs at the Solution Explorer and start debugging.
    public class GameServ : IGameServ
    {
        public int Position()
        {
            return 5;
        }
        public int Sth()
        {
            return 0;
        }
    }
}
