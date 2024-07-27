namespace DarkSurvival.Scripts.Systems.Utils.MessageBus
{
    public class InteractionMessage
    {
        public string InteractableName { get; private set; }

        public InteractionMessage(string interactableName)
        {
            InteractableName = interactableName;
        }
    }
}
