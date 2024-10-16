using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance; 

    private Camera mainCamera;
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
        mainCamera = Camera.main; // Pega a referência da câmera principal
    }
    #endregion

    // Ajusta a câmera com base no tamanho do grid
    public void AdjustCamera(int gridSize)
    {
        float gridHalfSize = gridSize / 2f; // Calcula metade do grid

        // Ajuste o tamanho ortográfico da câmera para que todo o grid fique visível
        mainCamera.orthographicSize = gridHalfSize + 2; // Ajusta o zoom da câmera

        // Centraliza a câmera no meio do grid
        mainCamera.transform.position = new Vector3(gridHalfSize, gridHalfSize, -10f);
    }
}
