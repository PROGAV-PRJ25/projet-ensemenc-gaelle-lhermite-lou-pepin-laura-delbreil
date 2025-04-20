public class Hachich : Plantes
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
            PlaceNecessaire = 0.5f;
            VitesseCroissance = 5;
            BesoinEau = 2.0f;
            BesoinLumiere = 8.0f;
            TempPreferee = 25;
            EsperanceDeVie = 30;
            Fruits = 5;
            EtatSante = 0.50f;
        }

        public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain)
        {
            
            if (eau >= BesoinEau) EtatSante += 0.25f;
            if (lumiere >= BesoinLumiere) EtatSante += 0.25f;
            if (temperature >= TempPreferee-3 && temperature <= TempPreferee+5) EtatSante += 0.25f;
            if (typeTerrain == TerrainPrefere) EtatSante += 0.25f;


            if ((EtatSante < 0.5)|| (age>EsperanceDeVie))// si plante inférieure à 50 elle meurt ou si son espérance de vie est atteinte
            {
                    Console.WriteLine($"{Nom} est morte.");
                    return;
            }

            croissanceActuelle += VitesseCroissance * EtatSante;
            age += 2; //on augmente de 2 car saut de deux semaines dans le temps

            if (croissanceActuelle == 0)
            {
                Console.WriteLine("Vous venez de planter du HACHICH: . ");
            }

            if ((croissanceActuelle>0)&&(croissanceActuelle<20))
            {
                Console.WriteLine("🌱");
            }

            if ((croissanceActuelle>20)&&(croissanceActuelle<40))
            {
                Console.WriteLine("🌿");
            }

            if ((croissanceActuelle>40)&&(croissanceActuelle<60))
            {
                Console.WriteLine("🥦");
            }

             if ((croissanceActuelle>40)&&(croissanceActuelle<80))
            {
                 Console.WriteLine("C'est la récolte!");
                 Console.WriteLine("🍃");
            }

            if ((croissanceActuelle>80)&&(croissanceActuelle<100))
            {
                 Console.WriteLine("Vite vite vite!!!!! ");
                 Console.WriteLine("🍂");
            }

            if ((croissanceActuelle==100))
            {
                 Console.WriteLine("Hachich a fini son cycle de vie");
                 Console.WriteLine("🪦");
            }


            // Simuler une maladie aléatoire
            
        }

        public override void Afficher()
        {
            Console.WriteLine($"[{Nom}] Croissance: {croissanceActuelle} cm | Santé: {EtatSante}% | Âge: {age} sem");
        }
    }