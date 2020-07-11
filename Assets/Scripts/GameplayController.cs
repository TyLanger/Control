using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{

    // instance
    public static GameplayController Instance { get; private set; }

    public float globalMoveSpeed = 2;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color ColourFromColourType(ColourType c)
    {
        switch (c)
        {
            case ColourType.White:
                return Color.white;
                break;

            case ColourType.Green:
                return Color.green;
                break;

            case ColourType.Pink:
                return Color.magenta;
                break;

            case ColourType.Orange:
                return new Color(1, 0.5f, 0.07f);
                break;
        }
        return Color.black;
    }
}
