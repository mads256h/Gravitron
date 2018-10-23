using JetBrains.Annotations;
using UnityEngine;

namespace IO
{
    public abstract class IOBase : IOInputBase
    {
        [SerializeField] [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        protected IOInputBase[] OutputComponents;

        [SerializeField] [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        protected bool OutputEnabled;

        protected virtual void OutputUpdate()
        {

        }

        protected virtual void OutputEnable()
        {
            foreach (var ioBase in OutputComponents)
            {
                ioBase.InputEnable();
            }

            OutputEnabled = true;
            OutputUpdate();
        }

        protected virtual void OutputDisable()
        {
            foreach (var ioBase in OutputComponents)
            {
                ioBase.InputDisable();
            }

            OutputEnabled = false;
            OutputUpdate();
        }

        protected virtual void OutputToggle()
        {
            if (OutputEnabled)
                OutputDisable();
            else
                OutputEnable();
        }

    }
}
