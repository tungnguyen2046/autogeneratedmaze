using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject floor;
    
    public GameObject topRight;
    public GameObject topLeft;
    public GameObject bottomRight;
    public GameObject bottomLeft;

    public void Init(bool top, bool bottom, bool right, bool left, bool walkable)
    {
        topWall.SetActive(top);
        bottomWall.SetActive(bottom);
        leftWall.SetActive(left);
        rightWall.SetActive(right);
        floor.SetActive(walkable);
    }
}
