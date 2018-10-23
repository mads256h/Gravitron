using JetBrains.Annotations;

namespace IO
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
    public sealed class IOGateChild : IOInputBase
    {
        [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public IOGateBase Gate;

        public override void InputEnable()
        {
            base.InputEnable();
            Gate?.ChildInputEnable(this);
        }

        public override void InputDisable()
        {
            base.InputDisable();
            Gate?.ChildInputDisable(this);
        }
    }
}
