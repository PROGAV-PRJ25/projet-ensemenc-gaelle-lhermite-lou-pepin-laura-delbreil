//Tests de fonctionnement des classes de temmporalité, avec ajout de temps
Temporalite temp1 = new Temporalite(DateOnly.Parse("2025-1-1")); //Parse présent pour permettre la récupération du string en format date
Meteo meteo = new Meteo();

for (int i = 0; i<25; i++){
    meteo.GenererEvenement(temp1.SaisonActuelle, temp1); //on génére un événement météo en fonction de la saison et de la temporalité dans laquelle on est
    if (temp1.EtatUrgence && temp1.GetType() != typeof(TempoUrgence)) // si EtatUrgence est vrai mais que l'on est pas en état d'urgence, on passe en mode urgence
        {
            temp1 = new TempoUrgence(temp1.DateActuelle){ //on récupère les informations des saisons de temp1
                Ete = temp1.Ete,
                Automne = temp1.Automne,
                Hiver = temp1.Hiver,
                Printemps = temp1.Printemps,
            }; 
        }
     else if (temp1.EtatUrgence == false && temp1.GetType() == typeof(TempoUrgence)) // si EtatUrgence est faux mais que l'on est en état d'urgence, on passe en mode normal
        {
            temp1 = new Temporalite(temp1.DateActuelle){ //on récupère les informations des saisons de temp1
                Ete = temp1.Ete,
                Automne = temp1.Automne,
                Hiver = temp1.Hiver,
                Printemps = temp1.Printemps
            }; 
        }

    if (temp1.GetType() == typeof(TempoUrgence)){ //Pour afficher le mode dans lequel on est 
        Console.WriteLine("MODE URGENCE");
        meteo.ModifierValeursSaison(temp1.SaisonActuelle); //Lorsqu'on est en mode urgence, on modifie les valeurs des saisons
    }
    else{
        Console.WriteLine("MODE NORMAL"); 
    }
    Console.WriteLine();
    Console.WriteLine(temp1);
    Console.WriteLine(temp1.SaisonActuelle); // Pour vérifier la bonne modification des valeurs 
    Console.WriteLine(meteo);
    Console.WriteLine(); 
    temp1.AvancerTemps();
}
