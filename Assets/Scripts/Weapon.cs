using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Bullet bullet;

    public Transform bulletSpawn;

    public float bulletSpeed = 10;

    //public float fireRate = 1;
    float timeOfNextShot;
    public float timeBetweenShots = 1;


    public void Fire(ColourType colour, Vector2 direction)
    {

        if (Time.time > timeOfNextShot)
        {
            timeOfNextShot = Time.time + timeBetweenShots;
            // spawn a bullet
            Bullet bulletCopy = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);

            bulletCopy.BulletSetup(colour, direction, bulletSpeed);
        }


    }
    

}
