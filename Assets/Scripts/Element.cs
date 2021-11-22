using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    //Is a mine?
    public bool mine;

    public int row = 0;
    public int column = 0;

    //Different Textures
    public Sprite[] emptyTextures;

    // Start is called before the first frame update
    void Start()
    {
        //Randomly decide if gameObject is a mine or not
        mine = Random.value < 0.15;
        emptyTextures = FindObjectOfType<GameManager>().Textures;

        //Register Grid
        gameObject.name += " " + GameManager.num;
        GameManager.num++;
        GameManager.elements[column - 1, row - 1] = this;

        //OnMouseUpAsButton();
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
        if (mine)
        {
            //Uncover mines
            StartCoroutine(FindObjectOfType<GameManager>().UncoverMines());

            print("you lose");
        }
        else
        {
            //show adjacent mine number
            LoadTexture(FindObjectOfType<GameManager>().adjacentMines(column, row));

            //uncover area without mines
            FindObjectOfType<GameManager>().FFunCover(column, row, new bool[GameManager.w, GameManager.h]);

            //Check if game finished
            if (FindObjectOfType<GameManager>().isFinished())
                print("You Won!!");
        }
    }

    public bool IsCovered()
    {
        return GetComponent<SpriteRenderer>().sprite = emptyTextures[0];
    }
}
