using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    
    [Header("Réglages du mouvement")]
    public float vitesseDeplacement = 5f;      
    public float forceDeBattement = 6f;        
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
        
        
        if (directionHorizontale > 0.01f)
        {
            spriteRenderer.flipX = true;   
        }
        else if (directionHorizontale < -0.01f)
        {
            spriteRenderer.flipX = false;    
        }
    }

    void GererVol()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * forceDeBattement, ForceMode2D.Impulse);
        }
    }

    void LimiterVitesseDeChute()
    {
        if (rb.linearVelocity.y < -vitesseChuteMax)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -vitesseChuteMax);
        }
    }
}
