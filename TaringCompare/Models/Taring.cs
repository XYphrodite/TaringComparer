using System;
using System.Collections.Generic;

namespace TaringCompare.Models
{
    public class Taring
    {
        public ushort LitersMax { get; set; }
        public ushort LevelMin { get; set; }
        public ushort LevelMax { get; set; }
        public string Title { get; set; } = "No title";
        public string Description { get; set; } = "No desсription";
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int SensorLength { get; set; }
        public int DistanceX { get; set; }
        public int DistanceY { get; set; }
        public string HtmlPage { get; set; } = @"<html><body><h1 align='center'>Description</h1><p align='center'>Not created, please add the description.</p></body></html>";

        List<TaringItem> taringList = new List<TaringItem>();
        public List<TaringItem> TaringList { get; set; }
        //public Tank Tank { get; set; }



        //public void AddTaringItem(TaringItem taringItem)
        //{
        //    taringItem.Number = (uint)TaringList.Count + 1;
        //    taringItem.TaringID = TaringID;
        //    taringItem.Delta = TaringList.Count == 0 ? taringItem.LitersLevel : taringItem.LitersLevel - TaringList[TaringList.Count - 1].LitersLevel;
        //    TaringList.Add(taringItem);
        //    taringHeader.rowCnt = (byte)taringList.Count;
        //}

        //public override bool Equals(object obj)
        //{
        //    if (obj == null) return false;

        //    // Optimization for a common success case.
        //    if (Object.ReferenceEquals(this, obj)) return true;

        //    // If run-time types are not exactly the same, return false.
        //    if (this.GetType() != obj.GetType()) return false;

        //    var taring = (Taring)obj;

        //    bool result = TaringID.Equals(taring.TaringID)
        //                  && Ver.Equals(taring.Ver)
        //                  && IsEncrypted.Equals(taring.IsEncrypted)
        //                  && Units.Equals(taring.Units)
        //                  && LitersMax.Equals(taring.LitersMax)
        //                  && LevelMin.Equals(taring.LevelMin)
        //                  && LevelMax.Equals(taring.LevelMax)
        //                  && Title.Equals(taring.Title)
        //                  && Description.Equals(taring.Description)
        //                  && HtmlPage.Equals(taring.HtmlPage)
        //                  && State.Equals(taring.State)
        //                  && TaringList.Count.Equals(taring.TaringList.Count);

        //    if (result != false)
        //    {
        //        for (int i = 0; i < TaringList.Count; i++)
        //        {
        //            if (TaringList[i].RawLevel != taring.TaringList[i].RawLevel) result = false;
        //            if (TaringList[i].LitersLevel != taring.TaringList[i].LitersLevel) result = false;
        //            if (TaringList[i].Delta != taring.TaringList[i].Delta) result = false;
        //            if (TaringList[i].Number != taring.TaringList[i].Number) result = false;
        //        }
        //    }

        //    return result;
        //}
        public static bool operator ==(Taring lhs, Taring rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Taring lhs, Taring rhs) => !(lhs == rhs);
    }
}
