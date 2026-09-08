using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    [Header("Réglages")] public float tempsAvantEclosion = 5f;
    public float delaiAvantRamassable = 0.5f;
    public GameObject prefabEnemy;

    private bool estRamassable = false;

    void Start()
    {
        Invoke(nameof(Eclore), tempsAvantEclosion);
        Invoke(nameof(RendreRamassable), delaiAvantRamassable);
    }

    void RendreRamassable()
    {
        estRamassable = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!estRamassable)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {

            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AjouterPoints(ScoreManager.instance.pointsParOeufRamasse);
            }

            Destroy(gameObject);
        }
    }

    void Eclore()
    {
        Debug.Log("L'œuf éclot ! L'ennemi remonte sur une autruche.");
        Instantiate(prefabEnemy, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
