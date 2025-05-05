public class Hachich : Plante
    {
        private float croissanceActuelle = 0;
        private float age = 0;

        public Opium()
        {
            Nom = "Opium";
            EstVivace = true;
            EstComestible = true;
            TerrainPrefere = "terre";
            Espacement = 20;
            PlaceNecessaire = 0.7;
            VitesseCroissance = 8;
            BesoinEau = 3.0;
            BesoinLumiere = 6.0;
            TempPreferee = 27;
            EsperanceDeVie = 15;
            Rendement = 2;
        }

        public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain)
        {
            float score = 0;

            if (eau >= BesoinEau) score += 0.02;
            if (lumiere >= BesoinLumiere) score += 0.1;
            if (temperature >= TempPreferee-3 && temperature <= ZoneTempPreferee+5) score += 0.06;
            if (typeTerrain == TerrainPrefere) score += 0.3;

            if (score < 0.5) // si plante inférieure à 50 elle meurt
            {
                EtatSante -= 10;
                if (EtatSante <= 0)
                {
                    Console.WriteLine($"{Nom} est morte.");
                    return;
                }
            }

            croissanceActuelle += VitesseCroissance * score;
            age += 2; //on augmente de 2 car saut de deux semaines dans le temps

            // Simuler une maladie aléatoire
            
        }

        public override void Afficher()
        {
            Console.WriteLine($"[{Nom}] Croissance: {croissanceActuelle} cm | Santé: {EtatSante}% | Âge: {age} sem");
        }
    }