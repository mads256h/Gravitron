using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    public float Speed = 20.0f;
    public float Force = 20.0f;
    private Rigidbody2D _rigidbody2D;

	// Use this for initialization
	private void Start ()
	{
	    _rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void FixedUpdate ()
	{
        _rigidbody2D.MovePosition(_rigidbody2D.position + (Vector2)transform.right * Speed * Time.fixedDeltaTime);
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.collider.gameObject.GetComponent<PlayerController>().Die();
        }
        else if (col.collider.gameObject.layer == LayerMask.NameToLayer("GravAble"))
        {
            if (col.collider.gameObject.tag != "Gravitronned")
            col.rigidbody.AddForce(transform.right * Force, ForceMode2D.Impulse);
        }

        Destroy(gameObject);
    }
}
