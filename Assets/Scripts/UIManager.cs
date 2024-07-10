using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    public Image blackScreen;

    public float fadeSpeed = 0f;
    public bool fadeToBlack, fadeFromBlack;

    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject heartContainerPrefab;
    [SerializeField] List<GameObject> heartContainers;
    HeartContainer currentContainer;

    int totalHearts;
    float currentHearts;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        heartContainers = new List<GameObject>();
        totalHearts = HealthManager.instance.maxHealth;
        SetUpHearts();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    public void SetUpHearts()
    {
        heartContainers.Clear();
        for (int i = healthBar.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(healthBar.transform.GetChild(i).gameObject);
        }

        currentHearts = (float)totalHearts;

        for (int i = 0; i < totalHearts; i++)
        {
            GameObject newHeart = Instantiate(heartContainerPrefab, healthBar.transform);
            heartContainers.Add(newHeart);
            if(currentContainer != null)
            {
                currentContainer.next = newHeart.GetComponent<HeartContainer>();
            }
            currentContainer = newHeart.GetComponent<HeartContainer>();
        }
        currentContainer = heartContainers[0].GetComponent<HeartContainer>();
    }

    public void SetCurrentHealth(float health)
    {
        currentHearts = health;
        currentContainer.SetHeart(health);
    }

    public void AddHearts(float healAmount)
    {
        currentHearts += healAmount;
        if(currentHearts > totalHearts)
        {
            currentHearts = (float)totalHearts;
        }
        currentContainer.SetHeart(currentHearts);
    }

    public void RemoveHearts(float damageAmount)
    {
        currentHearts -= damageAmount;
        if(currentHearts <= 0)
        {
            currentHearts = 0f;
        }
        currentContainer.SetHeart(currentHearts);
    }
}
