using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; // pour les observablesCollection
using System.IO; //pour la gestion et le traitement des donnees fichieers et  repertoires

namespace CarnetAdresse.Class  
{
   static class Sauvegarde  // une classe static n'a pas de constructeur et n'est pas instancie ,on peut aussi l'utiliser,pour appeler toutes les methoses qui sont aussi static qui vont aussi crees des actions.
                            // dans une classe tatic tout doit etre static: variables,procedures,fonctions


    {
        // l'endroit ou on va stocker le fichier dans un dossier speciale dans le dossier rumine ou se trouve tous les users de windows
        // ,mais dans le dossier Appdata/Rumine qui est un dossier cache.ce chemin est valble pour stockage de fichiers,,ce arobase @ c'est a cause de l'antiflash 
        // qui est aussi considere comme un caractere d'echappement,,alors antiflash ici est que pour les chaines de caRacteres,non pour 1 caractere d'echappement

                                                 // Declarations  DES 3 Variables.

     static private string _dossier = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CarnetAdresse\";// le chemin complet du dossier(repertoire) qui sappelle CarnetAdresse

    static private string _fichier= "contacts.csv"; // le fichier qui va etre dans le dossier, ce fichier va contenir tous les contacts sauvargder

                                                    //   static private string _chemincomplet = _dossier + _fichier; ou d'une maniere plus propre et qui est pour sa

     static private string _chemincomplet = Path.Combine(_dossier + _fichier); // path .combine  est fait pour cela il va bien combiner  dossier et fichier tout en traitant les eventueles erreurs

                                                        // DECLARATIONs DE ce methode pour sauvgarder le fichier CSV

        public static void SauvegarderCSV(ObservableCollection<Personne> contacts) //cette methode est pour sauvgarder les donnees ,(public)elle doit etre acccessible a l'exterieur, 
                                                                                   // (on estdans une classe static)on lui passe  un parametrepour savoir ce qu'on veut sauvagrder 
                                                                                   // on lui passe :observablescollection,parce que nos contacts sont stocke dans un observablescollections
                                                                                   //observablescollections de type personnes ,et on l'appelle contacs : ce sont nos contacts

        {
                                      // verifions que le repertoire CarnetAdresse existe bien dans le dossier Appdata,sinon cela creera une erreur
                if(!Directory.Exists(_dossier))  // si directory n'existe pas
                                                                                        // on met ici la variable dossier ,parce que le chemin du dossier est dans la variable dossier
            {
                Directory.CreateDirectory(_dossier);  // cree directory
            }

            //StringBuilder permet dutiliser une methode qui sappelle apend qui permet dajouter des chaines de caracteres bout a bout et demande a stringbilder de construire notre ligne
            StringBuilder lignea = new StringBuilder();

            using (StreamWriter fich = new StreamWriter(_chemincomplet,false,Encoding.UTF8, 1024)) 
            //using est automatiquement tres ineressant ,il permet d'utiliser la methode dispose  pour liberer la memoire qu'il a utilise.
             //donc lorsque vous avez un objet disposebale ou qui a une methode dispose, eh bien using doit etre utilise,on est pas oblige  mais c'est conseille,c'est bien pour liberer la memoire
             //la methode apend  ajout des donnes a cotes des donnes deja existantes,la ici on va faire quil ecrit la totalite des donness,,encodage utf8 est utilise,pas deproble pour les accents
             // le buffer est une reservation de donnees,il le conserve la dedans et apres le reeecrire sur le disque ,plus le buffer est petit en taille c'est mieux,il ne bouffe pas trop de memoire


            {
            foreach(Personne personne in contacts) // pour chaque personne qui se trouve dans contacs fait quelque chose le contacts c'est celui en parametre en hautdans la methode'
                                                  // ce boucle va tourner tant qu'il aura des contacts dans cette collection nommes contacts
                                                  
                {
                    lignea.Append(personne.Nom + " ? "); // la methode append va ajouter au bout de la ligne chacun des champs de ma personne
                    lignea.Append(personne.Prenom + " ? "); // Notez bien tout est construit avec le strenbilder qui sappelle ligne
                    lignea.Append(personne.Adresse + " ? ");
                    lignea.Append(personne.EMail + " ? ");
                    // donc ecrire ce ligne dans mon fichier enutilisant le stremwriter (fich en appliquant sa methode writeline qui permet d'ecrire dans le fichier)
                    // utilisant aussnt auusi le strembilder(ligne) qui va construire la ligne    
                    fich.WriteLine(lignea.ToString()); // ligne ecrite

                    lignea.Clear(); // pour vider ma strembilder pour liberer la memoire de cette  ligne
                }
                // TOUTES CES METHODES se fait automatiquement a la fin de USING( using (StreamWriter fich = new StreamWriter(_chemincomplet,false,Encoding.UTF8, 1024))
                // il existe aussi la methode Flush() qui permet de vider la memoire tampon ou buffer qu'on avait mis a 1024 c'est.fich.Flush(): pour lecrire immediatement sur le disque
                //La methode Close() qui permet de fermer le fichier ,mais el appelle ausii la methode flush-fich.Close();
                // la methode Dispose() permet de liberer toutes les donnees qui ont ete stockes dans la memoire par lobjet stremwriter  fich.Dispose();


            }
        }

        //Cette Fonction va nous permettre de remplir  lobjet Contacts a partir du fichier CSVet nous la retourner
       public static ObservableCollection<Personne> ChargerCsv (ObservableCollection<Personne> contacts) // on ne et pas void paceque  c'est une fonction,qui va nous retourner une observerbalecollection de <personne> 
                                                                                                           // la methode sapel chargerCSV et l'objet a remplir est(observablecollections<personne>) 
                               
        {
            if (File.Exists(_chemincomplet))
            {
                //alors il va executer le return contacts ...,,tout en bas....le accolade de fermeture est tout en bas.



                using (StreamReader fichier = new StreamReader(_chemincomplet, Encoding.UTF8))

                {

                    string lignea;  // pourstocker les lignes dufichier CSV

                    while ((lignea = fichier.ReadLine()) != null) // tant qu'il aura des lignes dans le fichesCSV stoke les dans la variable ligne1
                    {
                        if (!string.IsNullOrWhiteSpace(lignea)) // ctte methode c'est pour savoir que si notre chaine de caractere est completemnet vide ou contient des espaces vides
                        {
                            //alors il faut s'assurer qu'il ny apèas de chaines vides pour ne pas palanquer l'application



                            Personne personne = new Personne("Nom", "Prenom", "Adresse", "EMail");// pou chaque ligne trouve dans lobjet CSV, il fo que je cree un objet Personne et le stocke dans ma liste de contact
                                                                                                  //ligne1.Split(';'); //Split estla methode la plus basique pour faire une liste // 
                                                                                                  // si j'avais voulu modifier je ferai ceci:List<string> champs2 =ligne1.split(';').Tolist(): on voit on peut convertir un Array en autre type de list


                            string[] champs = lignea.Split(';');   // je cree un tableau qui s'appelle champs il va stocker tous les champs separes  par des points virgules dans le tableau

                            personne.Nom = champs[0];  // qui est la premiere ligne dans le tableau  // extraire chaque champs de ma ligne pour les remettre dans mon objet personne
                            personne.Prenom = champs[1];
                            personne.Adresse = champs[2];
                            personne.EMail = champs[3];

                            contacts.Add(personne); // remettre l'objet personne dans ma liste de contacts

                        }

                    }

                }
                return contacts;
            }
        }
    }

}
