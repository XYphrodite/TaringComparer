using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaringComparer.Models;

namespace MieltaMarketplace.Models
{
    public class Tank
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "req_a")]
        [Range(1, 5000, ErrorMessage = "len")]
        public int a { get; set; }
        [Required(ErrorMessage = "req_b")]
        [Range(1, 5000, ErrorMessage = "len")]
        public int b { get; set; }
        [Required(ErrorMessage = "req_c")]
        [Range(1, 5000, ErrorMessage = "len")]
        public int c { get; set; }
        [Required(ErrorMessage = "req_volume")]
        [Range(1, 5000, ErrorMessage = "len")]
        public float Volume { get; set; }
        [Required(ErrorMessage = "req_form")]
        public TankForm Form { get; set; }
        [Range(1, 99999999, ErrorMessage = "len")]
        [Required(ErrorMessage = "req_veh")]
        public int VehicleModelId { get; set; }
        //public VehicleModel VehicleModel { get; set; }
        [MaxLength(450)]
        //public string LittleDescription { get; set; }
        public ICollection<Taring> Tarings { get; set; } = new List<Taring>();
        [NotMapped]
        public string FormStr { get { return Form.ToString(); } }
        public static List<TankForm> GetAllForms()
        {
            return new List<TankForm>
            {
                TankForm.Rectangle,
                TankForm.Cube,
                TankForm.Cylinder,
                TankForm.Specific,
                TankForm.Unknown,
                TankForm.All,
                TankForm.Unspecific,
                TankForm.Common
            };
        }
    }

    public enum TankForm : byte
    {
        Rectangle = 0,
        Cube = 1,
        Cylinder = 2,
        Specific = 3,
        Unknown = 4,
        All = 5,
        Common = 6,
        Unspecific = 7
    }
}
