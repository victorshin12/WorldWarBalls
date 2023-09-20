using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    //Initial values for bullet
    private float m_Speed = 1800f;   // this is the projectile's speed
    public float m_Lifespan = 3f; // this is the projectile's lifespan (in seconds)

    private Rigidbody m_Rigidbody;

    private GameObject player;
    public PlayerController playerControl;
    public ParticleManager particleScript;

    private GameObject enemy;


    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //Shoots bullet real fast forward and then removes it after m_Lifespan seconds
        m_Rigidbody.AddForce(m_Rigidbody.transform.forward * m_Speed);
        Destroy(gameObject, m_Lifespan);
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerController>();
        particleScript = player.GetComponent<ParticleManager>();


        var main = playerControl.rocketExplosionParticle.main;
        main.loop = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //If bullet hits enemy, then remove both bullet and enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerControl.playerAudio.PlayOneShot(playerControl.explosionSound, 0.7f);
            enemy = other.gameObject;
            Vector3 enemyPos = enemy.transform.position;

            particleScript.DoExplosionParticle(enemyPos);
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
    }
}