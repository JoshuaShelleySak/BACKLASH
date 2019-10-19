using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Attached to a button on the leaderboard screen. Resets kills and wins before sending player back to the main menu.
/// </summary>
public class FinishButton : MonoBehaviour {

    public void FinishGame()
    {
        PlayerPrefs.SetInt("P1Kills", 0);
        PlayerPrefs.SetInt("P2Kills", 0);
        PlayerPrefs.SetInt("P3Kills", 0);
        PlayerPrefs.SetInt("P4Kills", 0);

        PlayerPrefs.SetInt("P1Wins", 0);
        PlayerPrefs.SetInt("P2Wins", 0);
        PlayerPrefs.SetInt("P3Wins", 0);
        PlayerPrefs.SetInt("P4Wins", 0);

        SceneManager.LoadScene("TitleScreenScene");
    }
}
