using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public int health;

    public TextMeshProUGUI healthText;


    void Start()
    {
        health = 100;
        SetHealthText();
    }

    void Update()
    {
        
    }

    public void HealthSub()
    {
        health = health - 1;
        SetHealthText();
    }


    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();

        if (health == 0)
        {
            SceneManager.LoadScene(1);
            health = 1000;
            SetHealthText();
        }
    }
}
