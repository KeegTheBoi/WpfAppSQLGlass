using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppSQLGlass
{
    class Filtri
    {
        List<Studente> _studenti;

        public Filtri(List<Studente> studs)
        {
            _studenti = studs;
        }

        /// <summary>
        /// Ottiene una lista di Studenti maggiorenni
        /// </summary>
        /// <param name="Eta"></param>
        /// <returns></returns>
        public List<Studente> RicercaMaggiorenni()
        {
            List<Studente> studF18 = new List<Studente>();
            for (int i = 0; i < _studenti.Count; i++)
                if (_studenti[i].Anni >= 18)
                    studF18.Add(_studenti[i]);
            return studF18;
        }

        /// <summary>
        /// restituisce la somma dell'eta di tutti gli studenti presenti nella lista
        /// </summary>
        /// <returns></returns>
        public List<int> CalcolaMediaEta()
        {
            List<int> medie = new List<int>();
            float somma = 0;
            for (int i = 0; i < _studenti.Count; i++)
                somma += _studenti[i].Anni;
            medie.Add((int)somma / _studenti.Count);
            return medie;
        }

        /// <summary>
        /// Ottiene una lista di studenti con il nome passatoli nel parametro
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public List<Studente> RicercaPerNome(string nome)
        {
            List<Studente> studFNome = new List<Studente>();
            for (int i = 0; i < _studenti.Count; i++)
                if (_studenti[i].Nominativo == nome)
                    studFNome.Add(_studenti[i]);
            return studFNome;
        }

        /// <summary>
        /// Ottiene una lista di studenti con l'età passatoli nel parametro
        /// </summary>
        /// <param name="anni"></param>
        /// <returns></returns>
        public List<Studente> RicercaPerEta(int anni)
        {
            List<Studente> studFAnni = new List<Studente>();
            for (int i = 0; i < _studenti.Count; i++)
                if (_studenti[i].Anni == anni)
                    studFAnni.Add(_studenti[i]);
            return studFAnni;
        }


    }
}
