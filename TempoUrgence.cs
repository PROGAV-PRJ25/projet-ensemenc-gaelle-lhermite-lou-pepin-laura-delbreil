/// <summary>
/// 
/// Classe pr gérer un mode spécial d'urgence météo (grêle, intrus, canicule...)
/// Hérite de Temporalite mais avec avancée jour par jour (et non 2 semaines)
/// 
/// </summary>
public class TempoUrgence : Temporalite //Heritage de la classe temporalité pour créer une temporalité en cas d'urgence
 {
    // init mode urgence à partir d'une date existante
    // Redéfinit avance du temps : ici avance d'1 jour au lieu de 14
    public TempoUrgence(DateOnly dateDebut, int sautsTemps = 1) : base(dateDebut, sautsTemps) //saut de 1 jour par défaut en mode urgence
    {
        DateDebut = dateDebut;
        SautsTemps = sautsTemps;
    }

    public override string ToString() //Affichage de la présence d'un problème
    {
        return $"URGENCE LA TEAM IL Y A UN PROBLEME !! Les jours passent et vous devez résoudre ce problème...\nNous sommes actuellement le {DateActuelle}. Nous sommes en cette saison : {SaisonActuelle.Nom}"; 
    }
} 
