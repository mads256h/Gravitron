﻿using UnityEngine;


[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(Rigidbody2D))]
public class Platform : IOBase {

    public Vector2 Direction = new Vector2(0, 0);
    public float Speed = 1.0f;
    public float WaitTime = 1.0f;


    public Sprite EnabledSprite;
    public Sprite DisabledSprite;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _startDirection;
    private Vector2 _finishDirection;

    private bool _moveDown = false;
    private float _waitTimer = 0.0f;

	// Use this for initialization
	void Start ()
	{
	    _spriteRenderer = GetComponent<SpriteRenderer>();
        InputUpdate();
	    _rigidbody2D = GetComponent<Rigidbody2D>();
        _startDirection = _rigidbody2D.position;
        _finishDirection = new Vector2(_rigidbody2D.position.x + Direction.x, _rigidbody2D.position.y + Direction.y);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    if (!Enabled) return;

	    _waitTimer += Time.fixedDeltaTime;

	    if (_waitTimer <= WaitTime) return;

	    if (!_moveDown)
	    {
	        _rigidbody2D.MovePosition(new Vector2(_startDirection.x, _rigidbody2D.position.y + Direction.normalized.y * Speed * Time.fixedDeltaTime));
	        Debug.DrawLine(transform.position, new Vector2(_startDirection.x, _startDirection.y + _finishDirection.y));
	        if (_rigidbody2D.position.y >= _finishDirection.y)
	        {
	            _rigidbody2D.MovePosition(_finishDirection);
                _moveDown = true;
	            _waitTimer = 0;
	        }
	            
	    }

	    if (_moveDown)
	    {
	        _rigidbody2D.MovePosition(new Vector2(_startDirection.x, _rigidbody2D.position.y - Direction.normalized.y * Speed * Time.fixedDeltaTime));
	        Debug.DrawLine(transform.position, new Vector2(_startDirection.x, _startDirection.y));
	        if (_rigidbody2D.position.y <= _startDirection.y)
	        {
                _rigidbody2D.MovePosition(_startDirection);
	            _moveDown = false;
	            _waitTimer = 0;
            }
	            
	    }

		
	}

    public override void InputUpdate()
    {
        base.InputUpdate();
        _spriteRenderer.sprite = Enabled ? EnabledSprite : DisabledSprite;
    }
}
