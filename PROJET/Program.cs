//Tests de fonctionnement des classes de temmporalité, avec ajout de temps
Temporalite temp1 = new Temporalite(DateOnly.Parse("2025-1-1")); //Parse présent pour permettre la récupération du string en format date

Console.WriteLine(temp1); 

for (int i = 0; i<4; i++){
    temp1.AvancerTemps(); 
    Console.WriteLine(temp1); 
}

//Même chose pour la tempo d'urgence
Temporalite temp2 = new TempoUrgence(DateOnly.Parse("2025-1-1")); 
Console.WriteLine(temp2); 

for (int i = 0; i<4; i++){
    temp2.AvancerTemps(); 
    Console.WriteLine(temp2); 
}