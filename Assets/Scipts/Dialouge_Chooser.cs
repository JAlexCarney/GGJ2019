using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialouge_Chooser : MonoBehaviour
{
    public Sprite[] dialouge_boxes;
    public static List<int> used_prompt_indexes = new List<int>(); 
    // Start is called before the first frame update
    void Start()
    {
        int prompt_index = Random.Range(0, dialouge_boxes.Length);
        while (used_prompt_indexes.Contains(prompt_index))
        {
            prompt_index = Random.Range(0, dialouge_boxes.Length);
        }
        GetComponent<SpriteRenderer>().sprite = dialouge_boxes[prompt_index];
    }
}
