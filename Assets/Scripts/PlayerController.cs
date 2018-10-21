using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public float Speed = 20f;

    public float JumpForce = 20f;

    public bool IsGrounded = true;

    public LayerMask GravAbleLayer;

    public Gravitron Gravitron;

    public float JumpInterval = 0.1f;

    private new Rigidbody2D rigidbody2D;

    private float _jumpTimer = 0.0f;

	// Use this for initialization
	private void Start ()
	{
	    rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	private void FixedUpdate ()
	{
	    _jumpTimer += Time.fixedDeltaTime;

        rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * Speed, rigidbody2D.velocity.y);

	    if (IsGrounded && _jumpTimer >= JumpInterval && Math.Abs(Input.GetAxisRaw("Jump")) > 0.1f)
	    {
	        IsGrounded = false;
	        _jumpTimer = 0;
            rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
	    }

	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        foreach (var contact in col.contacts)
        {
            if (contact.point.y < transform.position.y - 0.6)
            {
                IsGrounded = true;
                break;
            }
        }
        
        if (col.collider.gameObject.layer == 9)
        {
            Gravitron.DropObject();
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    public void Die()
    {
        Debug.Log("Im dead xd lol");
    }
}
