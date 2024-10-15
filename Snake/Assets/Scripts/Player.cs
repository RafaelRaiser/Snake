using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance; // Singleton

    public GameObject bodyPrefab; // Prefab do corpo da cobra
    public Transform gridParent; // Pai do grid e corpo da cobra

    private List<Vector2> snakeBody;
    private Vector2 direction;
    private float moveTimer;
    private bool isGameOver;

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

    public void InitializeSnake()
    {
        // Inicializa a cobra no início do jogo
        snakeBody = new List<Vector2> { Vector2.zero };
        direction = Vector2.right;
        moveTimer = 1f / GameManager.Instance.snakeSpeed;
        isGameOver = false;
        InvokeRepeating("MoveSnake", 0f, moveTimer);
    }

    public void MoveSnake()
    {
        if (isGameOver) return;

        Vector2 nextPosition = snakeBody[0] + direction;

        // Teletransporte quando a cobra sai do grid (loop infinito)
        if (nextPosition.x >= GameManager.Instance.gridSizeX) nextPosition.x = 0;
        if (nextPosition.x < 0) nextPosition.x = GameManager.Instance.gridSizeX - 1;
        if (nextPosition.y >= GameManager.Instance.gridSizeY) nextPosition.y = 0;
        if (nextPosition.y < 0) nextPosition.y = GameManager.Instance.gridSizeY - 1;

        if (HasCollidedWithSelf(nextPosition))
        {
            isGameOver = true;
            GameManager.Instance.GameOver();
            return;
        }

        snakeBody.Insert(0, nextPosition);
        snakeBody.RemoveAt(snakeBody.Count - 1);
        UpdateSnakeBody();
    }

    public void SetDirection(Vector2 newDirection)
    {
        if (newDirection != -direction) direction = newDirection;
    }

    bool HasCollidedWithSelf(Vector2 headPosition)
    {
        for (int i = 1; i < snakeBody.Count; i++)
        {
            if (snakeBody[i] == headPosition) return true;
        }
        return false;
    }

    public void UpdateSnakeBody()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Vector2 segment in snakeBody)
        {
            Instantiate(bodyPrefab, segment, Quaternion.identity, gridParent);
        }
    }

    public void ResetSnake()
    {
        CancelInvoke("MoveSnake");
        InitializeSnake();
    }
}