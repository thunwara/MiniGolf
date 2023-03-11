using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ScoreboardManager : MonoBehaviour
{
    public string[] levels = { "Level 1", "Level 2", "Level 3" };
    public int levelindex;
    public int levelindex2;
    public int levelindex3;
    public TextMeshProUGUI PlayerNameText, PlayerPuttText;

    private PlayerRecord playerRecord;

    // Start is called before the first frame update
    void Start()
    {
        playerRecord = GameObject.Find("PlayerRecord").GetComponent<PlayerRecord>();
        PlayerNameText.text = "";
        PlayerPuttText.text = "";
        foreach (var player in playerRecord.GetScoreboardList())
        {
            PlayerNameText.text += player.name + "\n";
            PlayerPuttText.text += player.totalPutt + "\n";
        }
        Debug.Log("Start LevelIndex = " + levelindex);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerNameText.fontSize = PlayerPuttText.fontSize;
    }

    public void ButtonReturnMenu()
    {
        Debug.Log("levelIndex =" + levelindex);
        // Destroy(playerRecord.gameObject);
        if (levelindex == 1)
        {
            levelindex++;
            // levelindex2++;

            SceneManager.LoadScene("Level 2");
        }
        else if (levelindex == 2)
        {
            levelindex++;
            // levelindex3++;

            SceneManager.LoadScene("Level 3");
        }
        else if (levelindex == 3)
        {
            SceneManager.LoadScene("Menu");
        }
        Debug.Log("levelIndex After =" + levelindex);


    }

}
