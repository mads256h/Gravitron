﻿using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class PlayerController : MonoBehaviour
{
    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float Speed = 20f;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float JumpForce = 20f;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public bool IsGrounded = true;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public LayerMask GravAbleLayer;

    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Gravitron Gravitron;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float JumpInterval = 0.1f;

    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Canvas DeadCanvas;

    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Animator DeadAnimator;

    private bool _enabled = true;

    [CanBeNull] private Rigidbody2D _rigidbody2D;

    private float _jumpTimer = 0.0f;

	// Use this for initialization
	[UsedImplicitly(ImplicitUseKindFlags.Access)]
	private void Start ()
	{
	    _rigidbody2D = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    private void FixedUpdate ()
    {
        if (!_enabled) return;

        if (Input.GetKey(KeyCode.R)) Die();

	    _jumpTimer += Time.fixedDeltaTime;

        _rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * Speed, _rigidbody2D.velocity.y);

	    if (IsGrounded && _jumpTimer >= JumpInterval && Math.Abs(Input.GetAxisRaw("Jump")) > 0.1f)
	    {
	        IsGrounded = false;
	        _jumpTimer = 0;
            _rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
	    }

	    if (_rigidbody2D.position.y < -10.0f)
	        Die();

	}

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
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
        
        if (col.collider.gameObject.CompareTag("Gravitronned"))
        {
            Gravitron.DropObject();
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    public void Die()
    {
        DeadCanvas.enabled = true;
        DeadAnimator.enabled = true;
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        _enabled = false;
        Gravitron.DropObject();
        Destroy(Gravitron);

        StartCoroutine(changeLevel());
    }

    private IEnumerator changeLevel()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("MainScene");
    }
}
