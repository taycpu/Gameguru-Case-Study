using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusUI : MonoBehaviour
{
    [SerializeField] private Text text;

    public void SetBonusText(int bonus)
    {
        text.text = "Bonus =" + bonus;
    }
}