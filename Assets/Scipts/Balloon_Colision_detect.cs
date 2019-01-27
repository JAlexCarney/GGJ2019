using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Colision_detect : MonoBehaviour
{
    public Sprite poped_sprite;

    // called when this balloon hits something
    void OnCollisionEnter2D(Collision2D col)
    {
        // is it dangerous?
        if ( col.gameObject.tag == "Danger" )
        {
            // POP!
            GetComponent<SpriteRenderer>().sprite = poped_sprite;
            Destroy(gameObject, .5f);
            
        }
    }
}
