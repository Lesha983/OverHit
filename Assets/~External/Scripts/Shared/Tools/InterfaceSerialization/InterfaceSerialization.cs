namespace Chillplay.Tools
{
    using UnityEngine;

    [System.Serializable]
    public abstract class InterfaceField
    {
        [SerializeField]
        protected Object value = null;
    }

    [System.Serializable]
    public class Interface<IAny> : InterfaceField where IAny : class
    {
        public IAny Item
        {
            get => value as IAny;
            set => this.value = value as Object;
        }

        public static implicit operator IAny(Interface<IAny> i) => i.Item;

        public void TrySetFromObject(Object obj)
        {
            if (obj == null) Item = null;
            else if (obj is IAny) Item = obj as IAny;
            else if (obj is GameObject) SetFromGameObject(obj as GameObject);
        }

        public bool CanBeSetFromObject(Object obj)
        {
            return obj is IAny || obj is GameObject;
        }

        public void SetFromGameObject(GameObject gameObject)
        {
            Item = gameObject.GetComponent<IAny>();
        }
    }
}