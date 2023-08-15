using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaringCompare.Models;

namespace TaringCompare.Data
{
    public class Repository
    {
        public static IEnumerable<Taring> GetTarings()
        {
            IEnumerable<Taring> _list = new List<Taring>();
            using (ApplicationContext _context = new ApplicationContext())
            {
                _list = _context.Taring;
            }
            return _list.ToList();
        }
    }
}
