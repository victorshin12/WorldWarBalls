using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Speed of enemy
    public float speed;

    private Rigidbody enemyRb;

    private GameObject player;
    public PlayerController playerControl;



    // Start is called before the first frame update
    void Start()
    {

        //Getting some random stuff that is probably useful
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        playerControl = player.GetComponent<PlayerController>();

        FollowPlayer();
        

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        speed = Random.Range(3, 6);
        //Follows player by getting player's and enemy's vector3 and getting the direction
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
    }


}
