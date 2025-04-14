public class Hachich : Plante
    {
        private float croissanceActuelle = 0;
        private float age = 0;

        public Hachich()
        {
            Nom = "Hachich";
            EstVivace = true;
            EstComestible = true;
            TerrainPrefere = "terre";
            Espacement = 30;
            PlaceNecessaire = 0.5;
            VitesseCroissance = 5;
            BesoinEau = 2.0;
            BesoinLumiere = 8.0;
            TempPreferee = 25;
            EsperanceDeVie = 16;
            Rendement = 5;
        }

        public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain)
        {
            float score = 0;

            if (eau >= BesoinEau) score += 0.25;
            if (lumiere >= BesoinLumiere) score += 0.25;
            if (temperature >= TempPreferee-3 && temperature <= ZoneTempPreferee+5) score += 0.25;
            if (typeTerrain == TerrainPrefere) score += 0.25;

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
            age += 2;

            // Simuler une maladie aléatoire
            
        }

        public override void Afficher()
        {
            Console.WriteLine($"[{Nom}] Croissance: {croissanceActuelle} cm | Santé: {EtatSante}% | Âge: {age} sem");
        }
    }