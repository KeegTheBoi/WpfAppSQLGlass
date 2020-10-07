using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAppSQLGlass
{
    class Studente
    {
        public Studente(int key, string nominativo, int eta)
        {
            IDStudente = key;
            Anni = eta;
            Nominativo = nominativo;
        }

        public int IDStudente { get; set; }
        public string Nominativo { get; set; }
        public int Anni { get; set; }

        
    }
}
