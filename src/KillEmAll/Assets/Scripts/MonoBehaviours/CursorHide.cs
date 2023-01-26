using UnityEngine;

namespace MonoBehaviours
{
    public class CursorHide : UnityEngine.MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
