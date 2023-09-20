using System.Collections;
using TMPro;
using UnityEngine;
public class ProjectileTrigger : MonoBehaviour
{
    public bool canShoot = true;

    public PlayerController playerControl;
    
    public GameObject m_Projectile;    // this is a reference to  projectile prefab
    public Transform m_SpawnTransform; // this is a reference to the transform where the prefab will spawn

    public GameObject rocketPrefab;

    public int shootCooldown = 2;

    //player gameobject
    public GameObject player;
    //camera gameobject
    public GameObject cameraGo;


    //Texts on canvas
    public TextMeshProUGUI shootReady;
    public TextMeshProUGUI reloading;
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI powerupText1;


    void Awake()
    {
        //When game starts, hide reloading text and powerup text
        reloading.enabled = false;
        powerupText.enabled = false;
        powerupText1.enabled = false;

    }




    void Update()
    {
        if (!playerControl.GameOver)
        {
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                //If you can shoot, and you click mouse, shoot a bullet towards where you are
                //facing, then do coroutine for cooldown/reloading
                playerControl.playerAudio.PlayOneShot(playerControl.shootSound, 0.5f);
                Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
                canShoot = false;
                shootReady.enabled = false;
                reloading.enabled = true;
                StartCoroutine(ShootCooldown());
            }





            //////////////////
            if (Input.GetMouseButton(1) && playerControl.hasPowerup)
            {
                //If you right click and you have powerup enabled, do these:
                playerControl.hasPowerup = false;
                playerControl.powerupIndicator.SetActive(false);
                powerupText.enabled = false;
                powerupText1.enabled = false;

                Debug.Log("You used powerup!");
                DoMassiveShit();

            }
        }

    }

    private void DoMassiveShit()
    {
        playerControl.playerAudio.PlayOneShot(playerControl.rocketSound, 0.3f);

        //Air Strike method
        for (int j = 0; j < 30; j++)
        {
            int randomX = Random.Range(-23, 23);
            int randomZ = Random.Range(-23, 23);
            int randomY = Random.Range(53, 62);
            Instantiate(rocketPrefab, new Vector3(randomX, randomY, randomZ), Quaternion.Euler(180, 0, 0));
        }
    }


    IEnumerator ShootCooldown()
    {
        //When a player shoots, cooldown will initiate, then when it ends, shoot ready
        //text will appear and reloading text will dissapear
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
        shootReady.enabled = true;
        reloading.enabled = false;
    }

}