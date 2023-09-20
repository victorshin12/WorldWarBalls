using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public ParticleSystem particleToSpawn;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public ParticleSystem rocketExplosionParticle;
        public int size;
    }

    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<ParticleSystem>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<ParticleSystem>>();

        foreach(Pool pool in pools)
        {
            Queue<ParticleSystem> particlePool = new Queue<ParticleSystem>();

            for (int i = 0; i < pool.size; i++)
            {
                ParticleSystem ps = Instantiate(pool.rocketExplosionParticle);
                ps.gameObject.SetActive(false);
                particlePool.Enqueue(ps); 
            }

            poolDictionary.Add(pool.tag, particlePool);
        }

    }

    public ParticleSystem SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't excist.");
            return null;
        }

        particleToSpawn = poolDictionary[tag].Dequeue();

        particleToSpawn.gameObject.SetActive(true);
        particleToSpawn.transform.position = position;
        particleToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(particleToSpawn);

        return particleToSpawn;
    }

}
