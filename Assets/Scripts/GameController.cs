using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cubePrefab;
    Vector3 cubePosition;
    GameObject activeCube;
    int airplaneX, airplaneY;
    GameObject[,] grid;
    int gridX, gridY;
    bool airplaneActive;



    // Start is called before the first frame update
    void Start()
    {
        gridX = 16;
        gridY = 9;
        grid = new GameObject[gridX, gridY];

        for (int y = 0; y < gridY; y++)
        { for (int x = 0; x < gridX; x++)
            {
                cubePosition = new Vector3(x * 2, y * 2, 0);
                grid[x,y] = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
                grid[x, y].GetComponent<CubeController>().myX = x;
                grid[x, y].GetComponent<CubeController>().myY = y;
            }
        }

        airplaneX = 0;
        airplaneY = 8;
        grid[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.red;
        airplaneActive = false;


    }

    public void ProcessClick(GameObject clickedCube, int x, int y)
    {
        //first two parts use the same if statement but one activates and the other deactivates
        if (x == airplaneX && y == airplaneY)
        {
            if (airplaneActive)
            {
                airplaneActive = false;
                clickedCube.GetComponent<Renderer>().material.color = Color.red;
            }

            else
            {
                airplaneActive = true;
                clickedCube.GetComponent<Renderer>().material.color = Color.blue;
            }
        }

        //if the sky is clicked (whitecube) instead of the airplane
        else
        {
            if (airplaneActive)
            {
                grid[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
                

                airplaneX = x;
                airplaneY = y;
                grid[x, y].GetComponent<Renderer>().material.color = Color.red;
                grid[x, y].GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
