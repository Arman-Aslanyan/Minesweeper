using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    //Is a mine?
    public bool mine;
    //Row and Column
    public int row;
    public int num;

    //Different Textures
    public Sprite[] emptyTextures;
    public Sprite mineTexture;

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
        if (mine)
            GetComponent<SpriteRenderer>().sprite = emptyTextures[9];
        else
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
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
            LoadTexture(FindObjectOfType<GameManager>().adjacentMines(row, num));
            Debug.Log("test");
        }
    }

    public bool IsCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.name == "Blocks_0";
    }
}
