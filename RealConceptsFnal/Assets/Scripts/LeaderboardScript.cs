using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LeaderboardScript : MonoBehaviour
{

    //The text boxes that represnts the leaderboard
    public Text globalLeaderboard;
    public Text personalLeadboard;

    //The datamanager to get theplayer object
    private DataManager dataManager;

    //String to contain the actual leaderboard
    string scores;

    // Start is called before the first frame update
    void Start()
    {

        dataManager = FindObjectOfType<DataManager>();
        //The two strings to be the global and local leaderboards
        string globalHeader = "Global Scores";
        string localHeader = dataManager.GetUser().GetUsername() + " Scores";

       
        //Printing the leaderboard
        DisplayLeaderboard("highscore.txt", globalLeaderboard, globalHeader);
        globalLeaderboard.text = scores;
        DisplayLeaderboard(dataManager.GetUser().GetUsername() + "/" + dataManager.GetUser().GetUsername() + "sHighscores.txt", personalLeadboard, localHeader);
        personalLeadboard.text = scores;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Method to display the first 10 users from the leaderboard file
    public void DisplayLeaderboard(string path, Text leaderboard, string header)
    {

        //Read all the lines of the file on the path
        string[] lines = File.ReadAllLines(path);
        
        //Move through the array to print out the scores
        for (int i = 0; i < 11; i++)
        {

            //On the first time print out the header and none of the scores
            if (i == 0)
            {
                string tempLine = dataManager.GetEncrypter().EncryptDecrypt(lines[i]);
                scores = header + "\n";
                string[] line = tempLine.Split(',');
                scores = scores + (line[0] + "\t" + line[1] + "\n");
            }

            else if(i < lines.Length)
            {
                //Break up the string into an array to get rid of the comma tehn combine it with the previous parts
                string tempLine = dataManager.GetEncrypter().EncryptDecrypt(lines[i]);
                string[] line = tempLine.Split(',');
                scores = scores + (line[0] + "\t" + line[1] + "\n");

            }

        }

    }


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
