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
using CarnetAdresse.Class;
using System.Collections.ObjectModel; // vient d'ajouter pour afficher les changements dans les donnees



namespace CarnetAdresse
{
    // <summary>
    // Logique d'interaction pour MainWindow.xaml
    // </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Personne> listcontact = new ObservableCollection<Personne>(); // on remplacelistcontact par  observables collections pour afficher les changements dans les donnees
        public MainWindow()
        {
            InitializeComponent();

            dataGridCarnet.ItemsSource = listcontact;
            InterdireEdition();
            Sauvegarde.ChargerCsv(listcontact);// charger la liste de contacts
                                         // poueque la methosde chargerCSV apparait il faut aller dans la classe Sauvegarde et rend :(private static ObservableCollection<Personne> ChargerCsv) Public


        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)

        {
            listcontact.Add(new Personne(TextBoxNom.Text, TextBoxPrenom.Text, TextBoxAdresse.Text,TextBoxEMail.Text));



        }

        //private void dataGridCarnet_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    Personne selectedPersonne = dataGridCarnet.SelectedItem as Personne;

        //    if (selectedPersonne != null)
        //    {
        //        stackpanelEdit.DataContext = selectedPersonne;
        //    }
        //    else
        //    {
        //        stackpanelEdit.DataContext = null;

        //    }




        //}

        private void dataGridCarnet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Personne selectedPersonne = dataGridCarnet.SelectedItem as Personne;
            if (selectedPersonne != null)
            {
                stackpanelEdit.DataContext = selectedPersonne;
            }
            else
            {
                stackpanelEdit.DataContext = null;

            }

        }
        private void BtSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Personne selectedPersonne = dataGridCarnet.SelectedItem as Personne;

             if(selectedPersonne != null)
            {


                if (MessageBox.Show("Voulez vous Supprimer " + "  " + selectedPersonne.Nom + "  " + selectedPersonne.Prenom +"  " + selectedPersonne.Adresse + "  " + selectedPersonne.EMail + " ? ", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    listcontact.Remove(selectedPersonne);
                }

            }
             
        }

        private void BtEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            Personne newPersonne = stackpanelEdit.DataContext as Personne;

            if(newPersonne != null)
            {
                listcontact.Add(newPersonne);
                dataGridCarnet.SelectedItem = newPersonne;


                BtSupprimer.IsEnabled = true;
                BtEnregistrer.IsEnabled = false;
                BtAnnuler.Visibility = Visibility.Visible;
                BtEnregistrer.Focus();


            }
        }

        private void BtNouveau_Click(object sender, RoutedEventArgs e)
        {
            Personne newPersonne = new Personne(string.Empty, string.Empty, string.Empty,string.Empty);
            stackpanelEdit.DataContext = newPersonne;


            BtSupprimer.IsEnabled = false;
            BtEnregistrer.IsEnabled = true;
            BtAnnuler.Visibility = Visibility.Visible;

        }

        private void BtAnnuler_Click(object sender, RoutedEventArgs e)
        {
            stackpanelEdit.DataContext = null;

            BtSupprimer.IsEnabled = false;
            BtEnregistrer.IsEnabled = true;
            BtAnnuler.Visibility = Visibility.Collapsed;

        }
        private void autoriserEdition()
        {
            TextBoxNom.IsEnabled = true;
            TextBoxPrenom.IsEnabled = true;
            //TextBoxTelephone.IsEnabled = true;
            TextBoxAdresse.IsEnabled = true;
            TextBoxEMail.IsEnabled = true;
            TextBoxNom.Focus();



        }

        private void InterdireEdition()
        {
            TextBoxNom.IsEnabled = false;
            TextBoxPrenom.IsEnabled = false;
            //TextBoxTelephone.IsEnabled = false;
            TextBoxAdresse.IsEnabled = false;
            TextBoxEMail.IsEnabled = false;

        }

        private void stackpanelEdit_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(stackpanelEdit.DataContext == null)
            {
                InterdireEdition();

            }

            else
            {
                autoriserEdition();
            }
        }

        private void PrincipalWindows_Closing(object sender, System.ComponentModel.CancelEventArgs e) // je donne un nom au titre de ma fenetre :principalWindows,j'ai le mis a screencenter et via 
                                                                                                      // la methode closing j'ai cree cet evenement,cette methode Closing se declenche au moment ou la fenetre est ferme par l'user
                                                                                                      // sauvegarder mes donnees au moment de la fermeture de la fenetre de l'application  
        {
            Sauvegarde.SauvegarderCSV(listcontact); ///ici je donne listcontact comme paramete  et il conserver automatiquement dnas CSV
            // a quel moment je dois charger alors au lancement ,pour cela je dpis me rendre  a mon constructeur InterdireEdition ds(dataGridCarnet.ItemsSource = listcontact;)
        }



        //private void BoutonSupprimer_Click(object sender, RoutedEventArgs e)

        //{
        //    Personne selectedPersonne = dataGridCarnet.SelectedItem as Personne; // selectionnner l'enregistrement  qui a  ete selectionne par l'utilisateur

        //    if (selectedPersonne != null)

        //    {
        //        if (MessageBox.Show("voulez vous supprimer" + " " + selectedPersonne.Nom + "  " + selectedPersonne.Prenom + " ? ", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)

        //        {
        //            listcontact.Remove(selectedPersonne);
        //        }



    }

        }

        //private void BoutonNouveau_Click(object sender, RoutedEventArgs e)
        //{
        //    Personne newPersonne = new Personne(string.Empty, string.Empty);

        //    //BoutonSupprimer.IsEnabled = false; // desactive le bouton supprimer
        //    BoutonEnregistrer.IsEnabled = true; // active le bouton enregistrer
        //    BoutonAnnuler.Visibility = Visibility.Visible;

        //    stackpanelEdit.DataContext = newPersonne;  // ce qui explique le newpersonne est dans DataContext


        //}

         //private void BoutonEnregistrer_Click(object sender, RoutedEventArgs e)
         // {
         //     Personne newPersonne = stackpanelEdit.DataContext as Personne; //on le prend dans DataConcept
         //     if(newPersonne!=null)
         //     {
         //         listcontact.Add(newPersonne);

         //        // BoutonSupprimer.IsEnabled = true; // active le bouton supprimer
         //         BoutonEnregistrer.IsEnabled = false; // desactive le bouton enregistrer
         //         BoutonAnnuler.Visibility = Visibility.Collapsed;
         //         dataGridCarnet.SelectedItem = newPersonne;
         //         dataGridCarnet.Focus();

         //     }
         // }

          //private void BoutonAnnuler_Click(object sender, RoutedEventArgs e)
          //{
          //    stackpanelEdit.DataContext = null;
          //    //BoutonSupprimer.IsEnabled = true;
          //    BoutonEnregistrer.IsEnabled = false;

          //    BoutonAnnuler.Visibility = Visibility.Collapsed; // cela veut dir aie le bouton annler va disparaitre apres avoir annule la persone

          //}
        

 