using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
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
            if (string.IsNullOrEmpty(path)) return new List<Taring>();
            string data = File.ReadAllText(path);
            var tars = JsonSerializer.Deserialize<List<Taring>>(data);
            return tars is not null ?  tars : new List<Taring>();
        }

        public static IEnumerable<Taring> LoadFromDb() => Repository.GetTarings();
    }
}
