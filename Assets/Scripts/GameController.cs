using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /*[System.Serializable]
    public class DeclareMultiDimensionalArray
    {
        public Element[] elem;
    }*/

    public ElemRetry[] elems;
    public Sprite[] textures;

    private void Start()
    {
        elems = FindObjectsOfType<ElemRetry>();
    }

    public int AdjacentCount(Vector3 pos)
    {
        int mines = 0;
        if (FindNearbyMines(new Vector3(pos.x - 1, pos.y + 1, pos.z))) //Top-Left
            mines++;
        if (FindNearbyMines(new Vector3(pos.x, pos.y + 1, pos.z))) //Top
            mines++;
        if (FindNearbyMines(new Vector3(pos.x + 1, pos.y + 1, pos.z))) //Top-right
            mines++;
        if (FindNearbyMines(new Vector3(pos.x + 1, pos.y, pos.z))) //Right
            mines++;
        if (FindNearbyMines(new Vector3(pos.x + 1, pos.y - 1, pos.z))) //Bottom-Right
            mines++;
        if (FindNearbyMines(new Vector3(pos.x, pos.y - 1, pos.z))) //Bottom
            mines++;
        if (FindNearbyMines(new Vector3(pos.x - 1, pos.y - 1, pos.z))) //Bottom-Left
            mines++;
        if (FindNearbyMines(new Vector3(pos.x - 1, pos.y, pos.z))) //Left
            mines++;

        return mines;
    }

    public bool FindNearbyMines(Vector3 pos)
    {
        bool isMine = false;
        foreach(ElemRetry elem in elems)
        {
            if (elem.transform.position == pos && elem.mine)
            {
                isMine = true;
            }
        }

        return isMine;
    }

    public IEnumerator RevealMines()
    {
        print("You Lose");
        foreach(ElemRetry elem in elems)
        {
            if (elem.mine)
            {
                elem.GetComponent<SpriteRenderer>().sprite = textures[9];
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
