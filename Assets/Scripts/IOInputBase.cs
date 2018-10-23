using UnityEngine;

public abstract class IOInputBase : MonoBehaviour {

    public bool InputEnabled { get; protected set; }

    protected virtual void InputUpdate()
    {

    }

    public virtual void InputEnable()
    {
        InputEnabled = true;
        InputUpdate();
    }

    public virtual void InputDisable()
    {
        InputEnabled = false;
        InputUpdate();
    }

    public virtual void InputToggle()
    {
        if (InputEnabled)
            InputDisable();
        else
            InputEnable();
    }
}
