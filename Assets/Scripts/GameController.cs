using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cubePrefab;
    Vector3 cubePosition;

    //this is one way to do the 2a of part1
    //after I can try experiementing the other ways

    public static GameObject activeCube;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 16; i++)
        {

            cubePosition = new Vector3(i*2, 0, 0);
            Instantiate(cubePrefab, cubePosition, Quaternion.identity);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
