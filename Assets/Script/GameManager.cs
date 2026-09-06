using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Références UI")]
    public GameObject panelGameOver;
    public TextMeshProUGUI texteScoreFinal;

    private bool enGameOver = false;

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

    void Update()
    {
        if (enGameOver && Input.GetKeyDown(KeyCode.Return))
        {
            TenterDeRejouer();
        }
    }

    public void DeclencherGameOver()
    {
        Debug.Log("Game Over déclenché !");

        enGameOver = true;
        panelGameOver.SetActive(true);

        if (ScoreManager.instance != null)
        {
            texteScoreFinal.text = "Score final : " + ScoreManager.instance.scoreActuel;
        }

        Time.timeScale = 0f;
    }

    void TenterDeRejouer()
    {
        if (CoinManager.instance != null && CoinManager.instance.DepenserUnCredit())
        {
            Rejouer();
        }
        else
        {
            Debug.Log("Pas assez de crédits ! Insère une pièce (Tab).");
        }
    }

    void Rejouer()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
