 public class TempoUrgence : Temporalite //Heritage de la classe temporalité pour créer une temporalité en cas d'urgence
 {
    public TempoUrgence(DateOnly dateDebut, int sautsTemps=1):base(dateDebut, sautsTemps) //saut de 1 jour par défaut en mode urgence
    {
        DateDebut = dateDebut; 
        SautsTemps = sautsTemps; 
    }

    public override string ToString() //Affichage de la présence d'un problème
    {
        return $"URGENCE LA TEAM IL Y A UN PROBLEME !! Les jours passent et vous devez résoudre ce problème...\nNous sommes actuellement le {DateActuelle}. Nous sommes en cette saison : {SaisonActuelle.Nom}"; 
    }
} 
