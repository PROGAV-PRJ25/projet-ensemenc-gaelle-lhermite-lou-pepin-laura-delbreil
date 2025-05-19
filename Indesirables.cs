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

    public Indesirables(string nom, string icone, string solution, string iconeSolution, double probaApparition) 
    {
        Nom = nom;
        Icone = icone;
        Solution = solution; 
        IconeSolution = iconeSolution; 
        ProbaApparition = probaApparition; 
        LigneTerrain = random.Next(1,4); //!!!! > 4 à remplacer par nombre terrains +1
        ColonneActuelle = 0; //La colonne actuelle qui sera modifiée après 
        EstPresent = true;
    } 

    public void Deplacement(Temporalite temmporalite){
        if (ColonneActuelle=5){
            DiffusionPartout();//si tout le terrain a été touché par l'intrus ou la maladie, tous les terrains sont touchés par l'effet, l'indésirable est supprimé, et le joueur peut reprendre en mode classique
            temporalite.EtatUrgence = false;
            EstPresent = false;
        }
        else{
            ColonneActuelle ++;
            temporalite.EtatUrgence = true;  
        }
    }     

    public void DiffusionPartout(Temporalite temmporalite){
        //Effet appliqué partout
    }

    public void FairePartir(){
        if (Inventaire.objetSelectionne == Solution){
            EstPresent = false; 
            //Supprimer l'intru/la maladie du terrain
        }
    }
    
    public override string ToString()
    {
        return $"Nom : {Nom}, {Icone}, Solution pour l'enlever : {Solution}, Terrain n° {LigneTerrain} et Parcelle n° {ColonneActuelle + 1}"
    }
}



//Indesirables à créer dans temporalité (mais je ne modifie pas ce doc là car je ne suis pas à jour)
ListeIndesirables = new List<Indesirables>(); 

ListeIndesirables.Add(new Indesirables("Policier", "👮", "PistoletEau", "🔫"));
ListeIndesirables.Add(new Indesirables("Chien renifleur", "🐶", "Steak", "🥩"));
ListeIndesirables.Add(new Indesirables("Voleur adverse", "🕵️", "Cameras", "📸"));
ListeIndesirables.Add(new Indesirables("Rats", "🐀", "Fromage", "🧀"));
ListeIndesirables.Add(new Indesirables("Perroquet", "🦜", "Flute", "🪈"));

ListeIndesirables.Add(new Indesirables("Moisissures", "🧫", "VinaigreBlanc", "🍶"));
ListeIndesirables.Add(new Indesirables("Fusariose", "🦠", "CharbonActif", "🌑"));
ListeIndesirables.Add(new Indesirables("Oïdium", "🔬", "Ail", "🧄"));