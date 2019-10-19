using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enables the number of rounds that will be played and which map is chosen to be changed based on button pushes. Sets the PlayerPrefs to represent the chosen map and number of rounds.
/// </summary>
public class StartScreenController : MonoBehaviour {

    [SerializeField]
    Text numOfRounds;

    [SerializeField]
    Text mapSelected;

    //0 = saloon, 1 = desert
    int currentMapSelection = 0;

    //Can chose from 1, 3 or 5 rounds
    int[] roundSelection = new int[3] { 1, 3, 5 };
    int currentRoundSelection = 0;

	// Use this for initialization
	void Start () {
        //Sets them to the default, allows the player to instantly press start instead of having to go through the options.
        PlayerPrefs.SetInt("numOfRounds", roundSelection[currentRoundSelection]);
        PlayerPrefs.SetInt("mapSelected", currentMapSelection);
    }

    //Attached to a button, allows the player to cycle through the round selection.
    public void NextNumOfRounds()
    {
        //If it is going to go out of the bounds of the array after the next increase, set it to -1.
        if (currentRoundSelection == 2)
        {
            currentRoundSelection = -1;
        }
        //Increase round selection by 1
        currentRoundSelection++;
        //Changes the text to give the player feedback on what the current number of rounds is at.
        numOfRounds.text = roundSelection[currentRoundSelection].ToString();
        //Sets the PlayerPrefs to the currently selected number of rounds, used by the gameManager to decide how many rounds to allow.
        PlayerPrefs.SetInt("numOfRounds", roundSelection[currentRoundSelection]);
    }

    //Attached to a button, allows the player to cycle back through the round selection.
    public void PreviousNumOfRounds()
    {
        //Decrease current round selection by one.
        currentRoundSelection--;
        //If it goes out of bounds, set it to the highest round number. Allows players to cycle through without having to change which button to press
        if (currentRoundSelection == -1)
        {
            currentRoundSelection = 2;
        }
        //Changes the text to give the player feedback on what the current number of rounds is at.
        numOfRounds.text = roundSelection[currentRoundSelection].ToString();
        //Sets the PlayerPrefs to the currently selected number of rounds.
        PlayerPrefs.SetInt("numOfRounds", roundSelection[currentRoundSelection]);
    }

    //Attached to a button, goes to the next stage in the map selection.
    public void NextStage()
    {
        //Increases the currently selected map number.
        currentMapSelection++;
        //If it goes out of bounds of the array, set it back to the beginning.
        if (currentMapSelection == 2)
        {
            currentMapSelection = 0;
        }
        //Sets the currently selected map into PlayerPrefs, used by the gameManager to decide which map to load once the start button is pressed.
        PlayerPrefs.SetInt("mapSelected", currentMapSelection);
        //Changes the text on screen to display which map is currently selected.
        ChangeStageText();
    }

    //Attached to a button, goes to the previous stage in the map selection.
    public void PreviousStage()
    {
        //Decreases the currently selected map number.
        currentMapSelection--;
        //If it goes out of bounds, move it to the end of the array.
        if (currentMapSelection == -1)
        {
            currentMapSelection = 1;
        }
        //Sets the currently selected map into PlayerPrefs, used by the gameManager to decide which map to load once the start button is pressed.
        PlayerPrefs.SetInt("mapSelected", currentMapSelection);        
        //Changes the text on screen to display which map is currently selected.
        ChangeStageText();
    }

    //Changes the currently selected map text relative to what the currentMapSelection is. Gives player feedback.
    private void ChangeStageText()
    {
        switch (currentMapSelection)
        {
            case 0:
                mapSelected.text = "Desert";
                break;
            case 1:
                mapSelected.text = "Saloon";
                break;
        }
        Debug.Log(PlayerPrefs.GetInt("mapSelected"));
    }
}
