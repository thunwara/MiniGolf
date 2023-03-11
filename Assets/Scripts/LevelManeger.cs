using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManeger : MonoBehaviour
{
    public GolfBall ball;
    public TextMeshProUGUI lablePlayerName;

    private PlayerRecord playerRecord;
    private int playerIndex;
    void Start()
    {
        playerRecord = GameObject.Find("PlayerRecord").GetComponent<PlayerRecord>();
        playerIndex = 0;
        SetupPlayer();
        // Debug.Log(playerIndex);
        Debug.Log("level length = " + playerRecord.level.Length);
    }

    private void SetupPlayer()
    {
        ball.SetUpBall(playerRecord.playerColor[playerIndex]);
        lablePlayerName.text = playerRecord.playerList[playerIndex].name;
        Debug.Log(playerRecord.playerList[playerIndex].name);
    }

    public void nextPlayer(int previousPutt)
    {
        playerRecord.addPutt(playerIndex, previousPutt);
        if (playerIndex < playerRecord.playerList.Count - 1)
        {
            playerIndex++;
            SetupPlayer();
            Debug.Log("from next player");
            // ball.SetUpBall(playerRecord.playerColor[playerIndex]);
        }
        else
        {
            Debug.Log("playerRecord.levelIndex = " + playerRecord.levelIndex);
            Debug.Log("playerRecord.level.Length =" + playerRecord.level.Length);
            if (playerRecord.levelIndex == playerRecord.level.Length - 1)
            {
                //load the scoreboard scene
                // Debug.Log("Scoreboard");
                SceneManager.LoadScene("ScoreBoard");
            }
            else
            {
                playerRecord.levelIndex++;
                SceneManager.LoadScene(playerRecord.level[playerRecord.levelIndex]);
            }
        }

    }

}
