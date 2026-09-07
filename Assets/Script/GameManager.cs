using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Références UI - Game Over")]
    public GameObject panelGameOver;
    public TextMeshProUGUI texteScoreFinal;

    [Header("Références UI - Main Menu")]
    public GameObject panelMainMenu;

    private bool enGameOver = false;
    private bool enMainMenu = true;

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
        // Le jeu démarre en pause, sur le menu principal
        Time.timeScale = 0f;
        panelMainMenu.SetActive(true);
    }

    void Update()
    {
        if (enMainMenu && Input.GetKeyDown(KeyCode.Return))
        {
            TenterDeCommencer();
        }
        else if (enGameOver && Input.GetKeyDown(KeyCode.Return))
        {
            TenterDeRejouer();
        }
    }

    void TenterDeCommencer()
    {
        if (CoinManager.instance != null && CoinManager.instance.DepenserUnCredit())
        {
            enMainMenu = false;
            panelMainMenu.SetActive(false);
            Time.timeScale = 1f; // le jeu démarre vraiment
        }
        else
        {
            Debug.Log("Pas assez de crédits ! Insère une pièce (Tab).");
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
