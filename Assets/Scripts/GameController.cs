﻿using System.Collections;
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
    int moveY, moveX;
    int targetX, targetY;



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
        targetX = airplaneX;
        targetY = airplaneY;
        grid[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.red;
        airplaneActive = false;
        depotX = gridX - 1;
        depotY = 0;
        grid[depotX, depotY].GetComponent<Renderer>().material.color = Color.black;

        moveX = 0;
        moveY = 0;
    }

    public void ProcessClick(GameObject clickedCube, int x, int y)
    {
        //first two parts use the same if statement but one activates and the other deactivates
        if (x == airplaneX && y == airplaneY)
        {
            if (airplaneActive)
            {
                //we deactivate here
                airplaneActive = false;
                clickedCube.GetComponent<Renderer>().material.color = Color.red;
            }

            else
            {
                //we activate here
                airplaneActive = true;
                clickedCube.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
        else if (airplaneActive)
        {
            targetX = x;
            targetY = y;
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

    void CalculateDirection()
    {
        //can do it this way or a more clearer way would be a else statement
        
       // moveX = 0;
       // moveY = 0;
        


        // every 30th of a second im setting these variables, to whats appropriate
        // the down arrow has taken priority over the up arrow and
        // the right has taken priority over the left b/c i used else if
        if (airplaneY > targetY)
        {
            moveY = -1;

        }
        else if (airplaneY < targetY)
        {
            moveY = 1;
        }
        else
        {
            moveY = 0;
        }

        if (airplaneX < targetX)
        {
            moveX = 1;
        }
        else if (airplaneX > targetX)
        {
            moveX = -1;
        }
        else
        {
            moveX = 0;
        }

    }

    void MoveAirplane()
    {
        CalculateDirection();


        // we have to check if the airplane is active in order to move it
        if (airplaneActive)
        {
            grid[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
            grid[depotX, depotY].GetComponent<Renderer>().material.color = Color.black;

            //right here we put the airplane in its new spot
            airplaneX += moveX;
            airplaneY += moveY;

            //it continues to move so gotta make sure it doesn't leave screen
            // using this method because its checking the actual movement of the plane
            if (airplaneX >= gridX)
            {
                airplaneX = gridX - 1;
            }
            else if(airplaneX < 0)
            {
                airplaneX = 0;
            }

            if(airplaneY >= gridY)
            {
                airplaneY = gridY - 1;
            }
            else if (airplaneY < 0)
            {
                airplaneY = 0;
            }

            grid[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time > turnTimer)
        {
            MoveAirplane();


            LoadCargo();
            DeliverCargo();
            print("Cargo: " + airplaneCargo + " Score: " + score);

            turnTimer += turnLength;

        }


    }
}
