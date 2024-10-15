using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance; // Singleton

    private Camera mainCamera;

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

        mainCamera = Camera.main;
    }

    public void AdjustCamera(int gridSize)
    {
        // Ajusta o zoom e a posição da câmera com base no grid
        float cameraDistance = gridSize / 2f + 2;
        mainCamera.orthographicSize = cameraDistance;
        mainCamera.transform.position = new Vector3(gridSize / 2f, gridSize / 2f, -10f);
    }
}
