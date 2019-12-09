using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Timers;


//The game manager script will control all behaviours of the game
//This includes making the pattern and verifying the user inputed the corret pattern
public class GameManager : MonoBehaviour
{

    //The following section is for booleans to keep the game logic in check
    private bool shouldLit;
    private bool shouldDark;
    private bool activeGame;
    private bool time;
    
    //The following section of variables are linked to needed parts to make the game work
    public double stayLitHowLong; //Tells the game how long to stay lit up for
    private double stayLitCounter; //Counter for how long the variable has been lit for
    private double stayDarkCounter; //Counter for how long the color should be dark for
    public SpriteRenderer[] colors; //Represents all the sprites that will light up for the user to click
    private List<int> sequence; //The sequence of colors to verify thta the user selected the right color
    //A list is used here since we can dynamically allocate memory for it so that it wont run out of space
    private int sequenceLength; //The amount of colors that needed to light up on every time
    private int listLocation; //Know where in the list we are
    private int playerLocation; //Know where in the list the user is

    //The following section of variables are linked to data management and UI elements
    UIManager uIManager;
    private DataManager dataManager; //Will control data
    private double timer;  //Timer to keep track of how long it takes for the user to click a button
    private List<double> timers; //list to store all the the times for clicks
    private List<int> clicked; //Keep track of which option the player clicked
    public Camera cam;

    // Start is called before the first frame update
    void Awake()
    {

        //Setting the ui and data managers
        dataManager = FindObjectOfType<DataManager>();
        uIManager = FindObjectOfType<UIManager>();

        uIManager.ChangeText("Welcome " + dataManager.GetUser().GetUsername());
        sequence = new List<int>();
        timers = new List<double>();
        clicked = new List<int>();

        HideOrShowBlocks(false);
        uIManager.SetDataManager(dataManager);

    }

    // Update is called once per frame
    //Since update is updated once per frame we can put all the logic for making the game know when it should be lit in this method
    void Update()
    {

        //Checking to see if the color should be lit up or not
        if (shouldLit)
        {

            //Subtracting time so the button knows how long to be lit up for
            stayLitCounter -= Time.deltaTime;

            if(stayLitCounter <= 0)
            {

                //Making the color go back to its dim state
                colors[sequence[listLocation]].color = new Color(colors[sequence[listLocation]].color.r, colors[sequence[listLocation]].color.g, colors[sequence[listLocation]].color.b, .5f);

                //Making the game know it should no longer be lit up
                shouldLit = false;

                //Making the game know how long to wait between lighting up the next color
                stayDarkCounter = stayLitHowLong;

                //Making the game know that the sequence should be dark
                shouldDark = true;

                //Move the list location up by one to go to next object
                listLocation++;
            }

        }


        //Checking to see if the game should be in its dark state
        if (shouldDark)
        {

            //Count down the time so the game knows how long it has been dark for
            stayDarkCounter -= Time.deltaTime;

            //Check to see if the game has reached the end of the sequence
            if (listLocation >= sequenceLength)
            {

                //Make the game know it should no longer be dark and that the game is now active for the user to use
                shouldDark = false;
                activeGame = true;

                //Choosing if the sequence should be entered in forward or reverse
                int random = Random.Range(0, 2);

                if (random == 0)
                    uIManager.HideOrShow(true);

                else
                {
                    uIManager.HideOrShow(false);
                    sequence.Reverse();
                }

                //Starting the first timer
                timer = 0;
                time = true;

            }

            //Check to see if the game has been dark for long enought
            if(stayDarkCounter <= 0)
            {

                //Light up the next color in the sequence
                colors[sequence[listLocation]].color = new Color(colors[sequence[listLocation]].color.r, colors[sequence[listLocation]].color.g, colors[sequence[listLocation]].color.b, 1f);

                //Setting the counter and bool so that the game knows how long to stay lit up for
                stayLitCounter = stayLitHowLong;
                shouldLit = true;

                //Set should dark to false so that the game knows nothing should be lit up
                shouldDark = false;

            }

        }


        //Checking if the timer should be active to recored user time between clicks
        if (time)
        {
            timer += Time.deltaTime;
        }

    }


    //Method to start the game when the user selects that option
    public void StartGame()
    {

        //Randomizin and showing the blocks
        RandomizeBlocks();
        HideOrShowBlocks(true);

        //Set the players score to 0 and resent the sequence in case anything is still in it and set the sequence length to one so the first color is chosen
        dataManager.GetUser().SetScore(0);
        sequence.Clear();
        timers.Clear();
        clicked.Clear();
        sequenceLength = 2;

        //Set the path for the rounds data to be recorded
        dataManager.SetPath(dataManager.GetUser().GetUsername() + "/" + System.DateTime.Now.ToString("dddd, dd MMMM yyyy HH mm ss") + ".txt");


        //Setting the list location and player location to 0 so that it will always start from the beggining
        listLocation = 0;
        playerLocation = 0;

        //Hide the buttons
        uIManager.HideButtons();
        uIManager.ChangeText("Score: " + dataManager.GetUser().GetScore());

        //Choose the first color in the sequence
        ChooseColor();

    }


    private void ChooseColor()
    {

        //CLearing the sequence so a new sequence can be created
        sequence.Clear();

        //While loop so that the sequence is created starting at position 0 every time
        while (sequence.Count < sequenceLength)
        {
            //Selecting which of the colors will be used
            int randomColor = Random.Range(0, colors.Length);

            //Adding which color was choosen to the list
            sequence.Add(randomColor);

            //Changing the color of the block to be a lighter version of it as if it was clicked
            colors[sequence[listLocation]].color = new Color(colors[sequence[listLocation]].color.r, colors[sequence[listLocation]].color.g, colors[sequence[listLocation]].color.b, 1f);

            //Setting the counter to be the amount of time the timer should be lit for
            stayLitCounter = stayLitHowLong;

            //Making it so the game knows that the button should be lit
            shouldLit = true;
        }

    }


    //Method to get which of the colors was pressed to verify it against the sequence
    public void WhichPressed(int pressed)
    {

        //Check to make sure the game is in fact active so the user can actually click things
        if (activeGame)
        {

            clicked.Add(pressed);

            //Stopping the timer and adding it to the list
            timers.Add(timer);
            timer = 0;

            //Check to see if the button the user pressed is the same as what it shouldve been
            if (pressed == sequence[playerLocation])
            {
                
                //Add one to the player location to move to the next sequence
                playerLocation++;

                //Check to see if the player has made it to the end of the list
                if(playerLocation >= sequence.Count)
                {

                    //Add once to the player score and increase the sequence length by one and display new score
                    dataManager.GetUser().IncreaseScore();
                    sequenceLength++;
                    uIManager.ChangeText("Score: " + dataManager.GetUser().GetScore());

                    //Record data and clear all sequences
                    dataManager.WriteData(sequence, timers, clicked);
                    timers.Clear();
                    clicked.Clear();

                    //Setting the list location and player position back at 0 so that is starts from the beggining
                    playerLocation = 0;
                    listLocation = 0;

                    //Randomizing where the blocks are
                    RandomizeBlocks();

                    //Choose the new sequence and setting the game to not be active
                    ChooseColor();
                    activeGame = false;
                }

            }

            //Else statement for if the player lost the game
            else
            {
                activeGame = false;

                //Writing to the global and local score boards
                dataManager.InputHighScore("highscore.txt");
                dataManager.InputHighScore(dataManager.GetUser().GetUsername() + "/" + dataManager.GetUser().GetUsername() + "sHighscores.txt");
                dataManager.WriteData(sequence, timers, clicked);
                
                SceneManager.LoadScene(2);
            }

        }

    }


    //Method to hide each block so that there positions can be randomized
    public void HideOrShowBlocks(bool which)
    {
        foreach (SpriteRenderer sprite in colors)
            sprite.gameObject.SetActive(which);
    }


    //Method to randomize the position of the blocks on the screen
    public void RandomizeBlocks()
    {

        //Temp list of colors this will be used to verify that no position overlaps
        List<SpriteRenderer> temp = new List<SpriteRenderer>();

        //Getting the height and width of the screen so that the block can only exist in a given range
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        //Making the object go to a random position
        foreach (SpriteRenderer color in colors)
        {

            //Goto in case the blocks do overlap
            TryAgain:

            //Choosing the random height and width
            float positionX = Random.Range(-width, width);
            float positionY = Random.Range(-height, height);

            //Move through the temp loop for verification that none overlap
            foreach (SpriteRenderer tempColor in temp)
            {

                //Transforming the color
                color.transform.position = new Vector3(positionX, positionY, color.transform.position.z);

                //Check to see if the transformed position overlaps with any of the other colors
                if(AreColliding(color.gameObject, tempColor.gameObject))
                {
                    goto TryAgain;
                }

                //We also need to check that the block is not outside of the screen
                if(Mathf.Abs(color.transform.position.x) + getSpriteRect(color.gameObject).width >= Mathf.Abs(width) || 
                    Mathf.Abs(color.transform.position.y) + getSpriteRect(color.gameObject).height  >= Mathf.Abs(height))
                {
                    goto TryAgain;
                }
            }

            //Adding the transformed color to the temp list
            temp.Add(color);

        }
    }


    //Method to get the rectangle of a game object to check if anything overlaps
    public Rect getSpriteRect(GameObject gameObject)
    {

        //Get the vector position of the object 
        Vector3 pos = gameObject.transform.position;

        //Get the sprite renderer used just to know the bounds of the sprite
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        Rect tmpr = new Rect(pos, sr.bounds.size);

        tmpr.center = tmpr.min;

        return tmpr;

    }


    //Method to check if two objects are colliding
    public bool AreColliding(GameObject gameObject1, GameObject gameObject2)
    {

        //Getting the two rectangles to compare
        Rect rect1 = getSpriteRect(gameObject1);
        Rect rect2 = getSpriteRect(gameObject2);

        //Check if the two overlap. If they do return true else return false
        if (rect1.Overlaps(rect2))
            return true;
        
        return false;

    }

}
