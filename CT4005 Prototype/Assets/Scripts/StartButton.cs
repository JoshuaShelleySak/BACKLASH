using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Resets all of the PlayerPrefs for kills and wins so that none are kept from previous sessions. Loads the map based on the map selected in the menu.
/// </summary>
public class StartButton : MonoBehaviour {

    public void StartGame()
    {
        PlayerPrefs.SetInt("P1Kills", 0);
        PlayerPrefs.SetInt("P2Kills", 0);
        PlayerPrefs.SetInt("P3Kills", 0);
        PlayerPrefs.SetInt("P4Kills", 0);

        PlayerPrefs.SetInt("P1Wins", 0);
        PlayerPrefs.SetInt("P2Wins", 0);
        PlayerPrefs.SetInt("P3Wins", 0);
        PlayerPrefs.SetInt("P4Wins", 0);

        switch (PlayerPrefs.GetInt("mapSelected"))
        {
            case 0:
                SceneManager.LoadScene("Scene 1", LoadSceneMode.Single);
                break;
            case 1:
                SceneManager.LoadScene("Scene 2", LoadSceneMode.Single);
                break;
        }
    }


}
