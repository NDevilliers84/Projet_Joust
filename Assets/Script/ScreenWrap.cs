using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private float limiteGauche;
    private float limiteDroite;

    void Start()
    {
        CalculerLimitesEcran();
    }

    void CalculerLimitesEcran()
    {
        Camera camera = Camera.main;

        // ViewportToWorldPoint(0,0) = coin bas-gauche de l'écran en coordonnées du monde
        // ViewportToWorldPoint(1,0) = coin bas-droite de l'écran en coordonnées du monde
        Vector3 coinGauche = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector3 coinDroit = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));

        limiteGauche = coinGauche.x;
        limiteDroite = coinDroit.x;
    }

    void Update()
    {
        Vector3 position = transform.position;

        if (position.x < limiteGauche)
        {
            position.x = limiteDroite;
            transform.position = position;
        }
        else if (position.x > limiteDroite)
        {
            position.x = limiteGauche;
            transform.position = position;
        }
    }
}
