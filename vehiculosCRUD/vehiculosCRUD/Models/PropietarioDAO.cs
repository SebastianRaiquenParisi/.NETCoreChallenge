using System;
using System.Collections.Generic;

namespace vehiculosCRUD.Models
{
    public class PropietarioDAO
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public _propietario[] data { get; set; }
        public _support support { get; set; }


    }
    public class _propietario {
        public int id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
    }
    public class _support { 
        public string url { get; set; }
        public string text { get; set; }
    }
}
