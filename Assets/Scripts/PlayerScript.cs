using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public float horizontalValue;

    public Text score;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public Text lives;
    public Transform Destination;
    public Transform player;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    Animator anim;

    private int scoreValue = 0;
    private int livesValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        winTextObject.SetActive(false);    
        loseTextObject.SetActive(false);
        SetLivesText();
        SetCountText();
    }
    void SetCountText()
    {
        score.text = "Score: " + scoreValue.ToString();
    }
    void SetLivesText()
    {
        lives.text = "Lives: " + livesValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State",1);
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
    else if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State",0);
        }
    if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State",1);
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
    else if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State",0);
        }
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

    if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue = scoreValue += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }

       if (collision.collider.tag == "Enemy")
        {
            livesValue = livesValue -= 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }

       if (livesValue <= 0)
        {
            loseTextObject.SetActive(true);
            gameObject.SetActive(false);
        }
        if (scoreValue  == 8)
        {
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
            winTextObject.SetActive(true);
        }

        if(collision.collider.tag == "Teleporter")
        {
            scoreValue = scoreValue += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
            player.transform.position = Destination.transform.position;
            livesValue = 3;
            SetLivesText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}