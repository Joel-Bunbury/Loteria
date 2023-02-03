using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Models
{
    public class Tabla
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TablaID { get; set; }
        public string Nombre { get; set; }

        public Tabla()
        {
            this.Cartas = new HashSet<Carta>();
        }
        public virtual ICollection<Carta> Cartas { get; set; }

        public virtual ICollection<Ganador> Ganadores { get; set; }
    }
}
