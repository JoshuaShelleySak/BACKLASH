using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quits the game when the button that this script is attached to is pressed.
/// </summary>
public class ExitButton : MonoBehaviour {

    public void ExitGame()
    {
        Application.Quit();
    }
}
