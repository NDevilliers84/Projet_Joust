using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Références UI")]
    public TextMeshProUGUI texteScore;
    public TextMeshProUGUI texteVies;

    void Update()
    {
        if (ScoreManager.instance != null)
        {
            texteScore.text = "Score : " + ScoreManager.instance.scoreActuel;
        }
    }

    public void MettreAJourVies(int nombreDeVies)
    {
        texteVies.text = "Vies : " + nombreDeVies;
    }
}
