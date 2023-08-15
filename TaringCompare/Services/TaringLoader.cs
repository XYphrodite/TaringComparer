using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TaringCompare.Data;
using TaringCompare.Models;

namespace TaringCompare.Services
{
    public static class TaringLoader
    {
        public static IEnumerable<Taring> LoadFromJson()
        {
            OpenFileDialog ofd = new();
            ofd.ShowDialog();
            string path = ofd.FileName;
            string data = File.ReadAllText(path);
            var tars = JsonSerializer.Deserialize<List<Taring>>(data);
            return tars is not null ?  tars : new List<Taring>();
        }

        public static IEnumerable<Taring> LoadFromDb()
        {
            throw new NotImplementedException();
        }
    }

    //public class TaringViewModel
    //{
    //    public Taring Taring { get; set; }
    //    public int Quantity { get; set; }
    //}
}
