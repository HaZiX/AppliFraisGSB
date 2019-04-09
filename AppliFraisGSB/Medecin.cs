using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliFraisGSB
{
    class Medecin : Personne
    {
        public int IdCabinet { get; }
        public string dateNaissance { get; }
        public Medecin(int id, string name, string surname, string mail, string phone, string password, string sexe,string dateNaissance, int IdCabinet) : base(id, name, surname, mail, phone, password, sexe)
        {
            this.IdCabinet = IdCabinet;
            this.dateNaissance = dateNaissance;
        }
        public Medecin(string name, string surname, string mail, string phone, string password, string sexe, string dateNaissance, int IdCabinet) : base(name, surname, mail, phone, password, sexe)
        {
            this.IdCabinet = IdCabinet;
            this.dateNaissance = dateNaissance;
        }

    }
}
