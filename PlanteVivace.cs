// public abstract class PlanteVivace : Plantes
// {
//     protected float age = 0;
//     protected float croissanceActuelle = 0;
//     protected bool estEnDormance = false;

//     protected DateOnly DatePlantation; // Mémorise date initiale
//     protected int NombreDeRevivals = 0; // Limite les cycles à 1 ou 2 max

//     public void InitialiserPlantation(DateOnly date)
//     {
//         DatePlantation = date;
//     }

//     // Ajout de dateActuelle et d'un booléen "emplacementDisponible" passé par le gestionnaire de grille
//     public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain, DateOnly dateActuelle, bool emplacementDisponible)
//     {
//         // Début de dormance si espérance de vie atteinte
//         if (!estEnDormance && age >= EsperanceDeVie)
//         {
//             estEnDormance = true;
//             croissanceActuelle = 0;
//             Console.WriteLine($"{Nom} entre en dormance.");
//             return;
//         }

//         // Tentative de réveil un an après la plantation
//         if (estEnDormance && dateActuelle >= DatePlantation.AddYears(1))
//         {
//             if (emplacementDisponible)
//             {
//                 // Se replante
//                 age = 0;
//                 croissanceActuelle = 0;
//                 estEnDormance = false;
//                 EstVivante = true;
//                 EtatSante = 0.5f;
//                 NombreDeRevivals++;
//                 DatePlantation = dateActuelle;
//                 Console.WriteLine($"{Nom} repousse automatiquement.");
//             }
//             else
//             {
//                 EstVivante = false;
//                 estEnDormance = false;
//                 EtatSante = 0;
//                 Console.WriteLine($"{Nom} ne peut pas repousser : emplacement occupé → mort définitive.");
//             }
//             return;
//         }

//         // Si en dormance, ne pousse pas
//         if (estEnDormance) return;

//         // Sinon, pousse normalement
//         if (eau >= BesoinEau) EtatSante += 0.01f;
//         if (lumiere >= BesoinLumiere) EtatSante += 0.01f;
//         if (temperature >= TempPreferee - 3 && temperature <= TempPreferee + 5) EtatSante += 0.01f;
//         if (typeTerrain == TerrainPrefere) EtatSante += 0.01f;

//         if (EtatSante > 1.0f) EtatSante = 1.0f;

//         if (EtatSante < 0.5f)
//         {
//             EstVivante = false;
//             EtatSante = 0.0f;
//             Console.WriteLine($"{Nom} est trop faible et meurt temporairement.");
//             return;
//         }

//         croissanceActuelle += VitesseCroissance * 4 * EtatSante;
//         if (croissanceActuelle > 100f) croissanceActuelle = 100f;
//         age += 2; // 2 semaines par tour
//     }
// }
