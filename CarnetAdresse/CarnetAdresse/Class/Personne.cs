using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnetAdresse.Class
{
    class Personne
    {



        public Personne(string Nom, string Prenom, string Adresse,string EMail)
        {
            _nom = Nom;
            _prenom = Prenom;
            //_telephone = Telephone;
            _adresse = Adresse;
            _email = EMail;


        }
        private string _nom;
        private string _prenom;
        //private int _telephone;
        private string _adresse;
        private string _email;
        public string Nom
        {
            get
            {
                return _nom;
            }
            set
            {
                _nom = value;

            }
        }

        public string Prenom
        {
            get
            {
                return _prenom;
            }
            set
            {
                _prenom = value;

            }


        }

        //public int Telephone
        //{
        //    get
        //    {
        //        return _telephone;
        //    }
        //    set
        //    {
        //        _telephone = value;

        //    }
        //}

        public string Adresse
        {
            get
            {
                return _adresse;
            }
            set
            {
                _adresse = value;

            }
        }
        public string EMail
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;

            }
        }

    }
}
