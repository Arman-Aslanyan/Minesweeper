using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*[System.Serializable]
    public class DeclareMultiDimensionalArray
    {
        public Element[] elem;
    }*/

    private bool hasFilled = false;
    public static int num = 0;

    public Sprite[] Textures;
    //The Grid itself
    public static int w = 10; //Grid Width
    public static int h = 13; //Grid Height
    public static Element[,] elements = new Element[w, h];
    //public DeclareMultiDimensionalArray[] elements;

    public IEnumerator UncoverMines()
    {
        Element[] elems = FindObjectsOfType<Element>();
        foreach (Element elem in elems)
        {
            if (elem.mine)
            {
                elem.GetComponent<SpriteRenderer>().sprite = Textures[9];
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public bool MineAt(int x, int y)
    {
        //Coords in range? Then check for mine
        if (x >= 0 && y >= 0 && x < w && y < h)
            return elements[x, y].mine;
        return false;
    }

    public int adjacentMines(int x, int y)
    {
        int count = 0;

        if (MineAt(x, y + 1)) ++count; // top
        if (MineAt(x + 1, y + 1)) ++count; // top-right
        if (MineAt(x + 1, y)) ++count; // right
        if (MineAt(x + 1, y - 1)) ++count; // bottom-right
        if (MineAt(x, y - 1)) ++count; // bottom
        if (MineAt(x - 1, y - 1)) ++count; // bottom-left
        if (MineAt(x - 1, y)) ++count; // left
        if (MineAt(x - 1, y + 1)) ++count; // top-left

        return count;
    }

    public bool isFinished()
    {
        //Try to find a covered element that isn't a mine
        foreach (Element elem in elements)
        {
            if (elem.IsCovered() && !elem.mine)
                return false;
        }
        //There are non => all are mines => game won.
        return true;
    }

    public void FFunCover(int x, int y, bool[,] visited)
    {
        //Coords in range?
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            //visited already?
            if (visited[x, y])
                return;

            //uncover element
            elements[x, y].LoadTexture(FindObjectOfType<GameManager>().adjacentMines(x, y));

            //close to a mine? then no more work needed here
            if (FindObjectOfType<GameManager>().adjacentMines(x, y) > 0)
                return;

            //set visited flag
            visited[x, y] = true;

            //recursion
            FFunCover(x - 1, y, visited);
            FFunCover(x + 1, y, visited);
            FFunCover(x, y - 1, visited);
            FFunCover(x, y + 1, visited);
        }
    }
}
