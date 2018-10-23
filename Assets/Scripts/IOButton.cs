using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(Rigidbody2D))]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class IOButton : IOBase
{
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Sprite OpenSprite;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Sprite ClosedSprite;

    [CanBeNull] private SpriteRenderer _spriteRenderer;

	// Use this for initialization
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
	private void Start ()
	{
	    _spriteRenderer = GetComponent<SpriteRenderer>();
	    _spriteRenderer.sprite = OpenSprite;	    
	}

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") ||
            col.gameObject.layer == LayerMask.NameToLayer("GravAble"))
        {
            OutputEnable();
            _spriteRenderer.sprite = ClosedSprite;
        }
    }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") ||
            col.gameObject.layer == LayerMask.NameToLayer("GravAble"))
        {
            OutputDisable();
            _spriteRenderer.sprite = OpenSprite;
        }
    }


}
