using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : Shape
{

    public enum TriangleStates {  Spinning, ChargeUp, Dash, Decel};
    public TriangleStates currentState;

    public float minSpinTime = 1.5f;
    public float maxSpinTime = 3f;
    float spinStopTime;
    public float spinRate = 1;

    public float chargeUpTime = 3f;
    float chargeTime;

    public float dashMultiplier = 5;

    public float deceleration = 0.15f;
    float decelSpeed;

    // triangle shoots off straight in a direction
    // it bounces off the walls

    float timeOfLastCollision = 0;
    float timeBetweenBounces = 0.2f;

    void Start()
    {
        //transform.RotateAround(transform.position, Vector3.forward, Random.Range(0, 360));
        transform.RotateAround(transform.position, Vector3.forward, -30);
        Physics2D.queriesStartInColliders = false;
        currentState = TriangleStates.Spinning;
        RandomizeSpinStopTime();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState == TriangleStates.Spinning)
        {
            if (Time.time < spinStopTime)
            {
                transform.RotateAround(transform.position, Vector3.forward, spinRate);
            }
            else
            {
                // make this a little smarter
                // so you don't just spin and face a wall
                currentState = TriangleStates.ChargeUp;
                chargeTime = Time.time + chargeUpTime;
            }
        }
        else if (currentState == TriangleStates.ChargeUp)
        {
            if (Time.time > chargeTime)
            {
                // fully charged
                currentState = TriangleStates.Dash;
            }
        }
        else if (currentState == TriangleStates.Dash)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.up, dashMultiplier * moveSpeed * Time.fixedDeltaTime);
            RaycastHit2D hit;
            LayerMask mask = LayerMask.NameToLayer("Wall");
            hit = Physics2D.Raycast(transform.position, transform.up, 6f, ~mask);
            //Debug.DrawRay(transform.position, transform.up, Color.red, 10);
            //Debug.Log("hit dist: " + hit.distance);
            if (hit.collider != null)
            {
                //Debug.Log("Hit " + hit.collider.gameObject);
                if (hit.distance < 6f)
                {
                    currentState = TriangleStates.Decel;
                    decelSpeed = moveSpeed * dashMultiplier;
                }
            }
        }
        else if(currentState == TriangleStates.Decel)
        {
            decelSpeed *= deceleration;
            decelSpeed -= 0.03f; // decay so your speed gets to 0
            transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.up, decelSpeed * Time.fixedDeltaTime);
            if(decelSpeed < 0)
            {
                currentState = TriangleStates.Spinning;
                RandomizeSpinStopTime();
            }
        }
    }

    void RandomizeSpinStopTime()
    {
        spinStopTime = Time.time + Random.Range(minSpinTime, maxSpinTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // keeping in the bouncing for now
        // even with the raycasts and deceleration, sometimes it still hits the walls

        // don't collide with the same wall twice
        if (Time.time > timeOfLastCollision + timeBetweenBounces)
        {
            timeOfLastCollision = Time.time;
            if (col.gameObject.CompareTag("Wall"))
            {
                // hit a wall
                // find the normal of the surface I hit
                RaycastHit2D hit;
                LayerMask mask = LayerMask.NameToLayer("Wall");
                hit = Physics2D.Raycast(transform.position, transform.up, 5f);

                if (hit.normal != null)
                {
                    transform.up = Vector2.Reflect(transform.up, hit.normal);
                }
            }
        }
    }
}
