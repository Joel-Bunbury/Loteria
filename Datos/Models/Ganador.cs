using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Models
{
    public class Ganador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GanadorID { get; set; }
        public DateTime FechaHora { get; set; }
        public int TablaID { get; set; }
        [ForeignKey("TablaID")]
        public virtual Tabla Tabla { get; set; }
    }
}
