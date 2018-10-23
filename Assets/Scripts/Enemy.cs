using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class Enemy : MonoBehaviour
{

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float Speed = 2.0f;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public bool GoRight = false;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float RayDistance = 2.0f;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public LayerMask RayLayerMask;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public bool Enabled = true;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float MinMagnitude = 2.0f;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float Dramaticizer = 5.0f;

    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Rigidbody2D HeadRigidbody2D;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Rigidbody2D TorsoRigidbody2D;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Rigidbody2D ArmLeftRigidbody2D;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Rigidbody2D ArmRightRigidbody2D;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Rigidbody2D LegLeftRigidbody2D;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Rigidbody2D LegRightRigidbody2D;

    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Collider2D HeadCollider2D;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Collider2D TorsoCollider2D;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Collider2D ArmLeftCollider2D;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Collider2D ArmRightCollider2D;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Collider2D LegLeftCollider2D;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Collider2D LegRightCollider2D;

    [CanBeNull] private Rigidbody2D _rigidbody2D;
    [CanBeNull] private Collider2D _collider2D;

	// Use this for initialization
	[UsedImplicitly(ImplicitUseKindFlags.Access)]
	private void Start ()
	{
	    _rigidbody2D = GetComponent<Rigidbody2D>();
	    _collider2D = GetComponent<Collider2D>();
	}

    // Update is called once per frame
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    private void FixedUpdate ()
	{
	    if (!Enabled) return;

	    var rayCast = Physics2D.BoxCast(transform.position, Vector2.one, 0f, GoRight ? Vector2.right : Vector2.left, RayDistance,
	        RayLayerMask);

	    if (rayCast)
	    {
	        GoRight = !GoRight;
	    }

	    _rigidbody2D.velocity = new Vector2((GoRight ? 1 : -1) * Speed * Time.fixedDeltaTime, _rigidbody2D.velocity.y);
	}

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.layer == LayerMask.NameToLayer("GravAble") && col.relativeVelocity.magnitude > MinMagnitude) || col.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            _collider2D.enabled = false;
            _rigidbody2D.simulated = false;
            Enabled = false;
            HeadRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            TorsoRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            TorsoRigidbody2D.velocity = col.relativeVelocity * Dramaticizer;
            ArmLeftRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            ArmRightRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            LegLeftRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            LegRightRigidbody2D.bodyType = RigidbodyType2D.Dynamic;

            HeadCollider2D.enabled = true;
            TorsoCollider2D.enabled = true;
            ArmLeftCollider2D.enabled = true;
            ArmRightCollider2D.enabled = true;
            LegLeftCollider2D.enabled = true;
            LegRightCollider2D.enabled = true;
        }
    }
}
