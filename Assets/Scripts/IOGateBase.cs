public abstract class IOGateBase : IOBase {

    public virtual void CheckInput()
    {

    }

    public virtual void EnableInput(IOGateChild child)
    {
        CheckInput();
    }

    public virtual void DisableInput(IOGateChild child)
    {
        CheckInput();
    }
}
