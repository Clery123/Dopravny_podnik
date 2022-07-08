using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTO
{
    public class Path
    {
        public int pID { get; set; }
        public String From { get; set; }
        public String To{ get; set; }
        public int Number_of_stops { get; set; }
        public double Distance { get; set; }
    }
}
