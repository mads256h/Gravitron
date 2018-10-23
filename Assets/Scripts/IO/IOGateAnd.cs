using JetBrains.Annotations;

namespace IO
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
    public sealed class IOGateAnd : IOGateBase
    {
        [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public IOGateChild Child1;
        [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public IOGateChild Child2;

        private bool _1Enabled = false;
        private bool _2Enabled = false;

        protected override void CheckInput()
        {
            if (OutputEnabled)
            {
                if (!_1Enabled || !_2Enabled)
                {
                    OutputDisable();
                }
            }
            else
            {
                if (_1Enabled && _2Enabled)
                {
                    OutputEnable();
                }
            }
        }

        public override void ChildInputEnable(IOGateChild child)
        {
            if (child == Child1)
                _1Enabled = true;
            else
                _2Enabled = true;

            base.ChildInputEnable(child);
        }

        public override void ChildInputDisable(IOGateChild child)
        {
            if (child == Child1)
                _1Enabled = false;
            else
                _2Enabled = false;

            base.ChildInputDisable(child);
        }
    }
}
