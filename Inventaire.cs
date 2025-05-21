///
/// 
/// Classe pour gérer l'inventaire (acheter, sélectionner ou utiliser un objet)
/// 
/// 
/// A faire potentiellement : Solution.cs avec les objets achetables
public class Inventaire
{
    public string ObjetSelectionne{get; set;} //Objet sélectionné par l'utilisateur
    public List<Solutions> ObjetsPossedes { get; set;}
    public Solutions Solution { get; set; } //Liste des objets possédés par le joueur

    public Inventaire(){
        ObjetSelectionne = null;
        Solution = new Solutions("", "");
        ObjetsPossedes = Solution.GenererSolutions(); 
    }

    //Fct pour sélectionner un objet à utiliser dans l'inventaire
    public void SelectionnerObjet(string objet)
    {
        for (int i = 0; i<ObjetsPossedes.Count; i++){
            if (ObjetsPossedes[i].Nom == objet){
                ObjetSelectionne = objet;
            }
        }
    }

    //Fct pour utiliser un objet sur un indésirable
    public void UtiliserObjet(string objet)
    {
        if (objet != null){
            for (int i = 0; i<ObjetsPossedes.Count; i++){
                if (ObjetsPossedes[i].Nom == objet){
                    ObjetSelectionne = null;
                }
            }
        }
    }
    
}
