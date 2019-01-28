using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate_Random_World : MonoBehaviour
{
    public GameObject balloon;
    public List<Vector2> balloon_positions;
    public GameObject danger;

    private int num_balloons;
    private int num_dangers;
    private float max_distance;
    private float spawn_distance;
    private float player_spawn_distance;

    // Start is called before the first frame update
    void Start()
    {
        num_balloons = 29;
        num_dangers = 100;
        max_distance = 45;
        spawn_distance = 5f;
        player_spawn_distance = 15f;

        int i = 0;
        while (i < num_balloons)
        {
            float randx = Random.Range(-max_distance, max_distance);
            float randy = Random.Range(-max_distance, max_distance);
            Vector2 pos = new Vector2(randx, randy);

            if(Mathf.Pow(pos.x, 2f) + Mathf.Pow(pos.y, 2f) > Mathf.Pow(player_spawn_distance, 2f))
            {
                bool placeable = true;
                foreach(var pos2 in balloon_positions)
                {
                    if (Mathf.Pow(pos.x-pos2.x, 2f) + Mathf.Pow(pos.y-pos2.y, 2f) < Mathf.Pow(spawn_distance, 2f))
                    {
                        placeable = false;
                        break;
                    }
                }
                if (placeable)
                {
                    balloon_positions.Add(pos);
                    GameObject temp_balloon = Instantiate(balloon);
                    temp_balloon.transform.position = new Vector3(randx, randy, 0);
                    i++;
                }
            }
        }
        i = 0;
        while (i < num_dangers)
        {
            float randx = Random.Range(-max_distance, max_distance);
            float randy = Random.Range(-max_distance, max_distance);
            Vector2 pos = new Vector2(randx, randy);

            if (Mathf.Pow(randx, 2f) + Mathf.Pow(randy, 2f) > Mathf.Pow(player_spawn_distance, 2f))
            {
                bool placeable = true;
                foreach (var pos2 in balloon_positions)
                {
                    if (Mathf.Pow(pos.x - pos2.x, 2f) + Mathf.Pow(pos.y - pos2.y, 2f) < Mathf.Pow(spawn_distance, 2f))
                    {
                        placeable = false;
                        break;
                    }
                }
                if (placeable)
                {
                    GameObject temp_danger = Instantiate(danger);
                    temp_danger.transform.position = new Vector3(randx, randy, 0);
                    i++;
                }
            }
        }
    }
}
