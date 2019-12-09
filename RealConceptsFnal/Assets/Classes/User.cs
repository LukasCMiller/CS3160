using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to represent the user of the corsi task
//This class will have a username, password, score and path the there indivdual folder in order to track all information
public class User : MonoBehaviour
{
    private string username;
    private string password;
    private int score;
    private string path;

    //Constructor for the User class
    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
        score = 0; //Score will always equal 0 when the user is first initialized
        path = username;
    }//End constructor


    //*******************************************************
    //THE FOLLOWING SECTION IS JUST GETTERS AND SETTERS
    //*******************************************************

    public void SetScore(int score)
    {
        this.score = score;
    }

    public string GetUsername()
    {
        return username;
    }

    public string GetPassword()
    {
        return password;
    }

    public int GetScore()
    {
        return score;
    }

    public string GetPath()
    {
        return path;
    }

    //*******************************************************
    //END GETTERS AND SETTERS
    //*******************************************************


    //Method to increase the score of the player by one
    public void IncreaseScore()
    {
        score++;
    }

}
