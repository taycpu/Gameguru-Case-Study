using UnityEngine;

namespace Prototype.Core
{
    public class Tick : MonoBehaviour
    {
        public void SetTick()
        {
            gameObject.SetActive(true);
        }

        public void UnTick()
        {
            gameObject.SetActive(false);
        }
    }
}