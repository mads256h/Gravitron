using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Wire : MonoBehaviour, IOInterface
{
    public Sprite EnabledSprite;
    public Sprite DisabledSprite;


    private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start ()
	{
	    _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
	}

    public bool Enabled { get; private set; }

    private void UpdateSprite()
    {
        _spriteRenderer.sprite = Enabled ? EnabledSprite : DisabledSprite;
    }

    public void Enable()
    {
        Enabled = true;
        UpdateSprite();
    }

    public void Disable()
    {
        Enabled = false;
        UpdateSprite();
    }

    public void Toggle()
    {
        Enabled = !Enabled;
        UpdateSprite();
    }
}
