using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    
    public TextMeshProUGUI countText;
    public GameObject collectedObject;
    public GameObject collectOne;
    public GameObject collectTwo;
    public GameObject collectThree;
    public GameObject gameEnding;

    private int count;
    private int collectsound;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    bool m_CollectAudioPlayed;


    void Start()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource>();
        count = 0;
        collectsound = 0;

        collectOne.SetActive(false);
        collectTwo.SetActive(false);
        collectThree.SetActive(false);
        collectedObject.SetActive(false);
        gameEnding.SetActive(false);
        SetCountText();
        
    }


    void FixedUpdate()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if(isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
    }


    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count == 3)
        {
            collectedObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectibles"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            collectsound = collectsound + 1;
            SetCountText();
        }

        if (collectsound == 1)
        {
            collectOne.SetActive(true);
        }

        if (collectsound == 2)
        {
            collectTwo.SetActive(true);
        }

        if (collectsound == 3)
        {
            collectThree.SetActive(true);
            gameEnding.SetActive(true);
        }
    }
}
