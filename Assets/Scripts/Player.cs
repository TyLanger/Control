using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float acceleration = 0.1f;

    Camera mainCam;

    public Weapon greenWeapon;
    public Weapon pinkWeapon;
    bool greenWeaponActive = true;

    public Transform leftHandTransform;
    public Transform rightHandTransform;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        greenWeapon.transform.parent = transform;
        pinkWeapon.transform.parent = transform;
        greenWeapon.transform.SetPositionAndRotation(leftHandTransform.position, leftHandTransform.rotation);
        pinkWeapon.transform.SetPositionAndRotation(rightHandTransform.position, rightHandTransform.rotation);
        

    }

    // Update is called once per frame
    void Update()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical");

        transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(horInput, vertInput), moveSpeed * Time.fixedDeltaTime);

        //look at the mouse
        float camDist = mainCam.transform.position.z - transform.position.z;
        Vector3 mouse = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDist));
        float angleRad = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, (angleDeg - 90) % 360);

        if(Input.GetMouseButton(0))
        {
            // left click
            // shoot
            if(greenWeaponActive)
            {
                greenWeapon.Fire(ColourType.Green, transform.up);
            }
            else
            {
                pinkWeapon.Fire(ColourType.Pink, transform.up);
            }

        }

        if(Input.GetButtonDown("SwapWeapons"))
        {
            SwapWeapons();
        }
    }

    void SwapWeapons()
    {
        greenWeaponActive = !greenWeaponActive;

        // hide the weapon not being used
    }

    void PickupWeapon(Weapon newWeapon)
    {
        if(greenWeaponActive)
        {
            // drop old weapon
            Destroy(greenWeapon);
            greenWeapon = newWeapon;
            newWeapon.transform.SetPositionAndRotation(leftHandTransform.position, leftHandTransform.rotation);
        }
        else
        {
            // drop old weapon
            Destroy(pinkWeapon);
            pinkWeapon = newWeapon;
            newWeapon.transform.SetPositionAndRotation(rightHandTransform.position, rightHandTransform.rotation);
        }
        newWeapon.transform.parent = transform;
    }
}
