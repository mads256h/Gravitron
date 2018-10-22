public class IOGateChild : IOBase
{

    public IOGateBase Gate;

    public override void Enable()
    {
        base.Enable();
        Gate.EnableInput(this);
    }

    public override void Disable()
    {
        base.Disable();
        Gate.DisableInput(this);
    }
}
