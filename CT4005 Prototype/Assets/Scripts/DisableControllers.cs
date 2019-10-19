using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Uses the MenuManager and GameManager to remove any joined controllers. Attached to a button in the character join menu and the play button.
/// </summary>
public class DisableControllers : MonoBehaviour {

    [SerializeField] GameManager gm;
    [SerializeField] MenuManager mm;

    // Runs on button press.
    public void DisableActiveControllers()
    {
        gm.RemoveAllControllers();
        mm.ResetPlayerJoins();
    }
}
