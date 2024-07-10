using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    public HeartContainer next;

    [Range(0, 1)] float fill;
    [SerializeField] Image fillImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeart(float count)
    {
        fill = count;
        fillImage.fillAmount = fill;
        count --;
        if (next != null)
        {
            next.SetHeart(count);
        }
    }
}
