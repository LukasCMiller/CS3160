using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LostGameScreenScript : MonoBehaviour
{

    public Text textInformUser; //text to inform user about there score
    private DataManager dataManager; //Data manager to hold information about the user

    // Start is called before the first frame update
    void Awake()
    {

        dataManager = FindObjectOfType<DataManager>();
        textInformUser.text = dataManager.GetUser().GetUsername() + " got a score of " + dataManager.GetUser().GetScore();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method to open the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene(3);
    }
}
