using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public Sprite Pink;
    public Sprite Blue;
    public string startDirection;

    private Vector2 direction;
    private Vector2 bounceDirection;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioSource audio;
    private bool isPink;
    private bool started;
    private bool bounce;

    private Vector2 north = new Vector2(0f, 1f);
    private Vector2 east = new Vector2(1f, 0f);
    private Vector2 south = new Vector2(0f, -1f);
    private Vector2 west = new Vector2(-1f, 0f);

    private Vector2 spawn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();

        switch (startDirection)
        {
            case "north":
                direction = north;
                break;
            case "east":
                direction = east;
                break;
            case "south":
                direction = south;
                break;
            case "west":
                direction = west;
                break;
        }

        sr.color = Color.magenta;
        isPink = true;
        started = false;
        bounce = false;
        spawn = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Starts the game
        if (Input.GetButtonDown("Start"))
        {
            started = true;
        }

        //Switches colour
        if (Input.GetButtonDown("Switch"))
        {
            Switch();
        }

        //Respawns on out of bounds The boundaries are currently hardcoded and may need to change
        if (this.transform.position.y > 5.5 || this.transform.position.y < -5.5 || this.transform.position.x > 7.5 || this.transform.position.x < -7.5)
        {

            transform.position = spawn;
            //Is started needs to be reset. The downfall to calling start is that it defaults to pink
            Start();
            
        }
     
    }

    private void FixedUpdate()
    {
        //Movement
        if (started)
        {
            rb.MovePosition(rb.position + (direction * speed) * Time.deltaTime);

            //bounce off the walls
            if (bounce)
            {
                rb.MovePosition(rb.position + bounceDirection / 5);
                
                bounce = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "wall")
        {
            Bump();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "goal")
        {
            GameManage.LevelComplete();
        }
    }

    //Switches the colour and turn behaviour on fire.
    private void Switch()
    {
        if(isPink){
            isPink = false;
            sr.color = Color.cyan; //change sprite to blue here
        }
        else
        {
            isPink = true;
            sr.color = Color.magenta; //change sprite to pink here
        }
    }

    //On collision with a wall, change the direction of movement depending on the player's current colour
    private void Bump()
    {
        bounce = true;
        audio.Play();
        

        if (isPink)
        {
            if(direction == north)
            {
                bounceDirection = south;
                direction = east;
            }
            else if(direction == east)
            {
                bounceDirection = west;
                direction = south;
            }
            else if(direction == south)
            {
                bounceDirection = north;
                direction = west;
            }
            else
            {
                bounceDirection = east;
                direction = north;
            }
        }
        else
        {
            if (direction == north)
            {
                bounceDirection = south;
                direction = west;
            }
            else if (direction == east)
            {
                bounceDirection = west;
                direction = north;
            }
            else if (direction == south)
            {
                bounceDirection = north;
                direction = east;
            }
            else
            {
                bounceDirection = east;
                direction = south;
            }
        }
        
    }

}
