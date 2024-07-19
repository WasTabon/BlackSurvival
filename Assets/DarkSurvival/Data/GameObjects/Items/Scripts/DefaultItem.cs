using DarkSurvival.Data.ScriptableObjects;
using DarkSurvival.Scripts.Interfaces;
using UnityEngine;

namespace DarkSurvival.Data.GameObjects.Items.Scripts
{
    public class DefaultItem : MonoBehaviour, ICollectable
    {
        [SerializeField] protected ItemData _itemData;

        public ItemData Collect()
        {
            Destroy(gameObject);
            return _itemData;
        }
    }
}
