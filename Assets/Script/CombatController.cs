using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("UI")]
    public UIManager uiManager;
    
    [Header("Type d'ennemi (si ce n'est pas le Player)")]
    public int pointsBonusSiEnnemiSpecial = 0; 
    
     [Header("État")]
    public bool estVivant = true;

    [Header("Vies (uniquement pour le Player)")]
    public bool estLePlayer = false;   // coche cette case dans l'Inspector pour le Player
    public int nombreDeVies = 3;
    public float delaiAvantReapparition = 2f;
    
    [Header("Œuf (uniquement pour l'Enemy)")]
    public GameObject prefabOeuf;

    private Vector3 positionDeDepart;

    void Start()
    {
        // On mémorise la position d'origine, pour savoir où réapparaître
        positionDeDepart = transform.position;
        if (estLePlayer && uiManager != null)
        {
            uiManager.MettreAJourVies(nombreDeVies);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CombatController autrePersonnage = collision.gameObject.GetComponent<CombatController>();

        if (autrePersonnage == null || !autrePersonnage.estVivant || !estVivant)
        {
            return;
        }

        // Un duel n'a lieu que si l'un des deux est le Player
        // (deux ennemis qui se touchent ne se battent pas entre eux)
        if (!estLePlayer && !autrePersonnage.estLePlayer)
        {
            return;
        }

        GererDuel(autrePersonnage);
    }

    void GererDuel(CombatController adversaire)
    {
        if (transform.position.y > adversaire.transform.position.y)
        {
            adversaire.Mourir();
        }
        else if (transform.position.y < adversaire.transform.position.y)
        {
            Mourir();
        }
    }

    public void Mourir()
    {
        estVivant = false;
        Debug.Log(gameObject.name + " est mort !");

        if (estLePlayer)
        {
            // Le player se désactive temporairement, il va réapparaître
            gameObject.SetActive(false);

            nombreDeVies--;
            if (uiManager != null)
            {
                uiManager.MettreAJourVies(nombreDeVies);
            }

            if (nombreDeVies > 0)
            {
                Invoke(nameof(Reapparaitre), delaiAvantReapparition);
            }
            else
            {
                Debug.Log("Game Over ! Plus de vies.");
                
                if (GameManager.instance != null)
                {
                    GameManager.instance.DeclencherGameOver();
                }
            }
        }
        else
        {
            // L'ennemi meurt : on ajoute les points
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AjouterPointsEnnemi(pointsBonusSiEnnemiSpecial);
            }

            if (prefabOeuf != null)
            {
                Instantiate(prefabOeuf, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    void Reapparaitre()
    {
        // On replace le player à sa position de départ
        transform.position = positionDeDepart;

        // On réactive tout
        estVivant = true;
        gameObject.SetActive(true);

        Debug.Log("Le player réapparaît ! Vies restantes : " + nombreDeVies);
    }
}
