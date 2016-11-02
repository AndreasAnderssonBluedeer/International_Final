using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchUp.Core.Models
{
    public class Location
    {
        public Location() { }
        
        public string Id { get; set; }
        public string LocalizedName { get; set; }
        public int Rank { get; set; }
        public string Key { get; set; }
    }
}
