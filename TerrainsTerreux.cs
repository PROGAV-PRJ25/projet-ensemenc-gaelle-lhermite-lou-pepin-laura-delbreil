public class TerrainTerreux : Terrain // Classe qui hérite de terrain pour créer terrain argileux
{
    public TerrainTerreux(string nom, int nbParcelles) : base("Terre", nbParcelles)
    {
        Nom = nom;
        Couleur = "\x1b[48;5;94m"; // couleur spécifique au type de terrain
    }

}