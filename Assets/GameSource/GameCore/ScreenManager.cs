using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Core
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private List<UIScreen> screens;

        public void Start()
        {
            ChangeScreen(0);
        }

        public void ChangeScreen(int index)
        {
            for (int i = 0; i < screens.Count; i++)
            {
                if (i == index)
                {
                    screens[i].SetActive();
                }
                else
                {
                    screens[i].SetDeactive();
                }
            }
        }
    }
}