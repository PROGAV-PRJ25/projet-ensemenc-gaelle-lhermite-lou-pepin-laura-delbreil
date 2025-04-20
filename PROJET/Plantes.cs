public abstract class Plantes
    {
        public string? Nom { get; protected set; }
        public bool EstVivace { get; protected set; } // false = annuelle
        public bool EstComestible { get; protected set; }
        public string? TerrainPrefere { get; protected set; } // sable, terre, argile, cailloux
        public float Espacement { get; protected set; } // en cm
        public float PlaceNecessaire { get; protected set; } // en m²
        public float VitesseCroissance { get; protected set; } // en cm par semaine
        public float BesoinEau { get; protected set; } // L par semaine
        public float BesoinLumiere { get; protected set; } // heures par jour
        public float TempPreferee { get; protected set; } // °C
        public float EsperanceDeVie { get; protected set; } // en semaines
        public int Fruits { get; protected set; } // nb de fruits et/ou légumes produits
        public float EtatSante { get; protected set; } // en pourcentage
        
        public abstract void Pousser(float eau, float lumiere, float temperature, string typeTerrain);
        public abstract void Afficher();
    }