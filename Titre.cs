/// <summary>
/// 
/// Classe qui gère l'affichage du menu principal du jeu ENSEMENCE
/// Affiche titre, permet de démarrer ou de lire règles, affiche menu animé
/// 
/// </summary>
public class JeuEnsemence
{
    // Affiche titre ascii et lance menu principal 
    public void LancerMenuPrincipal()
    {
        string[] optionsMenuPrincipal = { "COMMENCER", "RÈGLES DU JEU" };

        while (true)
        {
            int selection = AfficherMenu(optionsMenuPrincipal);

            if (selection == 0)
            {
                return;
            }
            else if (selection == 1)
            {
                AfficherRegles(); // Voir les règles
            }
        }
    }

    // Affiche titre + règles complètes du jeu, centrées à l'écran
    private void AfficherRegles()
    {
        Console.Clear();
        AfficherTitre();

        string[] lignesRegles =
        {
            "-- RÈGLES --",
            "",
            "Bienvenue dans ENSEMENCE, un jeu de gestion pas comme les autres,",
            "où vous incarnez le maître d’un jardin de plantes atypiques.",
            "Ce jardin, foisonnant de formes et de couleurs étranges, est le fruit d’une culture attentive,",
            "où chaque plante possède ses propres exigences, son rythme, et parfois… ses caprices.",
            "",
            "Votre mission ? Semer, soigner, protéger…",
            "mais surtout comprendre les besoins de vos plantes extraordinaires",
            "pour qu'elles s’épanouissent dans leur terrain préféré,",
            "sous un climat que vous devrez apprendre à dompter.",
            "",
            "Certaines plantes sont comestibles,",
            "d'autres ornementales ou carrément fantastiques.",
            "Elles ont toutes quelque chose d’unique.",
            "",
            "Mais attention : tout ne pousse pas dans la sérénité.",
            "",
            "Le jardin est régulièrement menacé par des intrus.",
            "Dans votre version du jeu, ces intrus prennent la forme de policiers,",
            "sortes d’ennemis qui pénètrent votre potager avec des intentions peu claires.",
            "",
            "Lorsqu’ils apparaissent, le jeu bascule en mode urgence.",
            "Vous devrez alors agir vite pour les repousser ou protéger vos cultures",
            "à l’aide de moyens détournés – jamais violents, mais ingénieux.",
            "",
            "Entre gestion stratégique, observation méticuleuse",
            "et réactions vives face à l’imprévu,",
            "ENSEMENCE vous plonge dans un monde à la fois tranquille et menaçant,",
            "féerique mais fragile.",
            "",
            "Prêt à plonger les mains dans la terre",
            "et à défendre votre royaume végétal ?"
        };

        foreach (string ligne in lignesRegles)
        {
            CentrerTexte(ligne);
        }

        Console.WriteLine();
        CentrerTexte("Appuyez sur une touche pour revenir au menu principal...");
        Console.ReadKey(true);
    }

    // Affiche menu vertical, navigation clavier, retourne index sélectionné
    private int AfficherMenu(string[] options)
    {
        int selection = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            AfficherTitre();
            Console.WriteLine();

            for (int i = 0; i < options.Length; i++)
            {
                if (i == selection)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    CentrerTexte(options[i]);
                    Console.ResetColor();
                }
                else
                {
                    CentrerTexte(options[i]);
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow)
                selection = (selection - 1 + options.Length) % options.Length;
            else if (key == ConsoleKey.DownArrow)
                selection = (selection + 1) % options.Length;

        } while (key != ConsoleKey.Enter);

        return selection;
    }

    // Affiche ascii art centré représentant ENSEMENCE
    public static void AfficherTitre()
    {
        string[] titre =
        {
            @"  ╔═══════════════════════════════════════════════════════════════════════════════════╗",
            @"  ║                                                                                   ║",
            @"  ║   ███████╗███╗   ██╗███████╗███████╗███╗   ███╗███████╗███╗   ██╗██████╗███████╗  ║",
            @"  ║   ██╔════╝████╗  ██║██╔════╝██╔════╝████╗ ████║██╔════╝████╗  ██║█╔════╝██╔════╝  ║",
            @"  ║   █████╗  ██╔██╗ ██║███████╗█████╗  ██╔████╔██║█████╗  ██╔██╗ ██║█║     █████╗    ║",
            @"  ║   ██╔══╝  ██║╚██╗██║     ██║██╔══╝  ██║╚██╔╝██║██╔══╝  ██║╚██╗██║█║     ██╔══╝    ║",
            @"  ║   ███████╗██║ ╚████║███████║███████╗██║ ╚═╝ ██║███████╗██║ ╚████║██████╗███████╗  ║",
            @"  ║   ╚══════╝╚═╝  ╚═══╝╚══════╝╚══════╝╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝╚═════╝╚══════╝  ║",
            @"  ║                                                                                   ║",
            @"  ╚═══════════════════════════════════════════════════════════════════════════════════╝"
        };

        foreach (string ligne in titre)
        {
            CentrerTexte(ligne);
        }
    }

    // Centre un texte en console selon largeur actuelle
    public static void CentrerTexte(string texte)
    {
        int largeurConsole = Console.WindowWidth;
        int longueurTexte = texte.Length;
        int espaces = Math.Max((largeurConsole - longueurTexte) / 2, 0);
        string texteCentre = new string(' ', espaces) + texte;
        Console.WriteLine(texteCentre);
    }
}
