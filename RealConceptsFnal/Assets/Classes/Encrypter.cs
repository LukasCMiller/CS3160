using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


//The encrypter class will handle the encryption of all output files
public class Encrypter : MonoBehaviour
{

    private static int key; //Used to shift the ascii value in the files

    //Constructor for the encrypter
    public Encrypter()
    {
        key = 129;
    }


    //Method to do the encryption
    //It will take in a path in order to write the info to the file
    public string EncryptDecrypt(string textToEncrypt)
    {

        //String builders to make the output string one char at a time
        StringBuilder inSb = new StringBuilder(textToEncrypt);
        StringBuilder outSb = new StringBuilder(textToEncrypt.Length);

        //Temp char to move through the string one letter at a time
        char temp;

        //For loop to move through each of the chars
        for (int i = 0; i < textToEncrypt.Length; i++)
        {

            temp = inSb[i];
            temp = (char)(temp ^ key);
            outSb.Append(temp);

        }

        return outSb.ToString();

    }

}
