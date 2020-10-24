using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace iosha.RaspberryPi3WebControl.Storage.Entity
{
    [Table("tblLedSchema")]
    public class LedSchema
    {
        [Key]
        [Required]
        [Column("gId")]
        public Guid Id { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("nKey")]
        public int Key { get; set; }

        [Column("szName")]
        public string Name { get; set; }
       
        public List<LedItem> LedItems { get; set; }
    }
}
