using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    public int gridSizeX;
    public int gridSizeY;
    public float snakeSpeed;
    #region Singleton
    void Awake()
    {
        // Singleton para garantir uma única instância
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

    public void StartGame(int gridSize, float speed)
    {
        // Inicia o jogo com os parâmetros da UI
        gridSizeX = gridSize;
        gridSizeY = gridSize;
        snakeSpeed = speed;
        // Configura o ambiente inicial do jogo
        Player.Instance.InitializeSnake(); // Inicializa a cobra
        CameraManager.Instance.AdjustCamera(gridSize); // Ajusta a câmera
    }

    public void GameOver()
    {
        // Lógica de Game Over
        UIManager.Instance.ShowGameOverPanel();
    }

    public void RestartGame()
    {
        // Reiniciar o jogo com as mesmas configurações
        Player.Instance.ResetSnake();
        UIManager.Instance.HideGameOverPanel();
    }
}
