using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Réglages des vagues")]
    public GameObject prefabEnemy;
    public Transform[] pointsDeSpawn;      // endroits où les ennemis peuvent apparaître
    public int ennemisVagueDepart = 3;
    public float delaiEntreVagues = 3f;

    private int numeroVagueActuelle = 0;
    private List<GameObject> ennemisVivants = new List<GameObject>();

    void Start()
    {
        DemarrerNouvelleVague();
    }

    void Update()
    {
        // On nettoie la liste des ennemis détruits (mangés par le null après Destroy)
        ennemisVivants.RemoveAll(ennemi => ennemi == null);

        // Si tous les ennemis de la vague sont morts, on prépare la suivante
        if (ennemisVivants.Count == 0)
        {
            Invoke(nameof(DemarrerNouvelleVague), delaiEntreVagues);
            enabled = false; // on évite de rappeler Invoke plusieurs fois pendant l'attente
        }
    }

    void DemarrerNouvelleVague()
    {
        numeroVagueActuelle++;
        int nombreEnnemis = ennemisVagueDepart + (numeroVagueActuelle - 1); // +1 ennemi par vague

        Debug.Log("Vague " + numeroVagueActuelle + " : " + nombreEnnemis + " ennemis");

        for (int i = 0; i < nombreEnnemis; i++)
        {
            SpawnUnEnnemi();
        }

        enabled = true; // on réactive Update pour surveiller cette nouvelle vague
    }

    void SpawnUnEnnemi()
    {
        // On choisit un point de spawn au hasard parmi ceux disponibles
        Transform pointDeSpawn = pointsDeSpawn[Random.Range(0, pointsDeSpawn.Length)];

        GameObject nouvelEnnemi = Instantiate(prefabEnemy, pointDeSpawn.position, Quaternion.identity);
        ennemisVivants.Add(nouvelEnnemi);
    }
}
