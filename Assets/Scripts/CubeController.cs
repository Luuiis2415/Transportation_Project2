using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    GameController myGameController;
    public int myX, myY;


    // Start is called before the first frame update
    void Start()
    {

        myGameController = GameObject.Find("GameControllerObject").GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        myGameController.ProcessClick(gameObject, myX, myY);

        /*
        if (GameController.activeCube != null)
        {

            GameController.activeCube.GetComponent<Renderer>().material.color = Color.white;

        }

        the one that's been clicked will turn red
        and its keeping track of what's been clicked
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        GameController.activeCube = gameObject;
        */
    }
}
