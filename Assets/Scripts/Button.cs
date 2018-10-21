using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(Collider2D), typeof(Rigidbody2D))]
public class Button : MonoBehaviour
{
    public Component[] Components;
    public IOInterface[] IoInterfaces;

    public Sprite OpenSprite;
    public Sprite ClosedSprite;

    private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start ()
	{
	    _spriteRenderer = GetComponent<SpriteRenderer>();
	    _spriteRenderer.sprite = OpenSprite;

        IoInterfaces = new IOInterface[Components.Length];

        for (int i = 0; i < Components.Length; i++)
	    {
            Component component = Components[i];
            var ioInterface = component as IOInterface;
	        if (ioInterface != null)
	            IoInterfaces[i] = ioInterface;
	        else
	        {
	            Debug.LogErrorFormat("Component: {0} does not implement IOInterface", component);
	        }
        }
	    
	}
	

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") ||
            col.gameObject.layer == LayerMask.NameToLayer("GravAble"))
        {
            foreach (var ioInterface in IoInterfaces)
            {
                ioInterface.Enable();
            }
            _spriteRenderer.sprite = ClosedSprite;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") ||
            col.gameObject.layer == LayerMask.NameToLayer("GravAble"))
        {
            foreach (var ioInterface in IoInterfaces)
            {
                ioInterface.Disable();
            }
            _spriteRenderer.sprite = OpenSprite;
        }
    }


}
