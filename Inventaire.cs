///
/// 
/// Classe pour gérer l'inventaire (acheter, sélectionner ou utiliser un objet)
/// 
/// 
/// A faire potentiellement : Solution.cs avec les objets achetables
public class Inventaire
{
    public string ObjetSelectionne{get; set;} //Objet sélectionné par l'utilisateur
    public List<string> ObjetsPossedes {get; set;} //Liste des objets possédés par le joueur

    public Inventaire(){
        ObjetSelectionne = null;
        ObjetsPossedes = new List<string>();
    }

    //Fct pour acheter un objet
    public void AcheterObjet(string objet){
        ObjetsPossedes.Add(objet);
    }

    //Fct pour sélectionner un objet à utiliser dans l'inventaire
    public void SelectionnerObjet(string objet)
    {
        for (int i = 0; i<ObjetsPossedes.Count; i++){
            if (ObjetsPossedes[i] == objet){
                ObjetSelectionne = objet;
            }
        }
    }

    //Fct pour utiliser un objet et le supprimer de l'inventaire
    public void UtiliserObjet(string objet)
    {
        if (objet != null){
            for (int i = 0; i<ObjetsPossedes.Count; i++){
                if (ObjetsPossedes[i] == objet){
                    ObjetsPossedes.Remove(objet);
                    ObjetSelectionne = null;
                }
            }
        }
    }
    
}
