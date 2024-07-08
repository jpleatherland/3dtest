using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public CinemachineBrain cinemachineBrain;

    void Start()
    {
        instance = this;
    }

    void Update()
    {

    }
}