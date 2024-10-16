using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; 

    public GameObject gameOverPanel; 
    public GameObject startPanel; 
    public Button startButton; 
    public Button restartButton; 
    public Button backButton; 

    
    public TextMeshProUGUI gridSizeLabel;
    public TextMeshProUGUI speedLabel;

    public Slider gridSizeSlider;
    public Slider speedSlider;

    #region Singleton
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
    #endregion
    void Start()
    {
        // Configura os bot�es
        startButton.onClick.AddListener(OnStartGame);
        restartButton.onClick.AddListener(OnRestartGame);
        backButton.onClick.AddListener(OnBackToSettings); // Configura o bot�o de Voltar

        gameOverPanel.SetActive(false); // Esconde o painel de Game Over inicialmente
        startPanel.SetActive(true); // Mostra a tela de in�cio

        // Pausa o tempo no in�cio (menu vis�vel)
        Time.timeScale = 0;

        // Configura os sliders para atualizar os textos
        gridSizeSlider.onValueChanged.AddListener(UpdateGridSizeText);
        speedSlider.onValueChanged.AddListener(UpdateSpeedText);

        // Inicializa os textos
        UpdateGridSizeText(gridSizeSlider.value);
        UpdateSpeedText(speedSlider.value);
    }

    // Atualiza o texto do tamanho do grid
    void UpdateGridSizeText(float value)
    {
        gridSizeLabel.text = "Grid Size: " + value.ToString("F0"); // Formato sem casas decimais
    }

    // Atualiza o texto da velocidade da cobra
    void UpdateSpeedText(float value)
    {
        speedLabel.text = "Speed: " + value.ToString("F1"); // Formato com uma casa decimal
    }

    // M�todo para iniciar o jogo
    public void OnStartGame()
    {
        int gridSize = (int)gridSizeSlider.value;
        float speed = speedSlider.value;

        GameManager.Instance.StartGame(gridSize, speed); // Inicia o jogo com os par�metros do slider
        startPanel.SetActive(false); // Esconde o painel de in�cio ao come�ar o jogo

        // Retoma o tempo do jogo (Time.timeScale = 1)
        Time.timeScale = 1;
    }

    // M�todo para reiniciar o jogo
    public void OnRestartGame()
    {
        GameManager.Instance.RestartGame();
        // Certifique-se de que o tempo seja retomado ao reiniciar
        Time.timeScale = 1;
    }

    // M�todo para voltar para as configura��es (Tela Inicial)
    public void OnBackToSettings()
    {
        GameManager.Instance.RestartGame(); // Reinicia o jogo

        startPanel.SetActive(true); // Mostra o painel de in�cio
        gameOverPanel.SetActive(false); // Esconde o painel de Game Over

        startButton.gameObject.SetActive(true); // Mostra o bot�o de Start novamente

        // Pausa o tempo enquanto o jogador est� nas configura��es
        Time.timeScale = 0;
    }

    // Mostra o painel de Game Over
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);

        // Pausa o tempo no Game Over
        Time.timeScale = 0;
    }

    // Esconde o painel de Game Over
    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
}
