using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour {

    [SerializeField] GameObject mazeCellPrefab;
    [SerializeField] MazeCellObject[,] mazeCellObjects;
    [SerializeField] MazeData data;

    [SerializeField] int a, b;

    // This the physical size of our maze cells. Getting this wrong will result in overlapping
    // or visible gaps between each cell.
    public float cellSize = 1f;

    private MazeGenerator mazeGenerator;
    private List<GameObject> corners;

    private void Awake() 
    {
        mazeGenerator = GetComponent<MazeGenerator>();
        corners = new List<GameObject>();
    }

    private void Start() 
    {
        int level = GameManager.instance.Level;
        float r;
        if(level <= 5) r = 0.1f;
        else r = 0.2f;
        mazeGenerator.mazeHeight = (int)(8 * Mathf.Pow((1 + r), level));
        mazeGenerator.mazeWidth = (int)(10 * Mathf.Pow((1 + r), level));

        int index = 0;

        // Get our MazeGenerator script to make us a maze.
        MazeCell[,] maze = mazeGenerator.GetMaze();

        // Loop through every cell in the maze.
        for (int x = 0; x < mazeGenerator.mazeWidth; x++) 
        {
            for (int y = 0; y < mazeGenerator.mazeHeight; y++) 
            {
                // Instantiate a new maze cell prefab as a child of the MazeRenderer object.
                GameObject newCell = Instantiate(mazeCellPrefab, new Vector3((float)x * cellSize, 0f, (float)y * cellSize), Quaternion.identity, transform);

                // Get a reference to the cell's MazeCellPrefab script.
                MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();

                // Add top left and bottom left corner to the list of corners.
                corners.Add(mazeCell.topLeft);
                corners.Add(mazeCell.bottomLeft);

                // Determine which walls need to be active.
                bool top = maze[x, y].topWall;
                bool left = maze[x, y].leftWall;

                // Bottom and right walls are deactivated by default unless we are at the bottom or right
                // edge of the maze.
                bool right = false;
                bool bottom = false;
                if (x == mazeGenerator.mazeWidth - 1) 
                {
                    right = true;

                    // Add top right and bottom right corner to the list of corners.
                    corners.Add(mazeCell.topRight);
                    corners.Add(mazeCell.bottomRight);
                }
                if (y == 0) bottom = true;

                mazeCell.Init(top, bottom, right, left, true);

                if(x == mazeGenerator.mazeWidth - 1 && y == mazeGenerator.mazeHeight - 1)
                {
                    Transform destination = newCell.transform.GetChild(0);
                    destination.GetComponent<MeshRenderer>().material.color = Color.red;
                    GameObject triggerEnd = new GameObject("TriggerEnd");
                    triggerEnd.transform.parent = destination;
                    triggerEnd.transform.localPosition = new Vector3(0, 2, 0);
                    BoxCollider col =  triggerEnd.AddComponent<BoxCollider>();
                    col.size = new Vector3(4f, 0.1f, 4f);
                    col.isTrigger = true;
                }

                MazeCellData item = new MazeCellData
                {
                    coordinate = new Vector2Int(x, y), topBottomRightLeft = new List<bool>(){top, bottom, right, left}
                };

                data.mazeData[1].mazeCellData.Add(item);

                index++;
            }
        }

        foreach(GameObject corner in corners)
        {
            if(CanCreatePillar(corner))
            {
                corner.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }


    // MazeCellObject GetMazeCellObject(int a, int b)
    // {
    //     if(a < 0 || b < 0 || a > mazeGenerator.mazeWidth - 1 || b > mazeGenerator.mazeHeight - 1)
    //     {
    //         return null;
    //     }

    //     return mazeCellObjects[a, b];
    // }

    private bool CanCreatePillar(GameObject corner)
    {
        RaycastHit[] hits = new RaycastHit[4];
        bool top = Physics.Raycast(corner.transform.position + Vector3.up, Vector3.forward, out hits[0], 1f);
        bool bottom = Physics.Raycast(corner.transform.position + Vector3.up, Vector3.back, out hits[1], 1f);
        bool right = Physics.Raycast(corner.transform.position + Vector3.up, Vector3.right, out hits[2], 1f);
        bool left = Physics.Raycast(corner.transform.position + Vector3.up, Vector3.left, out hits[3], 1f);

        if(top && left || top && right || bottom && left || bottom && right) 
        {
            return true;
        }
        else if(top && !bottom && !right && !left || !top && bottom && !right && !left || !top && !bottom && right && !left || !top && !bottom && !right && left) 
        {
            return true;
        }
        else return false;
    }
}