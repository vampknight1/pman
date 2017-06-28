using Gapura.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gapura.BLL.ViewModel
{
    public class RequestHDVM
    {
        public RequestHDVM()
        {
            vmRequestHeader = new RequestHeader();
            vmRequestDetail = new RequestDetail();
        }

        public RequestHeader vmRequestHeader { get; set; }
        public RequestDetail vmRequestDetail { get; set; }
    }
}