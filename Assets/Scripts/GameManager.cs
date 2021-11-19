using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class DeclareMultiDimensionalArray
    {
        public Element[] elem;
    }

    private bool hasLost = false;

    public Sprite[] Textures;
    //The Grid itself
    public static int w = 10; //Grid Width
    public static int h = 13; //Grid Height
    public DeclareMultiDimensionalArray[] elements;

    public IEnumerator UncoverMines()
    {
        if (!hasLost)
        {
            /*Element[] blocks = FindObjectsOfType<Element>();
            for (int i = 0; i < blocks.Length; i++)
            {
                if (blocks[i].mine)
                {
                    blocks[i].GetComponent<SpriteRenderer>().sprite = Textures[9];
                    yield return new WaitForSeconds(0.1f);
                }
            }*/

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (elements[j].elem[i].mine)
                    {
                        elements[j].elem[i].GetComponent<SpriteRenderer>().sprite = Textures[9];
                        yield return new WaitForSeconds(0.0f);
                    }
                }
            }
        }
    }

    public bool MineAt(int x, int y)
    {
        //Coords in range? Then check for mine.
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            print("exists");
            elements[x].elem[y].mine = true;
            return true;
        }
        return false;
    }

    public int adjacentMines(int x, int y)
    {
        int count = 0;

        if (MineAt(x, y + 1))
            ++count; //top
        if (MineAt(x+1, y+1))
            ++count; //top-right
        if (MineAt(x + 1, y))
            ++count; //right
        if (MineAt(x+1, y-1))
            ++count; //bottom-right
        if (MineAt(x, y - 1))
            ++count; //bottom
        if (MineAt(x-1, y-1))
            ++count; //bottom-left
        if (MineAt(x - 1, y))
            ++count; //left
        if (MineAt(x-1, y+1))
            ++count; //top-left

        print(count);
        return count;
    }
}
