using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will control the behaviour of all the sprites so that on a button click something happens
public class ButtonClick : MonoBehaviour
{

    private SpriteRenderer block; //Represents the block

    public int blockNumber; //Number of the block so we know which one

    private GameManager gameManager; //Game manager as to know if the block we clicked matches the sequence 

    // Start is called before the first frame update
    void Start()
    {

        block = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method to change the color of the block to be the brighter version of the color indicate being pressed
    void OnMouseDown()
    {
        block.color = new Color(block.color.r, block.color.g, block.color.b, 1f);
    }//end OnMouseDown


    //Method to change the color of the block back to the dimmer version indicate not being pressed anymore
    void OnMouseUp()
    {
        block.color = new Color(block.color.r, block.color.g, block.color.b, .5f);
        gameManager.WhichPressed(blockNumber);
    }
}
