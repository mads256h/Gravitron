using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Wire : IOBase
{
    public Sprite EnabledSprite;
    public Sprite DisabledSprite;


    private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start ()
	{
	    _spriteRenderer = GetComponent<SpriteRenderer>();
        InputUpdate();
	}

    public override void InputUpdate()
    {
        base.InputUpdate();
        _spriteRenderer.sprite = Enabled ? EnabledSprite : DisabledSprite;
    }
}
