﻿using JetBrains.Annotations;

namespace IO
{
    public abstract class IOBase : IOInputBase
    {
        [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public IOInputBase[] OutputComponents;

        public bool OutputEnabled { get; protected set; }

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