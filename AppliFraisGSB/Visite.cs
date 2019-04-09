using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliFraisGSB
{
    class Visite
    {
        public int id { get; }
        public bool estRdv { get; }
        public string heureDebut { get; }
        public string heureDepart { get; }
        public int id_medecin { get; }
        public int id_visiteur { get; }
        public Visite(int id, bool estRdv, string heureDebut, string heureDepart, int id_medecin, int id_visiteur)
        {
            this.id = id;
            this.estRdv = estRdv;
            this.heureDebut = heureDebut;
            this.heureDepart = heureDepart;
            this.id_medecin = id_medecin;
            this.id_visiteur = id_visiteur;
        }
        public Visite(bool estRdv, string heureDebut, string heureDepart, int id_medecin, int id_visiteur)
        {
            this.estRdv = estRdv;
            this.heureDebut = heureDebut;
            this.heureDepart = heureDepart;
            this.id_medecin = id_medecin;
            this.id_visiteur = id_visiteur;
        }

    }
}
