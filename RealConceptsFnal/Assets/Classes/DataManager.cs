using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Threading;

//Class to act as a way to pass data between scenes without being destroyed after a scene is closed
//Will have a user
public class DataManager : MonoBehaviour
{

    private User user;
    private string currentPath;
    private Encrypter encrypter;

    //The constructor for the data manager will only intilizle the encryptor
    public DataManager()
    {
        encrypter = new Encrypter();
    }

    //*******************************************************
    //THE FOLLOWING SECTION IS GETTERS AND SETTERS
    //*******************************************************

    public void SetUser(User user)
    {
        this.user = user;
    }

    public User GetUser()
    {
        return user;
    }

    public void SetPath(string path)
    {
        currentPath = path;
    }

    public string GetPath()
    {
        return currentPath;
    }

    public Encrypter GetEncrypter()
    {
        return encrypter;
    }
    //*******************************************************
    //END GETTERS AND SETTERS
    //*******************************************************


    //Method to add the highscore to a file
    //Will take in a string path which will either be the players personal high scores or the global high scores
    public void InputHighScore(string path)
    {

        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine(encrypter.EncryptDecrypt(user.GetUsername() + "," + user.GetScore()));
        }

        //Read all lines from the file
        string[] lines = File.ReadAllLines(path);

        string t;

        //If to make sure the bubble sort algorithem will work
        if (lines.Length > 2)
        {
            for (int p = 0; p <= lines.Length - 2; p++)
            {
                for (int i = 0; i <= lines.Length - 2; i++)
                {
                    if (GetThePayerScore(lines[i + 1]) > GetThePayerScore(lines[i]))
                    {
                        t = lines[i];
                        lines[i] = lines[i + 1];
                        lines[i + 1] = t;
                    }
                }
            }
        }

        //If there is only two check the order
        else if (lines.Length == 2)
        {

            //Check to see which one is bigger
            if (GetThePayerScore(lines[1]) > GetThePayerScore(lines[0]))
            {
                t = lines[0];
                lines[0] = lines[1];
                lines[1] = t;
            }

        }

        //Write the information to the file again
        using (StreamWriter sw = new StreamWriter(path))
        {
            foreach (string line in lines)
            {
                sw.WriteLine(line);
            }
        }


        //Inside method to get the score associated with the line
        int GetThePayerScore(string line)
        {
            line = encrypter.EncryptDecrypt(line);
            string[] word = line.Split(',');
            return Convert.ToInt32(word[1]);

        }

    }


    //Method to write recorded data to a file
    //Takes in thtree lists to represent the sequence and the time it took for the user to click each part of the sequence
    public void WriteData(List<int> sequence, List<double> timers, List<int> clicked)
    {

        //Write the heading for the section
        using (StreamWriter sw = File.AppendText(currentPath))
        {
            sw.WriteLine(encrypter.EncryptDecrypt("BEGIN DATA FOR ROUND " + sequence.Count));
            sw.WriteLine(encrypter.EncryptDecrypt("Block\tClicked\tTime"));
            sw.WriteLine(encrypter.EncryptDecrypt("---------------------------------------------"));
     
        }

        //For loop to move through the file to get the data
        for (int i = 0; i < sequence.Count; i++)
        {
            
            //Use the stream to write all the data to the time
            using (StreamWriter sw = File.AppendText(currentPath))
            {

                //Check if the player actually choose correctly
                if (clicked.Count > i)
                {
                    sw.WriteLine(encrypter.EncryptDecrypt(sequence[i] + "\t" + clicked[i] + "\t" + timers[i]));
                }

                else
                {
                    sw.WriteLine(encrypter.EncryptDecrypt(sequence[i] + "\tN/A\tN/A"));
                }

            }

        }

        using (StreamWriter sw = File.AppendText(currentPath))
        {
            sw.WriteLine("");
        }
    }
}
