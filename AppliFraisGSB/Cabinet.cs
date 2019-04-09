using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliFraisGSB
{
    class Cabinet
    {
        public int IdCabinet { get; set; }
        public string Adresse { get; set;}
        public string Coordonnees { get; set; }

        public Cabinet(int IdCabinet, string adresse, string coordonnees)
        {
            this.IdCabinet = IdCabinet;
            this.Adresse = adresse;
            this.Coordonnees = coordonnees;
        }

        public Cabinet(string adresse, string coordonnees)
        {
            this.Adresse = adresse;
            this.Coordonnees = coordonnees;
        }
    }
}
