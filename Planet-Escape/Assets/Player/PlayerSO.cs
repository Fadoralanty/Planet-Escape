using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PlayerCharacter")]
public class PlayerSO : ScriptableObject
{
    public string PlayerCharacterName => playerCharacterName;
    [SerializeField] private string playerCharacterName;
    public int MaxHealth => maxHealth;
    [SerializeField] private int maxHealth;    
    public List<Card_SO> DefaultDeck => defaultDeck;
    [SerializeField] private List<Card_SO> defaultDeck;

}
