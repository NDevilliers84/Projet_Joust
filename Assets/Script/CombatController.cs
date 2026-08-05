using UnityEngine;

public class CombatController : MonoBehaviour
{
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

            if (nombreDeVies > 0)
            {
                Invoke(nameof(Reapparaitre), delaiAvantReapparition);
            }
            else
            {
                Debug.Log("Game Over ! Plus de vies.");
            }
        }
        else
        {
            // L'ennemi meurt pour de bon : on fait apparaître l'œuf, puis on détruit vraiment l'objet
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
