using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance; 

    public GameObject bodyPrefab;
    private List<Vector2> snakeBody; 
    private Vector2 direction; 
    private float moveTimer;
    private bool isGameOver = false; 

    #region Singleton
    void Awake()
    {
        // Singleton
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
    // Inicializa a cobra
    public void InitializeSnake()
    {
        snakeBody = new List<Vector2> { Vector2.zero }; // Inicia a cobra na posição (0,0)
        direction = Vector2.right; // Direção inicial para a direita
        moveTimer = 1f / GameManager.Instance.snakeSpeed; // Velocidade da cobra
        isGameOver = false;

        // Começa a mover a cobra em intervalos de tempo definidos
        InvokeRepeating("MoveSnake", 0f, moveTimer);
    }

    void Update()
    {
        // Captura o input do jogador
        HandleInput();
    }

    // Captura o input de W, A, S, D
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    void MoveSnake()
    {
        if (isGameOver) return;

        Vector2 nextPosition = snakeBody[0] + direction; // Calcula a nova posição da cabeça

        // Lógica de loop infinito para a câmera
        if (nextPosition.x >= GameManager.Instance.gridSizeX) nextPosition.x = 0;
        if (nextPosition.x < 0) nextPosition.x = GameManager.Instance.gridSizeX - 1;
        if (nextPosition.y >= GameManager.Instance.gridSizeY) nextPosition.y = 0;
        if (nextPosition.y < 0) nextPosition.y = GameManager.Instance.gridSizeY - 1;

        // Verifica se a cobra colidiu consigo mesma
        if (HasCollidedWithSelf(nextPosition))
        {
            isGameOver = true;
            GameManager.Instance.GameOver(); // Chama o Game Over
            return;
        }

        // Verifica se a cobra está comendo a comida
        if (nextPosition == GameManager.Instance.currentFoodPosition)
        {
            EatFood(); // Cobra cresce
        }
        else
        {
            // Movimenta a cobra removendo o último segmento
            snakeBody.RemoveAt(snakeBody.Count - 1);
        }

        // Insere a nova posição da cabeça
        snakeBody.Insert(0, nextPosition);
        UpdateSnakeBody();
    }

    // Verifica se a cobra colidiu consigo mesma
    bool HasCollidedWithSelf(Vector2 headPosition)
    {
        for (int i = 1; i < snakeBody.Count; i++)
        {
            if (snakeBody[i] == headPosition)
            {
                return true;
            }
        }
        return false;
    }

    // Atualiza visualmente o corpo da cobra
    void UpdateSnakeBody()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject); // Remove segmentos antigos
        }

        foreach (Vector2 segment in snakeBody)
        {
            Instantiate(bodyPrefab, segment, Quaternion.identity, transform); // Instancia novos segmentos
        }
    }

    // Método para fazer a cobra crescer ao comer a comida
    void EatFood()
    {
        GameManager.Instance.SpawnFood(); // Instancia nova comida
    }

    // Reseta a cobra ao reiniciar o jogo
    public void ResetSnake()
    {
        CancelInvoke("MoveSnake"); // Para o movimento
        InitializeSnake(); // Recomeça a cobra
    }
}