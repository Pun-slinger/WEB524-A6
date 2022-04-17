using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S2021A6QP.Models
{
    public class TrackMediaViewModel
    {
        public int Id { get; set; }
        public string MediaContentType { get; set; }
        public byte[] Media { get; set; }
    }
}