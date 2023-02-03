using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Models
{
    public class Carta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartaID { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Lema { get; set; }
        public bool Marcado { get; set; }

        public Carta() {
            this.Tablas = new HashSet<Tabla>();
        }
        public virtual ICollection<Tabla> Tablas { get; set; }
    }
}
