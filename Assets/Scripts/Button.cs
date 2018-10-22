using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(Collider2D), typeof(Rigidbody2D))]
public class Button : IOBase
{
    public Sprite OpenSprite;
    public Sprite ClosedSprite;

    private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start ()
	{
	    _spriteRenderer = GetComponent<SpriteRenderer>();
	    _spriteRenderer.sprite = OpenSprite;	    
	}
	

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") ||
            col.gameObject.layer == LayerMask.NameToLayer("GravAble"))
        {
            OutputEnable();
            _spriteRenderer.sprite = ClosedSprite;
        }
    }

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
