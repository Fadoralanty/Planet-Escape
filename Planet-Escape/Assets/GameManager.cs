using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    public List<Card_SO> PlayersDeck=new List<Card_SO>();
    public int playerHealth;
    public NodeType CurrentNodeType = NodeType.MinorEnemy;
    public bool IsNewGame;
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void NewGame()
    {
        IsNewGame = true;
        SceneManager.LoadScene("Map");
    }

    public void GoBackToMapScreen()
    {
        SceneManager.LoadScene("Map");
    }
}
