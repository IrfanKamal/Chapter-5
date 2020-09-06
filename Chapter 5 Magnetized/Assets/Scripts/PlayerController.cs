using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    private Rigidbody2D rb2D;
    public float moveSpeed = 5f;
    public float pullForce = 100f;
    public float rotateSpeed = 360f;
    private GameObject hookedTower;
    private bool isPulled = false;
    private UIController uiControl;
    private AudioSource myAudio;
    private bool isCrashed = false;
    private Vector3 startPosition;
    [HideInInspector]
    public bool isClicked = false;
    [HideInInspector]
    public GameObject closestTower;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        uiControl = GameObject.Find("Canvas").GetComponent<UIController>();
        myAudio = GetComponent<AudioSource>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCrashed)
        {
            if (!myAudio.isPlaying)
            {
                //Restart Scene
                RestartPosition();
            }
        }
        else
        {
            //Move the object
            rb2D.velocity = -transform.up * moveSpeed;

            if (isClicked && !isPulled)
            {
                //Debug.Log("Enter");
                if (closestTower != null && hookedTower == null)
                {
                    hookedTower = closestTower;
                }
                if (hookedTower)
                {
                    float distance = Vector2.Distance(transform.position, hookedTower.transform.position);

                    //Gravitation toward tower
                    Vector3 pullDirection = (hookedTower.transform.position - transform.position).normalized;
                    float newPullForce = Mathf.Clamp(pullForce / distance, 20, 50);
                    rb2D.AddForce(pullDirection * newPullForce);

                    //Angular velocity
                    rb2D.angularVelocity = -rotateSpeed / distance;
                    isPulled = true;
                }
            }

            if (!isClicked)
            {
                isPulled = false;
                hookedTower = null;
                rb2D.angularVelocity = 0;
            }
        }
    }

    // Method mendeteksi collision
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Jika bendanya adalah Wall
        if(collision.gameObject.tag == "Wall")
        {
            if (!isCrashed)
            {
                //Play SFX
                myAudio.Play();
                rb2D.velocity = Vector3.zero;
                rb2D.angularVelocity = 0f;
                isCrashed = true;
            }
        }
    }

    // Method memasuki event trigger
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Debug.Log("LevelClear!");
            uiControl.EndGame();
        }
    }


    public void RestartPosition()
    {
        //Set to start position
        this.transform.position = startPosition;

        //Restart rotation
        this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

        //Set isCrashed to false
        isCrashed = false;

        if (closestTower)
        {
            closestTower.GetComponent<SpriteRenderer>().color = Color.white;
            closestTower = null;
        }

        isPulled = false;
        hookedTower = null;
        rb2D.angularVelocity = 0;
    }
}
