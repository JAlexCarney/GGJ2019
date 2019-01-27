using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{
    public GameObject ballon_prefab;
    public GameObject game_over_prefab;

    private bool is_over;

    private float max_speed;
    private float acceleration;
    private float string_length;
    private Rigidbody2D body;
    private Animator animator;
    private bool facing_right;

    private List<GameObject> balloons;


    // Start is called before the first frame update
    void Start()
    {
        is_over = false;

        max_speed = 7.5f;
        acceleration = 4f;
        body = GetComponent<Rigidbody2D>();
        body.drag = 1f;
        body.angularDrag = 2f;

        animator = GetComponent<Animator>();
        facing_right = true;

        balloons = new List<GameObject>();
        string_length = 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Is_Balloon_Missing())
        {
            // game over
            if (!is_over)
            {
                is_over = true;
                GameObject game_over = Instantiate(game_over_prefab);
                game_over.transform.position = transform.position + new Vector3(0, -3, 0);
                Debug.Log("Game Over");
                foreach (var balloon in balloons)
                {
                    //ballon_prefab.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }
                //body.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                if (Input.GetKey(KeyCode.R))
                {
                    Debug.Log("Restarting");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
        else
        {
            // still playing
            Input_Handle();
            Update_Balloons();
            Update_Animation();
        }
    }

    void Add_Balloon(Sprite balloon_color)
    {
        Vector3 balloon_pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        float balloon_offset = Random.Range(1f, 2f);
        float balloon_offset_angle = Random.Range(0, Mathf.PI);
        balloon_pos.x = Mathf.Cos(balloon_offset_angle) * balloon_offset;
        balloon_pos.y = Mathf.Sin(balloon_offset_angle) * balloon_offset;
        GameObject balloon_temp = Instantiate(ballon_prefab);
        balloon_temp.transform.position = balloon_pos + transform.position;
        balloon_temp.GetComponent<SpriteRenderer>().sprite = balloon_color;
        balloons.Add(balloon_temp);
        Debug.Log(balloons.Count);
    }

    void Add_Balloon(Sprite balloon_color, Vector3 pos)
    {
        Vector3 balloon_pos = pos;
        GameObject balloon_temp = Instantiate(ballon_prefab);
        balloon_temp.transform.position = balloon_pos;
        balloon_temp.GetComponent<SpriteRenderer>().sprite = balloon_color;
        balloons.Add(balloon_temp);
        Debug.Log(balloons.Count);
    }

    bool Is_Balloon_Missing()
    {
        foreach (var balloon in balloons)
        {
            if (!balloon)
            {
                return true;
            }
        }
        return false;
    }

    // called when this Player hits something
    void OnTriggerEnter2D(Collider2D col)
    {
        // is it a balloon?
        if (col.gameObject.tag == "Balloon_Pickup")
        {
            // Pick it up!
            Add_Balloon(col.gameObject.GetComponent<SpriteRenderer>().sprite, col.gameObject.transform.position);
            Destroy(col.gameObject, 0f);
        }
    }

    void Update_Balloons()
    {
        // add force in direction of player relative to distance from player
        foreach (var balloon in balloons)
        {
            Rigidbody2D balloon_body = balloon.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2();

            float distance = (transform.position - balloon.transform.position).magnitude;
            if (distance > string_length)
            {
                float y_distance = balloon.transform.position.y - transform.position.y;
                float x_distance = balloon.transform.position.x - transform.position.x;

                float force_angle;
                // first quadrent
                if (y_distance >= 0 && x_distance >= 0)
                { force_angle = Mathf.PI + ((Mathf.PI / 2) - Mathf.Sin(Mathf.Abs(x_distance / distance))); }
                // second quadrent
                else if (y_distance >= 0 && x_distance <= 0)
                { force_angle = ((3f / 2f) * Mathf.PI) + Mathf.Sin(Mathf.Abs(x_distance / distance)); }
                // third quadrent
                else if (y_distance <= 0 && x_distance <= 0)
                { force_angle = (Mathf.PI / 2) - Mathf.Sin(Mathf.Abs(x_distance / distance)); }
                // fourth quadrent
                else
                { force_angle = (Mathf.PI / 2) + Mathf.Sin(Mathf.Abs(x_distance / distance)); }


                force.x = distance * Mathf.Cos(force_angle);
                force.y = distance * Mathf.Sin(force_angle);

                balloon_body.AddForce(force);
            }
            else
            {
                balloon_body.AddForce(new Vector3(0f,0.1f,0f));
            }

            // draw strings
            LineRenderer LineRend = balloon.GetComponent<LineRenderer>();
            LineRend.positionCount = 2;
            LineRend.SetPosition(0, balloon.transform.position + new Vector3(0,-0.39f,0));
            LineRend.SetPosition(1, transform.position);
        }
    }

    void Input_Handle()
    {
        Vector2 in_vec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (in_vec.x != 0 && Mathf.Abs(body.velocity.x) < max_speed)
        {
            // move left
            if (in_vec.x < 0)
            {
                body.AddForce(new Vector2(-acceleration, 0));
            }
            // move right
            if (in_vec.x > 0)
            {
                body.AddForce(new Vector2(acceleration, 0));
            }
        }
        if (in_vec.y != 0 && Mathf.Abs(body.velocity.y) < max_speed)
        {
            // move down
            if (in_vec.y < 0)
            {
                body.AddForce(new Vector2(0, -acceleration));
            }
            // move up
            if (in_vec.y > 0)
            {
                body.AddForce(new Vector2(0, acceleration));
            }
        }

        if (in_vec.x != 0 || in_vec.y != 0)
            body.drag = 1f; // speed up
        else
            body.drag = 10f; // slow down

    }

    void Update_Animation()
    {
        float x_speed = body.velocity.x;
        float y_speed = body.velocity.y;
        float speed = body.velocity.magnitude;
        if (x_speed < -0.1f && facing_right)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facing_right = false;
        }
        else if (x_speed > 0.1f && !facing_right)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facing_right = true;
        }
        if (speed < 0.1f)
        {
            animator.SetBool("is_idle", true);
            animator.SetBool("is_up", false);
            animator.SetBool("is_down", false);
            animator.SetBool("is_right", false);
        }
        else
        {
            animator.SetBool("is_idle", false);
            animator.SetBool("is_up", Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") == 0);
            animator.SetBool("is_down", Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0);
            animator.SetBool("is_right", Input.GetAxis("Horizontal") != 0);
        }
    }
}