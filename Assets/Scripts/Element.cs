using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    //Is a mine?
    public bool mine;
    public static bool spriteIsMine = false;
    //Row and Column
    public int row;
    public int num;

    //Different Textures
    public Sprite[] emptyTextures;

    // Start is called before the first frame update
    void Start()
    {
        //Randomly decide if gameObject is a mine or not
        mine = Random.value < 0.15;
        emptyTextures = FindObjectOfType<GameManager>().Textures;

        //Register Grid
        FindObjectOfType<GameManager>().elements[row - 1].elem[num - 1] = this;
    }

    public void LoadTexture(int adjacentCount)
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        if (mine && !spriteIsMine)
        {
            spr.sprite = emptyTextures[9];
            spriteIsMine = true;
        }
        else
            spr.sprite = emptyTextures[adjacentCount];
        print("Sprite Change");
    }

    private void OnMouseUpAsButton()
    {
        //gameObject is a mine
        if (mine)
        {
            StartCoroutine(FindObjectOfType<GameManager>().UncoverMines());
            Debug.Log("You Lose");
        }
        //Otherwise is not one
        else
        {
            //show adjacent mine number
            LoadTexture(FindObjectOfType<GameManager>().adjacentMines(row - 1, num - 1));
        }
    }

    public bool IsCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.name == "Blocks_0";
    }
}
