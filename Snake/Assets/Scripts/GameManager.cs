using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public int gridSizeX;
    public int gridSizeY;
    public GameObject gridCellPrefab;

    public static GameManager Instance;

    private void Awake()
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
        // Define o tamanho do grid baseado nas configurações do jogador
        gridSizeX = GameSettings.Instance.gridSize;
        gridSizeY = GameSettings.Instance.gridSize;

        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Gera o grid visual
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Instantiate(gridCellPrefab, new Vector2(x, y), Quaternion.identity);
            }
        }
    }
}