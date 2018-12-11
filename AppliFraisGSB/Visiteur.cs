using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliFraisGSB
{
    class Visiteur : Personne
    {
        public Visiteur(int id, string name, string surname, string mail, string phone, string password, string sexe) : base(id, name, surname, mail, phone, password, sexe)
        {

        }
    }
}
