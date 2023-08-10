using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaringCompare.Models
{
    public class TaringItem
    {
        [JsonIgnore]
        public long Id { get; set; }
        public uint Number { get; set; }
        public float Delta { get; set; }
        public ushort RawLevel { get; set; }
        public float LitersLevel { get; set; }
        public long TaringID { get; set; }
        public TaringItem() { }

        public TaringItem(ushort rawLevel, float litersLevel)
        {
            RawLevel = rawLevel;
            LitersLevel = litersLevel;
        }
    }
}
