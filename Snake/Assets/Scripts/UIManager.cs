using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton

    public Slider gridSizeSlider;
    public Slider speedSlider;
    public GameObject gameOverPanel;

    public Button startButton;
    public Button restartButton;
    public Button backButton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Configurar listeners dos botões
        startButton.onClick.AddListener(OnStartGame);
        restartButton.onClick.AddListener(OnRestartGame);
        backButton.onClick.AddListener(OnBackToSettings);
        gameOverPanel.SetActive(false);
    }

    public void OnStartGame()
    {
        int gridSize = (int)gridSizeSlider.value;
        float speed = speedSlider.value;
        GameManager.Instance.StartGame(gridSize, speed);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void OnRestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnBackToSettings()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}


