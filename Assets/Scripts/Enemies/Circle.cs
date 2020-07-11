using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape
{
   
    // circle tries to move towards the player

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.fixedDeltaTime);
    }

}
