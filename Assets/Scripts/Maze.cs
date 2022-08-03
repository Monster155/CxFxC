using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    public static int Round(int value)
    {
        if (value % 2 == 0)
            return value + 1;
        return value;
    }

    public static int[,] Generate(int width, int height)
    {
        if (width < 10 || height < 10)
            return null;

        width = Round(width);
        height = Round(height);

        int x, y;

        List<int> dir = new List<int>();
        Node[,] field = new Node[width, height];

        int j = Round(Random.Range(0, height - 1));

        field[1, j].Path = true;
        field[1, j].Check = true;

        while (true)
        {
            bool finish = true;

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (!field[x, y].Path)
                        continue;
                    finish = false;

                    if (x - 2 >= 0)
                        if (!field[x - 2, y].Check)
                            dir.Add(0);

                    if (y - 2 >= 0)
                        if (!field[x, y - 2].Check)
                            dir.Add(1);

                    if (x + 2 < width)
                        if (!field[x + 2, y].Check)
                            dir.Add(2);

                    if (y + 2 < height)
                        if (!field[x, y + 2].Check)
                            dir.Add(3);

                    if (dir.Count > 0)
                    {
                        switch (dir[Random.Range(0, dir.Count)])
                        {
                            case 0:
                                field[x - 1, y].Check = true;
                                field[x - 2, y].Check = true;
                                field[x - 2, y].Path = true;
                                break;

                            case 1:
                                field[x, y - 1].Check = true;
                                field[x, y - 2].Check = true;
                                field[x, y - 2].Path = true;
                                break;

                            case 2:
                                field[x + 1, y].Check = true;
                                field[x + 2, y].Check = true;
                                field[x + 2, y].Path = true;
                                break;

                            case 3:
                                field[x, y + 1].Check = true;
                                field[x, y + 2].Check = true;
                                field[x, y + 2].Path = true;
                                break;
                        }
                    }
                    else
                    {
                        field[x, y].Path = false;
                    }

                    dir.Clear();
                }
            }

            if (finish) 
                break;
        }

        int[,] result = new int[width, height];
        for (y = 0; y < height; y++)
            for (x = 0; x < width; x++)
                if (field[x, y].Check)
                    result[x, y] = 1;
                else
                    result[x, y] = -1;

        return result;
    }

    private struct Node
    {
        public bool Path;
        public bool Check;
    }
}