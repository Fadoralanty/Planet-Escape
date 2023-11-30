using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSite : MonoBehaviour
{
    public int PercentageToHeal=20;
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    public void Rest()
    {
        GameManager.Singleton.HealPlayerPercentage(PercentageToHeal);
        HideButtons();
    }   
    public void RemoveCard()
    {
        // open remove card screen
    }   
    public void Continue()
    {
        GameManager.Singleton.GoBackToMapScreen();
        HideButtons();
    }

    private void HideButtons()
    {
        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
    }
}
