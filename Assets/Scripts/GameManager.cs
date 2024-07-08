using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 playerRespawnPosition;
    private Quaternion playerRespawnRotation;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerRespawnPosition = PlayerController.instance.transform.position;
        playerRespawnRotation = PlayerController.instance.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
        CameraController.instance.cinemachineBrain.enabled = false;

        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        PlayerController.instance.transform.position = playerRespawnPosition;
        PlayerController.instance.transform.rotation = playerRespawnRotation;
        PlayerController.instance.playerModel.transform.rotation = playerRespawnRotation;
        CameraController.instance.cinemachineBrain.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);

        UIManager.instance.fadeFromBlack = true;

    }

    public void SetSpawnPoint(Vector3 newSpawnPosition)
    {
        playerRespawnPosition = newSpawnPosition;
        Debug.Log("Set spawn point to: " + transform.position);
    }
}
