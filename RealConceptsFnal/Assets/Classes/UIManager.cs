using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject buttonStartGame, buttonLogout, buttonLeaderboard; //Represents the button to start the game
    public Text informUser, state; //Text field to inform the users
    private DataManager dataManager; //Data manager to hold the player information
    private bool which, forward;
    private double timer;
    public double time;

    //Constructor for the UIManager
    public UIManager()
    {

    }

    void Update()
    {
        
        if (which && forward)
        {

            state.text = "Your turn. Enter the pattern forwards";


            timer = timer - Time.deltaTime;

            if(timer<=0)
                state.text = "";


        }

        else if(which && !forward)
        {

            state.text = "Your turn. Enter the pattern backwards";


            timer = timer - Time.deltaTime;

            if (timer <= 0)
                state.text = "";

        }

    }

    //Method to set the data manager so that the user can log out
    public void SetDataManager(DataManager data)
    {
        dataManager = data;
    }


    //Method to change the text of the inform user
    public void ChangeText(string text)
    {
        informUser.text = text;
    }


    //Method to hide all the button
    public void HideButtons()
    {
        buttonStartGame.SetActive(false);
        buttonLogout.SetActive(false);
        buttonLeaderboard.SetActive(false);
    }


    //Method to open the leaderboard screen
    public void OpenLeadboard()
    {
        SceneManager.LoadScene(3);
    }


    //Method to log the player out
    public void LogOut()
    {
        //Sending the user back to the login screen
        SceneManager.LoadScene(0);
    }


    //Method to hide or show the state text field
    public void HideOrShow(bool forward)
    {

        this.forward = forward;
        which = true;
        timer = time;
    }

}