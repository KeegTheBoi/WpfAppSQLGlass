using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfAppSQLGlass
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dtaGridStud.Visibility = Visibility.Hidden;
        }

        List<Studente> students = new List<Studente>();
        static int id;

        private void btnCreaStud_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int eta = int.Parse(txtBoxEta.Text);
                if(eta < 3 || eta > 70)
                    throw new ArgumentOutOfRangeException("Anni", eta, "Età inserita non accettabile");
                if (txtBoxNome.Text == "")
                    throw new FormatException("Inserire un nome perfavore.");
                Studente s = new Studente(++id, txtBoxNome.Text, eta);
                students.Add(s);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMostraGrid_Click(object sender, RoutedEventArgs e)
        {
            dtaGridStud.Visibility = Visibility.Visible;
            dtaGridStud.Items.Refresh();
            dtaGridStud.ItemsSource = students;
            DataGridColumn IDcol = dtaGridStud.Columns[0];
            IDcol.IsReadOnly = true;
            dtaGridStud.Columns[0].Width = 75;
            dtaGridStud.Columns[1].Width = 200;
            dtaGridStud.Columns[2].Width = 40;
            

        }

        private void txtBoxEta_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int result;
            if (!int.TryParse(e.Text, out result))
            {
                e.Handled = true;
            }
        }

        private void btnFiltra_Click(object sender, RoutedEventArgs e)
        {
            cmbRicerche.Items.Add("Ricerca Maggiorenni");
            cmbRicerche.Items.Add("Calcola Media");
            cmbRicerche.Items.Add("Ricerca Per Nome");
            cmbRicerche.Items.Add("Ricerca Per Età");
            AttivaMostraItems();
        }

        private void AttivaMostraItems()
        {
            btnEseguiAzioni.Visibility = Visibility.Visible; cmbRicerche.Visibility = Visibility.Visible;
             
        }


        private void btnEseguiAzioni_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Filtri sorts = new Filtri(students);
                switch (cmbRicerche.SelectedItem.ToString())
                {
                    case "Ricerca Maggiorenni":
                        dtaGridStud.ItemsSource = sorts.RicercaMaggiorenni();
                        break;
                    case "Calcola Media":
                        dtaGridStud.ItemsSource = sorts.CalcolaMediaEta();
                        break;
                    case "Ricerca Per Nome":                        
                        dtaGridStud.ItemsSource = sorts.RicercaPerNome(txtBoxNomeFiltered.Text);
                        break;
                    case "Ricerca Per Età":                       
                        dtaGridStud.ItemsSource = sorts.RicercaPerEta(int.Parse(txtBoxEtaFIltered.Text));
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCaricaStudentiFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> file = new List<string>();
                StreamReader sr = new StreamReader("Studenti.txt");
                while(!sr.EndOfStream)  
                    file.Add(sr.ReadLine());
                for(int i = 0; i < file.Count; i++)
                    students.Add(new Studente(++id, file[i].Split(' ')[0], int.Parse(file[i].Split(' ')[1])));
                btnCaricaStudentiFile.IsEnabled = false;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbRicerche_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(cmbRicerche.SelectedItem.ToString())
            {
                case "Ricerca Per Nome":
                    lblNomeRicerca.Visibility = Visibility.Visible;
                    txtBoxNomeFiltered.Visibility = Visibility.Visible;
                    lblEtaRIcerca.Visibility = Visibility.Hidden;
                    txtBoxEtaFIltered.Visibility = Visibility.Hidden;
                    break;
                case "Ricerca Per Età":
                    lblEtaRIcerca.Visibility = Visibility.Visible;
                    txtBoxEtaFIltered.Visibility = Visibility.Visible;
                    lblNomeRicerca.Visibility = Visibility.Hidden;
                    txtBoxNomeFiltered.Visibility = Visibility.Hidden;
                    break;
            }
        }
    }
}
