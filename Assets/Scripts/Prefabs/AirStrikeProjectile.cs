using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AirStrikeProjectile : MonoBehaviour
{
    //Initial values for bullet
    private float m_Speed = 1500;   // this is the projectile's speed
    public float m_Lifespan = 0.3f; // this is the projectile's lifespan (in seconds). It takes 1.6 sec to get to y = 0

    private Rigidbody m_Rigidbody;

    private GameObject player;
    public ParticleManager particleScript;

    private GameObject enemy;


    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
        particleScript = player.GetComponent<ParticleManager>();

        //Shoots bullet real fast forward and then removes it after m_Lifespan seconds
        m_Rigidbody.AddForce(m_Rigidbody.transform.up * m_Speed);

    }

    private void Update()
    {
        if(gameObject.transform.position.y < 0.5)
        {
            Vector3 rocketPos = new Vector3(transform.position.x, 0, transform.position.z);

            particleScript.DoRocketParticle(rocketPos);
            Destroy(gameObject); 

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If bullet hits enemy, then remove both bullet and enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerController.instance.playerAudio.PlayOneShot(PlayerController.instance.explosionSound, 1.0f);
            enemy = other.gameObject;
            Vector3 enemyPos = enemy.transform.position;

            particleScript.DoExplosionParticle(enemyPos);
            Destroy(other.gameObject);
            Destroy(gameObject);

        }

    }

}