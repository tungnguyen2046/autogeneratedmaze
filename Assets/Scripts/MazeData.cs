using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MazeData", menuName = "Maze/MazeData", order = 0)]
public class MazeData : ScriptableObject 
{
    public int mazeWidth, mazeHeight;
    public List<MazeSeedData> mazeData;
}

[Serializable]
public class MazeSeedData
{
    public List<MazeCellData> mazeCellData;
}

[Serializable] 
public class MazeCellData
{
    public Vector2Int coordinate;
    public List<bool> topBottomRightLeft;
}