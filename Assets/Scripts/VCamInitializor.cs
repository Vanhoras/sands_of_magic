using Cinemachine;
using UnityEngine;

public class VCamInitializor : MonoBehaviour
{

    void Start()
    {
        Player player = FindObjectOfType<Player>();
        CinemachineVirtualCamera vCam = gameObject.GetComponent<CinemachineVirtualCamera>();

        vCam.Follow = player.gameObject.transform;
        vCam.LookAt = player.gameObject.transform;
    }

    
}
