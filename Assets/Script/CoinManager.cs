using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("Crédits")]
    public int credits = 0;

    [Header("UI")]
    public TextMeshProUGUI texteCredits;

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

    void Start()
    {
        MettreAJourTexteCredits();
    }

    void Update()
    {
        // Tab pour insérer une pièce
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InsererPiece();
        }
    }

    void InsererPiece()
    {
        credits++;
        Debug.Log("Pièce insérée ! Crédits : " + credits);
        MettreAJourTexteCredits();
    }

    public bool DepenserUnCredit()
    {
        if (credits > 0)
        {
            credits--;
            MettreAJourTexteCredits();
            return true; // le crédit a bien été dépensé
        }

        return false; // pas assez de crédits
    }

    void MettreAJourTexteCredits()
    {
        if (texteCredits != null)
        {
            texteCredits.text = "Crédits : " + credits;
        }
    }
}
