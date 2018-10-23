using JetBrains.Annotations;

namespace IO
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
    public sealed class IOGateXor : IOGateSimpleBase
    {
        protected override void CheckInput()
        {
            if (OutputEnabled)
            {
                if (Child1Enabled == Child2Enabled)
                {
                    OutputDisable();
                }
            }
            else
            {
                if (Child1Enabled != Child2Enabled)
                {
                    OutputEnable();
                }
            }
        }
    }
}
