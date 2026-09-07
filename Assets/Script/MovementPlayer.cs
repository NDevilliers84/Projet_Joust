using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    
    [Header("Réglages du mouvement")]
    public float vitesseDeplacement = 5f;      // vitesse horizontale
    public float forceDeBattement = 6f;        // force ajoutée à chaque battement
    public float vitesseChuteMax = 8f;

    [Header("Références")] 
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        GererDeplacementHorizontal();
        GererVol();
        LimiterVitesseDeChute();
    }

    void GererDeplacementHorizontal()
    {
        float directionHorizontale = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(directionHorizontale * vitesseDeplacement, rb.linearVelocity.y);
        
        // On retourne le sprite selon la direction
        if (directionHorizontale > 0.01f)
        {
            spriteRenderer.flipX = true;   // regarde à droite (sens normal du dessin)
        }
        else if (directionHorizontale < -0.01f)
        {
            spriteRenderer.flipX = false;    // regarde à gauche (miroir)
        }
    }

    void GererVol()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // AddForce avec Impulse : on AJOUTE une poussée vers le haut,
            // au lieu d'imposer une vitesse fixe. Ça donne un vol plus naturel,
            // où chaque battement s'additionne à la vitesse actuelle.
            rb.AddForce(Vector2.up * forceDeBattement, ForceMode2D.Impulse);
        }
    }

    void LimiterVitesseDeChute()
    {
        // Si le joueur tombe plus vite que la limite autorisée, on plafonne
        if (rb.linearVelocity.y < -vitesseChuteMax)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -vitesseChuteMax);
        }
    }
}
