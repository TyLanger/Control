using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Shape
{

    public float timeBetweenDirectionChanges = 3;
    float timeOfNextChange;
    Vector3 currentDirection;
    

    // Start is called before the first frame update
    void Start()
    {
        RandomizeDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + currentDirection, moveSpeed * Time.fixedDeltaTime);

        if(Time.time > timeOfNextChange)
        {
            RandomizeDirection();
        }
    }

    void RandomizeDirection()
    {

        // I don't know if I want it to go straight back the way it came. 

        Vector3 lastDir = currentDirection;
        do
        {
            int r = Random.Range(0, 4); // 0, 1, 2, 3

            switch (r)
            {
                case 0:
                    currentDirection = new Vector3(1, 0, 0);
                    break;

                case 1:
                    currentDirection = new Vector3(-1, 0, 0);
                    break;

                case 2:
                    currentDirection = new Vector3(0, 1, 0);
                    break;

                case 3:
                    currentDirection = new Vector3(0, -1, 0);
                    break;
            }
        } while (lastDir == currentDirection);
        // if the new current dir is the same as the last dir, randomize it again

        timeOfNextChange = Time.time + timeBetweenDirectionChanges;
    }
}
