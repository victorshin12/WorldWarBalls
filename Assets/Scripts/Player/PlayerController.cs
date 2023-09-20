using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Initial values
    private float speed = 20.0f;
    public float powerupDuration = 1.3f;

    public GameObject powerupIndicator;

    private ProjectileTrigger projTrigger;
    public SpawnManager spawnMan;

    public bool hasPowerup = false;
    public bool GameOver = false;

    private int livesLeft = 5;
    public TextMeshProUGUI livesLeftText;
    public TextMeshProUGUI heartText;

    public int highScore = 0;
    public int myScore = 0;

    public static PlayerController instance;

    public bool followMe = true;

    Rigidbody rb;

    //Particles
    public ParticleSystem rocketExplosionParticle;

    //Audios
    public AudioSource playerAudio;
    public AudioClip shootSound;
    public AudioClip damagedSound;
    public AudioClip explosionSound;
    public AudioClip rocketSound;
    public AudioClip terrainSound;
    public AudioClip heartSound;
    public AudioClip powerupSound;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    public void Start()
    {
        PlayerPrefs.SetInt("HealthLeft", 5);
        transform.position = transform.position - new Vector3(0, -0.4f, 0);
       
        //Gets projectile trigger script
        projTrigger = GetComponent<ProjectileTrigger>();

        //Initial health
    //    livesLeftText.text = "Live Left: 5";

        heartText.enabled = false;

        GameOver = false;

        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    public void FixedUpdate()
    {       
        //Setting position for the powerup indicator
        if (!GameOver)
        {
            MovePlayer();
            powerupIndicator.transform.position = transform.position + new Vector3(0, 0.2f, 0);
        }


    }

    //Moves player with WASD/Arrow keys
    void MovePlayer()
    {
        //Get axis for player controls
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");

        //Initial value for mouse sensitivity
        float mouseSensitivity = 4f;

        //Mouse sensitivity will affect the speed of which the playear rotates
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity);

        //Player can move in all 4 directions
        transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup") && !hasPowerup)
        {
            playerAudio.PlayOneShot(powerupSound, 0.7f);
            //If you eat powerup, then destroy powerup game object and make hasPowerup true
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            projTrigger.powerupText.enabled = true;
            projTrigger.powerupText1.enabled = true;
            spawnMan.detonatorParticle.Stop();
        }
        if (other.gameObject.CompareTag("Heart"))
        {
            playerAudio.PlayOneShot(heartSound, 0.7f);
            //If player eats heart object, increase life by 1 and refresh lives left text
            Destroy(other.gameObject);
            livesLeft++;
            PlayerPrefs.SetInt("HealthLeft", livesLeft);
            //   livesLeftText.text = "Live Left: " + livesLeft;
            spawnMan.heartParticle.Stop();
            StartCoroutine(HeartTextCooldown());
        }
    }

    IEnumerator HeartTextCooldown()
    {
        heartText.enabled = true;
        yield return new WaitForSeconds(1.5f);
        heartText.enabled = false;
    }

    

    public void OnCollisionEnter(Collision collision)
    {
        //If player hits enemy, decrease health by 1. When it gets to zero, game over?
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAudio.PlayOneShot(damagedSound, 1.0f);
            livesLeft--;
            PlayerPrefs.SetInt("HealthLeft", livesLeft);
          //  livesLeftText.text = "Live Left: " + livesLeft;
            
            if(livesLeft == 0)
            {
                GameOver = true;
                myScore = spawnMan.waveNumber;
                PlayerPrefs.SetInt("GameScore", myScore);

                if (myScore > PlayerPrefs.GetInt("GameHighScore"))
                {
                    PlayerPrefs.SetInt("GameHighScore", myScore);
                }

                SceneManager.LoadScene(2);
            }

        }
    }

}
