using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Score")]
    public int scoreActuel = 0;

    [Header("Points")]
    public int pointsParEnnemi = 500;
    public int pointsParOeufRamasse = 250;

    [Header("Combo bonus")]
    public float delaiMaxEntreCombos = 3f;   // temps max entre deux kills pour garder le combo
    public int bonusParNiveauDeCombo = 100;  // points supplémentaires par niveau de combo

    private int comboActuel = 0;
    private float dernierKillTemps = -999f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AjouterPoints(int points)
    {
        scoreActuel += points;
        Debug.Log("Score : " + scoreActuel);
    }

    public void AjouterPointsEnnemi(int bonusSupplementaire = 0)
    {
        if (Time.time - dernierKillTemps <= delaiMaxEntreCombos)
        {
            comboActuel++;
        }
        else
        {
            comboActuel = 1;
        }

        dernierKillTemps = Time.time;

        int bonusCombo = (comboActuel - 1) * bonusParNiveauDeCombo;
        int totalPoints = pointsParEnnemi + bonusCombo + bonusSupplementaire;

        AjouterPoints(totalPoints);

        if (comboActuel > 1)
        {
            Debug.Log("Combo x" + comboActuel + " ! Bonus combo : +" + bonusCombo);
        }

        if (bonusSupplementaire > 0)
        {
            Debug.Log("Ennemi bonus ! +" + bonusSupplementaire + " points supplémentaires");
        }
    }
}
