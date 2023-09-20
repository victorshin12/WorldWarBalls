using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private GameObject player;
    public PlayerController playerControl;

    public ParticleSystem explosionParticle;
    public ParticleSystem rocketParticle;
    public ParticleSystem detonatorParticle;

    public int a = 5;

    public static ParticleManager instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerController>();

    }
    /*
    public void DoDetonatorParticle(Vector3 detonatorPos)
    {
        detonatorParticle.transform.position = detonatorPos;
        detonatorParticle.Play();
    }
    */
    public void DoExplosionParticle(Vector3 enemyPos)
    {
        
            explosionParticle.transform.position = enemyPos;

            explosionParticle.Play();
            StartCoroutine(WaitExplosionParticle());
        
    }

    IEnumerator WaitExplosionParticle()
    {
        yield return new WaitForSeconds(1);
        explosionParticle.Stop();
    }

    ////////////////////////////////////////////////////////////////////////////


    public void DoRocketParticle(Vector3 rocketPos)
    {
        if (PlayerPrefs.GetInt("Particles") == 0)
        {
            playerControl.rocketExplosionParticle.transform.position = rocketPos;
            ObjectPooler.Instance.SpawnFromPool("explosionParticle", rocketPos, Quaternion.identity);

            ParticleSystem tempParticle = ObjectPooler.Instance.particleToSpawn;
            tempParticle.Play();
            StartCoroutine(WaitRocketParticle(tempParticle));
        }

    }

    IEnumerator WaitRocketParticle(ParticleSystem _tempParticle)
    {
        yield return new WaitForSeconds(3);
        _tempParticle.Stop();
       
    }


}
