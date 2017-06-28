using Gapura.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gapura.BLL.ViewModel
{
    public class ReqHDVM
    {
        public RequestHeader RequestH { get; set; }
        public List<RequestDetail> RequestD { get; set; }
    }
}