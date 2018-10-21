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

    private new Rigidbody2D rigidbody2D;

	// Use this for initialization
	private void Start ()
	{
	    rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	private void FixedUpdate () {
		
        rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * Speed, rigidbody2D.velocity.y);

	    if (IsGrounded && Math.Abs(Input.GetAxisRaw("Jump")) > 0.1f)
	    {
	        IsGrounded = false;
            rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
	    }

	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts[0].point.y < transform.position.y)
        IsGrounded = true;

        
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
