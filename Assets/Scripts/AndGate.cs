using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AndGate : IOGateBase
{
    public IOGateChild Child1;
    public IOGateChild Child2;

    private bool _enabled = false;
    private bool _1Enabled = false;
    private bool _2Enabled = false;

    public override void CheckInput()
    {
        if (_enabled)
        {
            if (!_1Enabled || !_2Enabled)
            {
                _enabled = false;
                OutputDisable();
            }
        }
        else
        {
            if (_1Enabled && _2Enabled)
            {
                _enabled = true;
                OutputEnable();
            }
        }
    }

    public override void EnableInput(IOGateChild child)
    {
        if (child == Child1)
            _1Enabled = true;
        else
            _2Enabled = true;

        base.EnableInput(child);
    }

    public override void DisableInput(IOGateChild child)
    {
        if (child == Child1)
            _1Enabled = false;
        else
            _2Enabled = false;

        base.DisableInput(child);
    }
}
