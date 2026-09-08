using System.Collections;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("Invincibilité après réapparition")]
    public float dureeInvincibilite = 2f;
    public float vitesseClignotement = 0.15f;

    private bool estInvincible = false;
    private SpriteRenderer spriteRenderer;

    [Header("UI")] public UIManager uiManager;

    [Header("Type d'ennemi (si ce n'est pas le Player)")]
    public int pointsBonusSiEnnemiSpecial = 0;

    [Header("État")] public bool estVivant = true;

    [Header("Vies (uniquement pour le Player)")]
    public bool estLePlayer = false; 

    public int nombreDeVies = 3;
    public float delaiAvantReapparition = 2f;

    [Header("Œuf (uniquement pour l'Enemy)")]
    public GameObject prefabOeuf;

    private Vector3 positionDeDepart;

    void Start()
    {
        positionDeDepart = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (estLePlayer && uiManager != null)
        {
            uiManager.MettreAJourVies(nombreDeVies);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (estInvincible)
        {
            return;
        }

        CombatController autrePersonnage = collision.gameObject.GetComponent<CombatController>();

        if (autrePersonnage == null || !autrePersonnage.estVivant || !estVivant)
        {
            return;
        }

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
        transform.position = positionDeDepart;

        estVivant = true;
        gameObject.SetActive(true);

        Debug.Log("Le player réapparaît ! Vies restantes : " + nombreDeVies);

        estInvincible = true;
        StartCoroutine(ClignoterPendantInvincibilite());

        IEnumerator ClignoterPendantInvincibilite()
        {
            float tempsEcoule = 0f;

            while (tempsEcoule < dureeInvincibilite)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;

                yield return new WaitForSeconds(vitesseClignotement);
                tempsEcoule += vitesseClignotement;
            }
            
            spriteRenderer.enabled = true;
            estInvincible = false;
        }
    }
}
