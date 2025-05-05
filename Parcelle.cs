public class Parcelle
{
    public int Id { get; set; }
    // public Plante? Plante { get; set; }

    public Parcelle(int id)
    {
        Id = id;
        //Plante = null;
    }

    public string AfficherContenu()
    {
        return ".";
        // Plante == null ? " . " : Plante.Symbole;
    }
}
