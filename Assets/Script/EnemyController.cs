using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Réglages du mouvement")]
    public float vitesseDeplacement = 3f;
    public float intervalleChangementDirection = 2f;

    [Header("Poursuite du player")]
    [Range(0f, 1f)]
    public float agressivite = 0.6f;   // 0 = totalement aléatoire, 1 = fonce droit sur le player

    private Rigidbody2D rb;
    private Vector2 direction;
    private float timerChangementDirection;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // On récupère automatiquement le Player grâce à son tag
        GameObject objetPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objetPlayer != null)
        {
            player = objetPlayer.transform;
        }

        ChoisirNouvelleDirection();
    }

    void Update()
    {
        timerChangementDirection += Time.deltaTime;

        if (timerChangementDirection >= intervalleChangementDirection)
        {
            ChoisirNouvelleDirection();
            timerChangementDirection = 0f;
        }

        rb.linearVelocity = direction * vitesseDeplacement;
    }

    void ChoisirNouvelleDirection()
    {
        // Direction purement aléatoire (comme avant)
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector2 directionAleatoire = new Vector2(x, y).normalized;

        if (player != null)
        {
            // Direction vers le player
            Vector2 directionVersPlayer = ((Vector2)player.position - (Vector2)transform.position).normalized;

            // On mélange les deux directions selon le curseur "agressivite"
            // Lerp = interpolation : 0 donne 100% aléatoire, 1 donne 100% vers le player
            direction = Vector2.Lerp(directionAleatoire, directionVersPlayer, agressivite).normalized;
        }
        else
        {
            direction = directionAleatoire;
        }

        if (direction == Vector2.zero)
        {
            direction = Vector2.right;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 normaleCollision = collision.GetContact(0).normal;
            direction = Vector2.Reflect(direction, normaleCollision).normalized;
        }
    }
}
