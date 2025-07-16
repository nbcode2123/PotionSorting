using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    public GameObject CineMachineCamera;
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void SetPotionShelfToCamera(GameObject middlePivot, float size)
    {
        CineMachineCamera.GetComponent<CinemachineVirtualCamera>().Follow = middlePivot.transform;
        CineMachineCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = size;
    }

}
