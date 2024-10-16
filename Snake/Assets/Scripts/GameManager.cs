using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    public int gridSizeX;
    public int gridSizeY;
    public float snakeSpeed;
    public GameObject foodPrefab; 
    public Vector2 currentFoodPosition;
    private GameObject currentFoodInstance; 

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
    // Inicia o jogo
    public void StartGame(int gridSize, float speed)
    {
        gridSizeX = gridSize;
        gridSizeY = gridSize;

        snakeSpeed = speed;

        // Inicializa a cobra
        Player.Instance.InitializeSnake();

        // Ajusta a c�mera para que o grid fique centralizado e vis�vel
        CameraManager.Instance.AdjustCamera(gridSize);

        // Instancia a comida no in�cio do jogo
        SpawnFood();
    }

    // M�todo para instanciar comida aleatoriamente
    public void SpawnFood()
    {
        // Se j� existir uma comida na cena, destr�i a comida anterior
        if (currentFoodInstance != null)
        {
            Destroy(currentFoodInstance);
        }

        // Gera uma posi��o aleat�ria dentro dos limites do grid
        int xPos = Random.Range(0, gridSizeX);
        int yPos = Random.Range(0, gridSizeY);
        currentFoodPosition = new Vector2(xPos, yPos);

        // Instancia a nova comida na posi��o aleat�ria
        currentFoodInstance = Instantiate(foodPrefab, currentFoodPosition, Quaternion.identity);
    }

    // M�todo para exibir o Game Over
    public void GameOver()
    {
        UIManager.Instance.ShowGameOverPanel(); // Mostra a tela de Game Over
    }

    // Reinicia o jogo
    public void RestartGame()
    {
        Player.Instance.ResetSnake(); // Reseta a cobra
        SpawnFood(); // Instancia nova comida
        UIManager.Instance.HideGameOverPanel(); // Esconde o painel de Game Over
    }
}
