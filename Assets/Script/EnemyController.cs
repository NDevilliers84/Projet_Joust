using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Réglages du mouvement")]
    public float vitesseDeplacement = 3f;
    public float intervalleChangementDirection = 2f;
    public float vitesseDeRotation = 3f;   // vitesse à laquelle l'ennemi tourne vers sa nouvelle direction

    [Header("Poursuite du player")]
    [Range(0f, 1f)]
    public float agressivite = 0.6f;

    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 directionCible;
    private float timerChangementDirection;
    private Transform player;
    
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject objetPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objetPlayer != null)
        {
            player = objetPlayer.transform;
        }

        ChoisirNouvelleDirection();
        direction = directionCible;
    }

    void Update()
    {
        timerChangementDirection += Time.deltaTime;

        if (timerChangementDirection >= intervalleChangementDirection)
        {
            ChoisirNouvelleDirection();
            timerChangementDirection = 0f;
        }

        // On tourne progressivement vers la direction cible, au lieu de basculer instantanément
        direction = Vector2.Lerp(direction, directionCible, vitesseDeRotation * Time.deltaTime).normalized;

        rb.linearVelocity = direction * vitesseDeplacement;
        
        if (direction.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }

    void ChoisirNouvelleDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector2 directionAleatoire = new Vector2(x, y).normalized;

        if (player != null)
        {
            Vector2 directionVersPlayer = ((Vector2)player.position - (Vector2)transform.position).normalized;
            directionCible = Vector2.Lerp(directionAleatoire, directionVersPlayer, agressivite).normalized;
        }
        else
        {
            directionCible = directionAleatoire;
        }

        if (directionCible == Vector2.zero)
        {
            directionCible = Vector2.right;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 normaleCollision = collision.GetContact(0).normal;

            // Au lieu d'un reflet parfait, on choisit une nouvelle direction cible
            // qui s'éloigne de la surface, mélangée à un peu d'aléatoire pour rester naturel
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            Vector2 nouvelleDirectionAleatoire = new Vector2(x, y).normalized;

            directionCible = (normaleCollision + nouvelleDirectionAleatoire * 0.5f).normalized;
        }
    }
}
