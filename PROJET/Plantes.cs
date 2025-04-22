using System.Dynamic;


/// <summary>
/// Classe qui permet de définir les caractéristiques des plantes 
/// 
/// méthodes associées: 
///         public void Recolter(Inventaire inventaire) : 
///             méthode qui permet de récolter le sgraines de la plantes et les ajoute ds invenataire
///         public abstract void Pousser(float eau, float lumiere, float temperature, string typeTerrain);   
///             méthode qui fait pousser la plantes selon les critères de chaque plante
///         public abstract void Afficher();
///             affichage de chaque plante selon leur stade de croissance
///         public void AfficherMessages()
///             affichage des messages répétitif pour les plantes == récolte et mort
/// 
/// </summary>
public abstract class Plantes
    {
        public string Nom { get; protected set; }
        public bool EstComestible { get; protected set; }
        public string? TerrainPrefere { get; protected set; } // sable, terre, argile, cailloux
        public float Espacement { get; protected set; } // en cm
        public float PlaceNecessaire { get; protected set; } // en m²
        public float VitesseCroissance { get; protected set; } // en cm par semaine
        public float CroissanceActuelle { get; protected set; } // en cm par semaine
        public float BesoinEau { get; protected set; } // L par semaine
        public float BesoinLumiere { get; protected set; } // heures par jour
        public float TempPreferee { get; protected set; } // °C
        public float EsperanceDeVie { get; protected set; } // en semaines
        public int Fruits { get; protected set; } // nb de fruits et/ou légumes produits
        public float EtatSante { get; protected set; } // en pourcentage 
        public bool EstVivante {get; protected set; } //bool qui traite de la vie ou de la mort de la plante
        
        public abstract void Pousser(float eau, float lumiere, float temperature, string typeTerrain);
        public abstract void Afficher();
        
        public void Recolter(Inventaire inventaire)
        {
            if (!Estvivante)
            {
                Console.WriteLine($"{Nom} est morte, aucune récolte possible.");
                return;
            }

            Console.WriteLine($"Vous récoltez {Fruits} graines de {Nom}.");
            inventaire.AjouterGraines(Nom, Fruits);
        }

        public void AfficherMessages()
        {
             if (CroissanceActuelle > 75)
             {
             Console.WriteLine($"Vite c'est la récolte de {Nom}!");
             }
        }
    }