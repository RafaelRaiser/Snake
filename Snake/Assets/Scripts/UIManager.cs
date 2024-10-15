using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; 

    public Slider gridSizeSlider;
    public Slider speedSlider;

    public TextMeshProUGUI gridSizeLabel;  // Texto do TextMesh Pro para exibir o tamanho do grid
    public TextMeshProUGUI speedLabel;     // Texto do TextMesh Pro para exibir a velocidade da cobra

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;  // Texto do TextMesh Pro para exibir "Game Over"

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
        // Listeners para atualizar os textos conforme os sliders mudam
        gridSizeSlider.onValueChanged.AddListener(UpdateGridSizeText);
        speedSlider.onValueChanged.AddListener(UpdateSpeedText);

        // Configurar listeners dos botões
        startButton.onClick.AddListener(OnStartGame);
        restartButton.onClick.AddListener(OnRestartGame);
        backButton.onClick.AddListener(OnBackToSettings);
        gameOverPanel.SetActive(false);

        // Atualizar os textos iniciais com base nos valores iniciais dos sliders
        UpdateGridSizeText(gridSizeSlider.value);
        UpdateSpeedText(speedSlider.value);
    }

    // Atualiza o texto ao lado do slider de tamanho do grid
    void UpdateGridSizeText(float value)
    {
        gridSizeLabel.text = "Grid Size: " + value.ToString("F0"); // Formato sem casas decimais
    }

    // Atualiza o texto ao lado do slider de velocidade
    void UpdateSpeedText(float value)
    {
        speedLabel.text = "Snake Speed: " + value.ToString("F1"); // Formato com uma casa decimal
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
        gameOverText.text = "Game Over"; // Atualiza o texto de Game Over
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