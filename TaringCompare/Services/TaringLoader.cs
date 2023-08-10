using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
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
            return !string.IsNullOrEmpty(data) ? JsonSerializer.Deserialize<List<Taring>>(data) : new List<Taring>();
        }

        public static async Task LoadFromDb()
        {
            throw new NotImplementedException();
        }
    }
}
