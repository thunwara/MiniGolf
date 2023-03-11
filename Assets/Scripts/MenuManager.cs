using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField inputPlayerName;
    public PlayerRecord playerRecord;
    public Button buttonStart;
    public Button buttonAddPlayer;

    public void bottonAddPlayer()
    {
        playerRecord.addPlayer(inputPlayerName.text);
        buttonStart.interactable = true;
        inputPlayerName.text = "";
        if (playerRecord.playerList.Count == playerRecord.playerColor.Length)
        {
            buttonAddPlayer.interactable = false;
        }
    }

    public void Start()
    {
        // Debug.Log("level length 1 = " + playerRecord.level[0]);
        // Debug.Log("level length 2 = " + playerRecord.level[1]);
        // Debug.Log("level length 3 = " + playerRecord.level[2]);
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene(playerRecord.level[0]);
        // SceneManager.LoadScene("Level 2");

    }
}