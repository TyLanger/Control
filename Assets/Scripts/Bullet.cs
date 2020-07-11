using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    ColourType myColour;

    public float moveSpeed = 10;

    float maxLifetime = 4;

    void Awake()
    {
        Invoke("Cleanup", maxLifetime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // travel straight
        transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.up, moveSpeed * Time.fixedDeltaTime);
    }

    public void BulletSetup(ColourType colour, Vector2 direction, float speed)
    {
        transform.up = direction;
        myColour = colour;

        GetComponent<SpriteRenderer>().color = GameplayController.Instance.ColourFromColourType(myColour);
        moveSpeed = speed;
    }

    public ColourType GetColour()
    {
        return myColour;
    }

    public void Cleanup()
    {
        // add back to item pool
        Destroy(gameObject);
    }
}
