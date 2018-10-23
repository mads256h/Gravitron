using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(Rigidbody2D))]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class Bullet : MonoBehaviour
{

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float Speed = 20.0f;
    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float Force = 20.0f;
    [CanBeNull] private Rigidbody2D _rigidbody2D;

	// Use this for initialization
	[UsedImplicitly(ImplicitUseKindFlags.Access)]
	private void Start()
	{
	    _rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	[UsedImplicitly(ImplicitUseKindFlags.Access)]
	private void FixedUpdate()
	{
        _rigidbody2D?.MovePosition(_rigidbody2D.position + (Vector2)transform.right * Speed * Time.fixedDeltaTime);
		
	}

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    private void OnCollisionEnter2D([NotNull] Collision2D col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.collider.gameObject.GetComponent<PlayerController>().Die();
        }
        else if (col.collider.gameObject.layer == LayerMask.NameToLayer("GravAble"))
        {
            if (col.collider.gameObject.tag != "Gravitronned")
            col.rigidbody.AddForceAtPosition(transform.right * Force, transform.position, ForceMode2D.Impulse);
        }

        Destroy(gameObject);
    }
}
