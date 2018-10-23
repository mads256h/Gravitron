using JetBrains.Annotations;
using UnityEngine;

namespace IO
{
    public class IOGateSimpleBase : IOGateBase
    {

        [SerializeField] [UsedImplicitly(ImplicitUseKindFlags.Assign)] protected IOGateChild GateChild1;
        [SerializeField] [UsedImplicitly(ImplicitUseKindFlags.Assign)] protected IOGateChild GateChild2;

        [SerializeField] [UsedImplicitly(ImplicitUseKindFlags.Assign)] protected bool Child1Enabled;
        [SerializeField] [UsedImplicitly(ImplicitUseKindFlags.Assign)] protected bool Child2Enabled;

        public override void ChildInputEnable(IOGateChild child)
        {
            if (child == GateChild1)
                Child1Enabled = true;
            else
                Child2Enabled = true;


            base.ChildInputEnable(child);
        }

        public override void ChildInputDisable(IOGateChild child)
        {
            if (child == GateChild1)
                Child1Enabled = false;
            else
                Child2Enabled = false;

            base.ChildInputDisable(child);
        }
    }
}
