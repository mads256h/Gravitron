using JetBrains.Annotations;

namespace IO
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
    public sealed class IOGateNot : IOBase {
        protected override void InputUpdate()
        {
            base.InputUpdate();

            if (InputEnabled)
                OutputDisable();
            else
                OutputEnable();
        }
    }
}
