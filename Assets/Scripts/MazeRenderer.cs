using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{

    [SerializeField]
    [Range(1, 50)]
    private int width = 10;

    [SerializeField]
    [Range(1, 50)]
    private int height = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform[] wallPrefabs;

    void Start()
    {
        WallState[,] maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }

    private void Draw(WallState[,] maze)
    {
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)], transform) as Transform;
                    topWall.localPosition = position + new Vector3(0, 0, size / 2);
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)], transform) as Transform;
                    leftWall.localPosition = position + new Vector3(-size / 2, 0, 0);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)], transform) as Transform;
                        rightWall.localPosition = position + new Vector3(+size / 2, 0, 0);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)], transform) as Transform;
                        bottomWall.localPosition = position + new Vector3(0, 0, -size / 2);
                    }
                }
            }

        }

    }
}
