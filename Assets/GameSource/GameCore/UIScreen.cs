using UnityEngine;

namespace Prototype.Core
{
    public class UIScreen : MonoBehaviour
    {
        public void SetActive()
        {
            gameObject.SetActive(true);
        }

        public void SetDeactive()
        {
            gameObject.SetActive(false);
        }
    }
}