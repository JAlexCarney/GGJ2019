using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate_Random_World : MonoBehaviour
{
    public GameObject balloon;
    public GameObject danger;
    private int num_balloons;
    private int num_dangers;
    private float max_distance;
    private float spawn_distance;

    // Start is called before the first frame update
    void Start()
    {
        num_balloons = 50;
        num_dangers = 100;
        max_distance = 50;
        spawn_distance = 7.5f;

        int i = 0;
        while (i < num_balloons)
        {
            float randx = Random.Range(-max_distance, max_distance);
            float randy = Random.Range(-max_distance, max_distance);

            if ((randx > spawn_distance || randy < spawn_distance) &&
                (randy > spawn_distance) || randy < spawn_distance)
            {
                GameObject temp_balloon = Instantiate(balloon);
                temp_balloon.transform.position = new Vector3(randx, randy, 0);
                i++;
            }
        }
        i = 0;
        while (i < num_dangers)
        {
            float randx = Random.Range(-max_distance, max_distance);
            float randy = Random.Range(-max_distance, max_distance);
            
            if ((randx > spawn_distance || randy < spawn_distance) &&
                (randy > spawn_distance) || randy < spawn_distance)
            {
                GameObject temp_danger = Instantiate(danger);
                temp_danger.transform.position = new Vector3(randx, randy, 0);
                i++;
            }
        }
    }
}
