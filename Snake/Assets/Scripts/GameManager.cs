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
        // Singleton para garantir uma �nica inst�ncia
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
        // Inicia o jogo com os par�metros da UI
        gridSizeX = gridSize;
        gridSizeY = gridSize;
        snakeSpeed = speed;
        // Configura o ambiente inicial do jogo
        Player.Instance.InitializeSnake(); // Inicializa a cobra
        CameraManager.Instance.AdjustCamera(gridSize); // Ajusta a c�mera
    }

    public void GameOver()
    {
        // L�gica de Game Over
        UIManager.Instance.ShowGameOverPanel();
    }

    public void RestartGame()
    {
        // Reiniciar o jogo com as mesmas configura��es
        Player.Instance.ResetSnake();
        UIManager.Instance.HideGameOverPanel();
    }
}
