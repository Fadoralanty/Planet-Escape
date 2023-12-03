using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New BuffIcons")]
public class BuffsIconsSO : ScriptableObject
{

    public Sprite PoisonIcon => poisonIcon;
    [SerializeField] private Sprite poisonIcon;
    public Sprite RegenIcon => regenIcon;
    [SerializeField] private Sprite regenIcon;
    public Sprite SlowIcon => slowIcon;
    [SerializeField] private Sprite slowIcon;    
    public Sprite FastIcon => fastIcon;
    [SerializeField] private Sprite fastIcon;
    public Sprite SpikesIcon => spikes;
    [SerializeField] private Sprite spikes;
    public Sprite BurnIcon => burn;
    [SerializeField] private Sprite burn;
    public Sprite IceIcon => ice;
    [SerializeField] private Sprite ice;
    public Sprite StunIcon => stun;
    [SerializeField] private Sprite stun;
    

}
