public class TerrainArgileux : Terrain
{
    public TerrainArgileux(string nom, int nbParcelles) : base("Argile", nbParcelles)
    {
        Nom = nom;
        Couleur = "\x1b[48;5;124m";
    }
}
