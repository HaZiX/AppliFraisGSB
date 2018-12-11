using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliFraisGSB
{
    class Personne
    {
        public string name { get; }
        public string surname { get; }
        public string mail { get; }
        public string phone { get; }
        public string password { get; }
        public string sexe { get; }

        public Personne(string name, string surname, string mail, string phone, string password, string sexe)
        {
            this.name = name;
            this.surname = surname;
            this.mail = mail;
            this.phone = phone;
            this.password = password;
            this.sexe = sexe;
        }
    }
}
