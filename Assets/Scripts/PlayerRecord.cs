using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerRecord : MonoBehaviour
{
    public List<player> playerList;
    public string[] level;
    public Color[] playerColor;
    [HideInInspector] public int levelIndex = 0;

    void OnEnable()
    {
        playerList = new List<player>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void addPlayer(string name)
    {
        playerList.Add(new player(name, playerColor[playerList.Count], level.Length));

    }

    public void addPutt(int playerIndex, int puttCount)
    {
        playerList[playerIndex].putts[levelIndex] = puttCount;
        // Debug.Log("levelIndex =" + levelIndex);
    }

    public List<player> GetScoreboardList()
    {
        foreach (var player in playerList)
        {
            foreach (var puttScore in player.putts)
            {
                player.totalPutt += puttScore;
            }
        }
        return (from p in playerList orderby p.totalPutt select p).ToList();
    }

    public class player
    {
        public string name;
        public Color color;
        public int[] putts;
        public int totalPutt;
        public player(string newName, Color newColor, int levelCount)
        {
            name = newName;
            color = newColor;
            putts = new int[levelCount];
        }
    }
}
