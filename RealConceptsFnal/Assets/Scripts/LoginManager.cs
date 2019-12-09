using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


//Script to work in order to login and create the user class and set them into the data manager so they can be passed across scenes
public class LoginManager : MonoBehaviour
{

    private DataManager dataManager;
    private User user;
    public InputField inputFieldUsername, inputFieldPassword; //Username and passwords enetered
    public Button buttonLogin, buttonCreate; //Buttons on UI
    public Text informUser; //To inform the user about information

    // Start is called before the first frame update
    void Start()
    {
        //Setting the data manager to what it should be
        dataManager = FindObjectOfType<DataManager>();
        
        //Calling verifydoc to ensure accounts file exists
        VerifyDoc();
    }


    void Update()
    {
        
    }


    //Method to make sure that the accounts.txt file exists
    private void VerifyDoc()
    {
        //Setting the path variable
        string path = "accounts.txt";

        if (!File.Exists(path))
        {
            File.CreateText(path);
        }

        //Making the high score file
        path = "highscore.txt";

        if (!File.Exists(path))
        {
            File.Create(path);
        }
    }

    //Method for the user to login
    private void Login()
    {

        //Bool to check if the information is good
        bool correct = false;

        //Get the path to the accounts.txt
        string path = "accounts.txt";

        //Read the file check if the username and password are in the same line
        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {

            //Deencrypting the line to verify against the entered information
            string tempLine = dataManager.GetEncrypter().EncryptDecrypt(line);

            //Spliting the string so that it is in two different parts
            string[] words = tempLine.Split(' ');

            //Checking if the username and password is entered on one of the lines
            if (words[0].Equals(inputFieldUsername.text) && words[1].Equals(inputFieldPassword.text))
            {

                //Make an instance of the user so that the proper user has scores
                dataManager.SetUser(new User(inputFieldUsername.text, inputFieldPassword.text));
                correct = true;

            }
        }
        
        //If it moves through all the lines in the file it will just inform user that information is incorrect
        if (!correct)
        {
            informUser.text = "Incorrect information entered";
        }

        else
        {
            LoadGame();
        }
    }//End Login


    //Method for the user to create an account
    private void CreateAccount()
    {

        if (VerifyField())
        {
            //Bool so that the user knows if the account exists or not
            bool avaliable = true;

            //Making the path to the accounts folder
            string path = "accounts.txt";

            //Check to make sure that the username entered was not already taken
            foreach (string line in File.ReadAllLines(path))
            {

                //Deencrypting the line to verify against the entered information
                string tempLine = dataManager.GetEncrypter().EncryptDecrypt(line);

                //Spliting the string so that it is in two different parts
                string[] words = tempLine.Split(' ');

                if (words[0].Equals(inputFieldUsername.text))
                {
                    avaliable = false;
                    informUser.text = "Username already taken";
                }
            }

            //Check if the username is avalailble
            if (avaliable)
            {

                //Making the user and storing them into the datamanager
                dataManager.SetUser(new User(inputFieldUsername.text, inputFieldPassword.text));

                //Create the account and auto sign in the user and add information to text file
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(dataManager.GetEncrypter().EncryptDecrypt(inputFieldUsername.text + " " + inputFieldPassword.text));
                    sw.Close();
                }

                MakeDirectory();

                LoadGame();
            }
        }

        else
        {
            informUser.text = "Please enter information";
        }

    }//end CreateAccount


    //Method to create a directory for a user so all there data can be stored in
    private void MakeDirectory()
    {

        Directory.CreateDirectory(inputFieldUsername.text);

        //Adding the high score file to the users directory
        string path = inputFieldUsername.text + "/" + inputFieldUsername.text + "sHighscores.txt";

        File.Create(path);
    }



    //Check to make sure both input fields have information in them
    private bool VerifyField()
    {
        if (inputFieldUsername.text.Equals("") && inputFieldPassword.text.Equals(""))
            return false;

        return true;
    }//End VerifyField



    //Method to load the main game scene
    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

}
