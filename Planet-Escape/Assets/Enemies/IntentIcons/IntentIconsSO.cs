using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New IntentIcons")]
public class IntentIconsSO : ScriptableObject
{
    public Sprite AttackIcon => attackIcon;
    [SerializeField] private Sprite attackIcon;

    public Sprite BlockIcon => blockIcon;
    [SerializeField] private Sprite blockIcon;

    public Sprite BuffIcon => buffIcon;
    [SerializeField] private Sprite buffIcon;

    public Sprite DebuffIcon => debuffIcon;
    [SerializeField] private Sprite debuffIcon;

}
