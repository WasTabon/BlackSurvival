using DarkSurvival.Scripts.Interfaces;
using DarkSurvival.Scripts.Systems.Utils.MessageBus;
using UnityEngine;

namespace DarkSurvival.Scripts.Gameplay.Crafting.General
{
    public class Workbench : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            MessageBus.Publish(new InteractionMessage("WorkbenchPanel"));
        }
    }
}
