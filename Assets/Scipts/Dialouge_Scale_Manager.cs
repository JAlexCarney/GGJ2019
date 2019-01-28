using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialouge_Scale_Manager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.parent.localScale.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
