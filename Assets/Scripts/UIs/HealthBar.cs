using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public RawImage health1;
    public RawImage health2;
    public RawImage health3;
    public RawImage health4;
    public RawImage health5;

    // Start is called before the first frame update
    void Start()
    {
        health1.enabled = true;
        health2.enabled = true;
        health3.enabled = true;
        health4.enabled = true;
        health5.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        int a = PlayerPrefs.GetInt("HealthLeft");



        if(a == 5)
        {
            health1.enabled = true;
            health2.enabled = true;
            health3.enabled = true;
            health4.enabled = true;
            health5.enabled = true;
        }

        if (a == 4)
        {
            health1.enabled = true;
            health2.enabled = true;
            health3.enabled = true;
            health4.enabled = true;
            health5.enabled = false;
        }

        if (a == 3)
        {
            health1.enabled = true;
            health2.enabled = true;
            health3.enabled = true;
            health4.enabled = false;
            health5.enabled = false;
        }

        if (a == 2)
        {
            health1.enabled = true;
            health2.enabled = true;
            health3.enabled = false;
            health4.enabled = false;
            health5.enabled = false;
        }

        if (a == 1)
        {
            health1.enabled = true;
            health2.enabled = false;
            health3.enabled = false;
            health4.enabled = false;
            health5.enabled = false;
        }


    }

    
}
