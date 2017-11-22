using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    float flag = 1;
    float y;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        y = transform.position.y;
        if (y <= -2)
        {
            flag = 1;
        }
        if (y >= 2)
        {
            flag = -1;
        }
        transform.Translate(0, (Time.deltaTime) * 4 * flag, 0);
    }
}
