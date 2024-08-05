using System.Collections.Generic;
using UnityEngine;

namespace DarkSurvival.Scripts.Interfaces
{
    public interface IUIPanel
    {
        void ManagePanel(bool state, Stack<RectTransform> stack);
    }
}
