using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    public List<Card_SO> PlayersDeck=new List<Card_SO>();
    public int playerHealth;
    public NodeType CurrentNodeType = NodeType.MinorEnemy;
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
}
