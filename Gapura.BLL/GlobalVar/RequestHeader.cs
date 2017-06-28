using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Gapura.BLL.Models;

namespace Gapura.BLL.GlobalVar
{
    public partial class RequestHeader
    {
        private int RequestID;
        public string SeqRequestNo
        {
            get
            {   // 001/RF/GA/I/17
                var seq = RequestID.ToString() + "/RF/GA/";

                return seq;
            }
        }
    }
}