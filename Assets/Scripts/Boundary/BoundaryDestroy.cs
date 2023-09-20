using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDestroy : MonoBehaviour
{

    private float zBound = 24;
    private float xBound = 24;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerBoundary();
    }

    void PlayerBoundary()
    {
        if (transform.position.z > zBound)
        {

            Destroy(gameObject);
        }
        if (transform.position.x > xBound)
        {
            Destroy(gameObject);
        }

        if (transform.position.z < -zBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -xBound)
        {
            Destroy(gameObject);
        }
    }

}
