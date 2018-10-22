using UnityEngine;

public abstract class IOBase : MonoBehaviour
{
    public IOBase[] OutputComponents;

    public virtual void OutputEnable()
    {
        foreach (var ioBase in OutputComponents)
        {
            ioBase.Enable();
        }
    }

    public virtual void OutputDisable()
    {
        foreach (var ioBase in OutputComponents)
        {
            ioBase.Disable();
        }
    }

    public virtual void OutputToggle()
    {
        foreach (var ioBase in OutputComponents)
        {
            ioBase.Toggle();
        }
    }

    public bool Enabled { get; protected set; }

    public virtual void InputUpdate()
    {

    }

    public virtual void Enable()
    {
        Enabled = true;
        InputUpdate();
    }

    public virtual void Disable()
    {
        Enabled = false;
        InputUpdate();
    }

    public virtual void Toggle()
    {
        if (Enabled) Disable();
        else Enable();
    }
}
