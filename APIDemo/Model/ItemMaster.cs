using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Model
{
    [Table("MasterItem")]
    public class ItemMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }

    }

    public class ItemMasterView
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}
