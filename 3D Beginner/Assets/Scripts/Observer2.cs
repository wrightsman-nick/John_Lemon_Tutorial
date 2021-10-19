using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Observer2 : MonoBehaviour
{
    private int health;

    public Transform player;
    public TextMeshProUGUI healthText;

    bool m_IsPlayerInRange;

    void Start ()
    {
        health = 1000;
        SetHealthText();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update ()
    {
        if(m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray (transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    health = health - 1;
                    SetHealthText();
                }
            }
        }
    }

      void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
        if (health == 0)
        {
            SceneManager.LoadScene (1);
            health = 1000;
            SetHealthText();
        }
    }
}
