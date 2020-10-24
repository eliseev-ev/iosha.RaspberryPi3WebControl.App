using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace iosha.RaspberryPi3WebControl.Storage.Entity
{
    [Table("tblLedItem")]
    public class LedItem
    {
        [Key]
        [Required]
        [Column("gId")]
        public Guid Id { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("nKey")]
        public int Key { get; set; }

        [Required]
        [Column("nPosition")]
        public int Position { get; set; }

        [Required]
        [Column("szColor")]
        protected string _color { get; set; }

        public Color Color
        {
            get
            {
                return System.Drawing.ColorTranslator.FromHtml(_color);
            }
            set
            {
                System.Drawing.ColorTranslator.ToHtml(value);
            }
        }
    }
}
