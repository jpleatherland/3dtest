using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Light checkLight; 
    private GameObject lightEffect;
    private bool isBrightening;
    // Start is called before the first frame update
    void Start()
    {
        checkLight = this.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Light>();
        lightEffect = this.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBrightening){
            checkLight.intensity = Mathf.MoveTowards(checkLight.intensity, 2.5f, 1.0f * Time.deltaTime);
            if (checkLight.intensity == 2.5f){
                isBrightening = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SetSpawnPoint(new Vector3(transform.position.x, 0f, transform.position.z-5f));

            Checkpoint[] allCheckPoints = FindObjectsOfType<Checkpoint>();

            for(int i = 0; i < allCheckPoints.Length; i++)
            {
                allCheckPoints[i].checkLight.enabled = false;
                allCheckPoints[i].lightEffect.SetActive(false);
            }

            isBrightening = true;
            lightEffect.SetActive(true);
        }
    }
}
