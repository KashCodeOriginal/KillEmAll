using UnityEngine;

namespace MonoBehavior
{
    public class CursorHide : MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
