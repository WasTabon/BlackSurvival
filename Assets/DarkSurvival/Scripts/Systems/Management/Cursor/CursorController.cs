using UnityEngine;

namespace DarkSurvival.Scripts.Systems.Management.Cursor
{
    public class CursorController
    {
        private bool _canSeeCursor;

        public bool CanSeeCursor => _canSeeCursor;

        public void LockCursor()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            _canSeeCursor = false;
        }

        public void UnlockCursor()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            _canSeeCursor = true;
        }
    }
}
