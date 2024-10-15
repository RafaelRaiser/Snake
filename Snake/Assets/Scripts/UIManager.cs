using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider gridSizeSlider;
    public Slider snakeSpeedSlider;
    public Button startButton;

    public static UIManager Instance;

    public int gridSize { get; private set; }
    public float snakeSpeed { get; private set; }

    #region Singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    void Start()
    {
        // Configurações iniciais
        gridSize = (int)gridSizeSlider.value;
        snakeSpeed = snakeSpeedSlider.value;

        // Vincula o botão Start ao método que iniciará o jogo
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        // Atualiza as configurações e carrega a cena do jogo
        gridSize = (int)gridSizeSlider.value;
        snakeSpeed = snakeSpeedSlider.value;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}