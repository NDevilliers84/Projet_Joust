using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Ennemi bonus")]
    public GameObject prefabEnemyBonus;
    [Range(0f, 1f)]
    public float chanceEnnemiBonus = 0.2f;
    
    [Header("Réglages des vagues")]
    public GameObject prefabEnemy;
    public Transform[] pointsDeSpawn;      
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
        
        ennemisVivants.RemoveAll(ennemi => ennemi == null);

        
        if (ennemisVivants.Count == 0)
        {
            Invoke(nameof(DemarrerNouvelleVague), delaiEntreVagues);
            enabled = false; 
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

        enabled = true; 
    }

    void SpawnUnEnnemi()
    {
        Transform pointDeSpawn = pointsDeSpawn[Random.Range(0, pointsDeSpawn.Length)];

        
        GameObject prefabAUtiliser = prefabEnemy;
        if (prefabEnemyBonus != null && Random.value < chanceEnnemiBonus)
        {
            prefabAUtiliser = prefabEnemyBonus;
        }

        GameObject nouvelEnnemi = Instantiate(prefabAUtiliser, pointDeSpawn.position, Quaternion.identity);
        ennemisVivants.Add(nouvelEnnemi);
    }
}
