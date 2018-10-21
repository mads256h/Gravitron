public interface IOInterface
{
    bool Enabled { get; }

    void Enable();
    void Disable();
    void Toggle();
}
