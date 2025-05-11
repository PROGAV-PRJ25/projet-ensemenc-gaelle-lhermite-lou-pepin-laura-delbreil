
public class TerrainTerreux : Terrain
{
    public TerrainTerreux(string nom, int nbParcelles) : base("Terre", nbParcelles)
    {
        Nom = nom;
        Couleur = "\x1b[48;5;94m"; // fond rouge foncé

    }

}