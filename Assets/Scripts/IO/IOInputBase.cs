using JetBrains.Annotations;
using UnityEngine;

namespace IO
{
    public abstract class IOInputBase : MonoBehaviour
    {
        [SerializeField] [UsedImplicitly(ImplicitUseKindFlags.Assign)] protected bool InputEnabled;

        protected virtual void InputUpdate()
        {

        }

        public virtual void InputEnable()
        {
            if (InputEnabled) return;
            InputEnabled = true;
            InputUpdate();

        }

        public virtual void InputDisable()
        {
            if (!InputEnabled) return;
            InputEnabled = false;
            InputUpdate();
        }

        public virtual void InputToggle()
        {
            if (InputEnabled)
                InputDisable();
            else
                InputEnable();
        }
    }
}
