using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("Réglages du mouvement")]
    public float vitesseDeplacement = 5f;      // vitesse horizontale
    public float forceDeBattement = 6f;        // force ajoutée à chaque battement
    public float vitesseChuteMax = 8f;         // vitesse de chute maximale (évite de tomber trop vite)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
