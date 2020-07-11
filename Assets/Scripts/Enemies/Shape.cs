using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColourType { White, Pink, Green, Orange };

public class Shape : MonoBehaviour
{

    // shapes are the enemies of the game
    // every enemy will extend from this


    ColourType colourType;

    protected float moveSpeed = 2f;
    protected Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        colourType = ColourType.White;
    }

    public void SetColour(ColourType c)
    {
        colourType = c;
    }

    void OnHit(ColourType bulletColour)
    {
        if(colourType == ColourType.White)
        {
            // any bullet is good enough
            Die();
        }

        if(bulletColour == colourType)
        {
            // bullet matches my colour
            Die();
        }
        else if(colourType == ColourType.Orange)
        {
            // I'm orange
            // swap to the opposite colour
            if(bulletColour == ColourType.Green)
            {
                // change to pink
                colourType = ColourType.Pink;
            }
            else if(bulletColour == ColourType.Pink)
            {
                colourType = ColourType.Green;
            }
        }
        else
        {
            // I'm not white
            // the bullet colour didn't match my colour
            // I'm also not orange
            // GETANGRY()
        }
    }

    void Die()
    {
        // play particle effects and stuff
        // maybe add yourself back to storage instead of deleting yourself
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // do something to the player
            Die();
        }
        else if(col.CompareTag("Bullet"))
        {
            Bullet b = col.GetComponent<Bullet>();
            OnHit(b.GetColour());
            b.Cleanup();

        }
    }
}
