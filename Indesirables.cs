///
/// 
///  Classe pour gérer les indésirables (apparition, mouvement, effet sur les plantes faire partir)
/// 
/// 

public class Indesirables
{
    public string Nom {get; set;}
    public string Icone {get; set;}
    public string Solution {get; set;}
    public string IconeSolution {get; set;}
    public string ProbaApparition {get; set;}
    public int LigneTerrain {get; set;}
    public int ColonneActuelle {get; set;}
    public bool EstPresent{get; set;} 
    public Random random = new Random();
    public int NbTerrains {get; set;}
    public bool EstVivante {get;set;}

    public Indesirables(string nom, string icone, string solution, string iconeSolution, double probaApparition, Menu menu, Plantes plante) 
    {
        Nom = nom;
        Icone = icone;
        Solution = solution; 
        IconeSolution = iconeSolution; 
        ProbaApparition = probaApparition;
        NbTerrains = menu.NbTerrains; //Nombre de terrains récupéré dans le menu lors du choix utilisateur
        LigneTerrain = random.Next(0,NbTerrains+1); // Ligne d'apparition = terrain d'apparition qui peut être entre le premier terrain (indice 0) et le dernier terrain
        ColonneActuelle = 0; //La colonne actuelle qui sera modifiée après 
        EstPresent = true; //L'indésirable est présent sur le terrain 
        EstVivante = plante.EstVivante; //Récupération de l'état de vie d'une plante
    } 

    //Fct pour faire apparaître les indésirables selon leur proba d'apparition
    public void Apparition(){

    }
    //Fct pour que les indésirables se déplacent/se diffusent sur toutes les cases d'un terrain
    public void Deplacement(Temporalite temmporalite, Jardin jardin){
        Plantes? plante = jardin.GetPlante(LigneTerrain, ColonneActuelle); //Récupération de la plante présente sur la case de l'indésirable

        if (plante != null && EstVivante == true) //si elle est en vie et que la case n'est pas vide, on tue cette plante
        {
            EstVivante = false;
        }
        if (ColonneActuelle==5){
            DiffusionPartout(jardin);//si tout le terrain a été touché par l'intrus ou la maladie, tous les terrains sont touchés par l'effet, l'indésirable est supprimé, et le joueur peut reprendre en mode classique
            temporalite.EtatUrgence = false; //On est plus en état d'urgence car tout a disparu
            EstPresent = false; //Indésirable disparait
        }
        else{
            ColonneActuelle ++; //L'indésirable continue d'avancer pour détruire les plantes
            temporalite.EtatUrgence = true;  //On est toujours en état d'urgence
        }
    }     

    //Fct qui permet de tuer les plantes sur tous les terrains si l'indésirable n'a pas été supprimé lorsqu'il s'est déplacé sur les 6 cases du terrain 
    public void DiffusionPartout(Jardin jardin)
    {
    for (int ligne = 0; ligne < jardin.Terrains.Length; ligne++)
    {
        for (int colonne = 0; colonne < 6; colonne++) //on a colonne = {0,5} sur chacun de nos terrains
        {
            Plantes? plante = jardin.GetPlante(ligne, colonne);
            if (plante != null)
            {
                plante.EstVivante = false;
            }
        }
    }
    EstPresent = false;
}

    //Fct pour faire partir l'indésirable en utilisant un objet
    public void FairePartir(Inventaire inventaire){
        if (inventaire.ObjetSelectionne == Solution){ //si l'objet sélectionné est la solution pour se débarasser de cet indésirable, alors on l'utilise sur lui et on fait disparaitre l'indésirable
            inventaire.UtiliserObjet(inventaire.ObjetSelectionne); //Appel d'une méthode pour utiliser l'objet présente dans inventaire
            EstPresent = false; //Indésirable n'est plus présent 
            Console.WriteLine("Bravo vous avez fait partir l'indésirable !!");
        }
        else{
            Console.WriteLine("Raté... cet objet ne vous sera d'aucune utilité...");
        }
    }
    
    public override string ToString()
    {
        return $"Nom : {Nom}, {Icone}, Solution pour l'enlever : {Solution}, Terrain n° {LigneTerrain} et Parcelle n° {ColonneActuelle + 1}";
    }
}



//Indesirables à créer dans temporalité (mais je ne modifie pas ce doc là car je ne suis pas à jour)
ListeIndesirables = new List<Indesirables>(); 

ListeIndesirables.Add(new Indesirables("Policier", "👮", "PistoletEau", "🔫", 0.1));
ListeIndesirables.Add(new Indesirables("Chien renifleur", "🐶", "Steak", "🥩", 0.1));
ListeIndesirables.Add(new Indesirables("Voleur adverse", "🕵️", "Cameras", "📸", 0.1));
ListeIndesirables.Add(new Indesirables("Rats", "🐀", "Fromage", "🧀", 0.1));
ListeIndesirables.Add(new Indesirables("Perroquet", "🦜", "Flute", "🪈", 0.1));

ListeIndesirables.Add(new Indesirables("Moisissures", "🧫", "VinaigreBlanc", "🍶", 0.1));
ListeIndesirables.Add(new Indesirables("Fusariose", "🦠", "CharbonActif", "🌑", 0.1));
ListeIndesirables.Add(new Indesirables("Oïdium", "🔬", "Ail", "🧄", 0.1));