using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElemRetry : MonoBehaviour
{
    public bool mine = false;
    private SpriteRenderer spr;
    private GameController GM;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseUpAsButton()
    {
        if (mine)
            StartCoroutine(GM.RevealMines());
        else
            LoadTexture(FindObjectOfType<GameController>().AdjacentCount(transform.position));
    }

    public void LoadTexture(int adjacentCount)
    {
        if (mine)
            spr.sprite = GM.textures[9];
        else
            spr.sprite = GM.textures[adjacentCount];
    }
}
