//Tests de fonctionnement des classes de temmporalité, avec ajout de temps
Temporalite temp1 = new Temporalite(DateOnly.Parse("2025-1-1")); //Parse présent pour permettre la récupération du string en format date
Meteo meteo = new Meteo();
Console.WriteLine(temp1); 

for (int i = 0; i<10; i++){
    temp1.AvancerTemps();
    meteo.GenererEvenement(temp1.SaisonActuelle);
    Console.WriteLine(meteo); 
    Console.WriteLine(temp1); 
}

//Même chose pour la tempo d'urgence
Temporalite temp2 = new TempoUrgence(temp1.DateActuelle); 
Console.WriteLine(temp2); 

for (int i = 0; i<4; i++){
    temp2.AvancerTemps(); 
    Console.WriteLine(temp2); 
}