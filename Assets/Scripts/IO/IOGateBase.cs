namespace IO
{
    public abstract class IOGateBase : IOBase {

        protected virtual void CheckInput()
        {

        }

        public virtual void ChildInputEnable(IOGateChild child)
        {
            CheckInput();
        }

        public virtual void ChildInputDisable(IOGateChild child)
        {
            CheckInput();
        }
    }
}
