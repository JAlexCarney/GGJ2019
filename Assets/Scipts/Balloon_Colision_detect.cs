using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Colision_detect : MonoBehaviour
{
    public GameObject pop_prefab;

    // called when this balloon hits something
    void OnCollisionEnter2D(Collision2D col)
    {
        // is it dangerous?
        if ( col.gameObject.tag == "Danger" )
        {
            // POP!
            GameObject pop = Instantiate(pop_prefab);
            pop.transform.position = transform.position;
            Destroy(gameObject, 0f);
            
        }
    }
}
