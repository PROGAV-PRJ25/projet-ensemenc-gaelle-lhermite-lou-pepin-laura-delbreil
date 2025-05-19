
public class TerrainSableux : Terrain
{
    public TerrainSableux(string nom, int nbParcelles) : base("Sable", nbParcelles)
    {
        Nom = nom;
        Couleur = "\x1b[48;5;136m"; // fond rouge foncé

    }

}