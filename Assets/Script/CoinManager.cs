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
            return true; 
        }

        return false; 
    }

    void MettreAJourTexteCredits()
    {
        if (texteCredits != null)
        {
            texteCredits.text = "Crédits : " + credits;
        }
    }
}
