using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTouchesTerrain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            PlayerController.instance.playerAudio.PlayOneShot(PlayerController.instance.terrainSound, 0.7f);
            Debug.Log("Bullet hit terrain");
            Destroy(gameObject);
        }
    }
    
}
