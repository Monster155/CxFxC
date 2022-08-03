using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float sampleSize = 1;
    
    [Header("Borders")]
    [SerializeField] private GameObject[] walls;
    [SerializeField] private GameObject[] corners;

    [Header("Inside")] 
    [SerializeField] private GameObject[] buildings;
    [SerializeField] private GameObject[] plants;

    private int[,] _map;

    private void Start() 
        => CreateMap();

    public void Clear()
    {
        for(int i = 0; i < transform.childCount; i++) 
            DestroyImmediate(transform.GetChild(i).gameObject);
    }
	
    public void CreateMap()
    {
        Clear();

        _map = Maze.Generate(width, height);

        if(_map == null) 
            return;

        width = Maze.Round(width);
        height = Maze.Round(height);

        float posX = -sampleSize * width / 2f - sampleSize / 2f;
        float posY = sampleSize * height / 2f - sampleSize / 2f;
        float xReset = posX;
       
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                posX += sampleSize;
                if (_map[x, y] != 1)
                {
                    GameObject tmpCube;
                    if ((x == 0 || x == width - 1) && (y == 0 || y == height - 1))
                    {
                        tmpCube = Instantiate(corners[Random.Range(0, corners.Length)], transform);
                    }
                    else if(x == 0 || x == width - 1)
                    {
                        tmpCube = Instantiate(Random.value < 0.8 ? walls[0] : walls[Random.Range(1, walls.Length)], transform);
                        tmpCube.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (y == 0 || y == height - 1)
                    {
                        tmpCube = Instantiate(Random.value < 0.8 ? walls[0] : walls[Random.Range(1, walls.Length)], transform);
                    }
                    else
                    {
                        tmpCube = Instantiate(buildings[Random.Range(0, buildings.Length)], transform);
                    }
                    
                    tmpCube.transform.localPosition = new Vector3(posX, 0.5f, posY);
                }
                else
                {
                    if(Random.value > 0.1)
                        continue;
                    
                    GameObject plant = Instantiate(plants[Random.Range(0, plants.Length)], transform);
                    plant.transform.localPosition = new Vector3(posX, 0.5f, posY);
                    plant.transform.rotation = Quaternion.Euler(0, Random.Range(0, 180), 0);
                }
            }
            posY -= sampleSize;
            posX = xReset;
        }
    }
}
