using JetBrains.Annotations;

public abstract class IOBase : IOInputBase
{
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public IOInputBase[] OutputComponents;

    public bool OutputEnabled { get; protected set; }

    protected virtual void OutputUpdate()
    {

    }

    public virtual void OutputEnable()
    {
        foreach (var ioBase in OutputComponents)
        {
            ioBase.InputEnable();
        }

        OutputEnabled = true;
        OutputUpdate();
    }

    public virtual void OutputDisable()
    {
        foreach (var ioBase in OutputComponents)
        {
            ioBase.InputDisable();
        }

        OutputEnabled = false;
        OutputUpdate();
    }

    public virtual void OutputToggle()
    {
        if (OutputEnabled)
            OutputDisable();
        else
            OutputEnable();
    }

}
