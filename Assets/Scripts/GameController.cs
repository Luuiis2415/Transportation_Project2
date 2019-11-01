using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cubePrefab;
    Vector3 cubePosition;
    GameObject activeCube;
    int airplaneX, airplaneY, startX, startY;
    int depotX, depotY;
    GameObject[,] grid;
    int gridX, gridY;
    bool airplaneActive;
    float turnLength, turnTimer;
    int airplaneCargo, airplaneCargoMax;
    int cargoBuildUp;
    int score;


    // Start is called before the first frame update
    void Start()
    {
        turnLength = 1.5f;
        turnTimer = turnLength;

        score = 0;

        airplaneCargo = 0;
        airplaneCargoMax = 90;
        cargoBuildUp = 10;

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

        startX = 0;
        startY = gridY - 1;
        airplaneX = startX;
        airplaneY = startY;
        grid[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.red;
        airplaneActive = false;
        depotX = gridX - 1;
        depotY = 0;
        grid[depotX, depotY].GetComponent<Renderer>().material.color = Color.black;
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
                grid[depotX, depotY].GetComponent<Renderer>().material.color = Color.black;

                airplaneX = x;
                airplaneY = y;
                grid[x, y].GetComponent<Renderer>().material.color = Color.red;
                grid[x, y].GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }

    void LoadCargo()
    {
        if(airplaneX == startX && airplaneY == startY)
        {
            airplaneCargo += cargoBuildUp;
            
            if(airplaneCargo > airplaneCargoMax)
            {
                airplaneCargo = airplaneCargoMax;
            }

        }
    }

    void DeliverCargo()
    {
        if (airplaneX == depotX && airplaneY == depotY)
        {
            score += airplaneCargo;
            airplaneCargo = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time > turnTimer)
        {
            LoadCargo();
            DeliverCargo();
            print("Cargo: " + airplaneCargo + " Score: " + score);

            turnTimer += turnLength;

        }


    }
}
