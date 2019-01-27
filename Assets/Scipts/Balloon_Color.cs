using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Color : MonoBehaviour
{
    public Sprite[] balloon_colors;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = balloon_colors[Random.Range(0,balloon_colors.Length)];
    }
}
