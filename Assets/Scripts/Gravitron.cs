using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Policy;
using UnityEngine;

public class Gravitron : MonoBehaviour
{
    public Camera Camera;

    public LayerMask TargetLayer;

    public float ShortDistance = 2.0f;
    public float LongDistance = 1000.0f;

    public float LongForce = 10.0f;

    public float PuntForce = 10.0f;
    public float RotationSpeed = 10.0f;

    public float MinHoldTime = 0.5f;

    private Rigidbody2D _currentObject;

    private float _holdTimer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var mousePos = Input.mousePosition;

	    Vector3 objectPos = Camera.WorldToScreenPoint(transform.position);
	    mousePos.x = mousePos.x - objectPos.x;
	    mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
	    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void FixedUpdate()
    {
        if (_currentObject == null)
        {
            _holdTimer += Time.fixedDeltaTime;
            if (_holdTimer > MinHoldTime && Math.Abs(Input.GetAxisRaw("Fire2")) > 0.1f)
            {
                var shortHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                    transform.right, ShortDistance, TargetLayer);

                if (shortHit)
                {
                    Debug.Log("Object up close: " + shortHit.collider.name);
                    _currentObject = shortHit.rigidbody;
                    _holdTimer = 0;
                }
                else
                {
                    var longHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                        transform.right, LongDistance, TargetLayer);

                    if (longHit)
                    {
                        Debug.Log("Object far away: " + longHit.collider.name);
                        var vec = transform.position - longHit.transform.position;
                        longHit.rigidbody.AddForce(vec.normalized * (1 / vec.sqrMagnitude) * LongForce *
                                                   Time.fixedDeltaTime);
                    }
                }
            }
        }

        if (_currentObject != null)
        {
            _holdTimer += Time.fixedDeltaTime;
            _currentObject.MovePosition(transform.position + transform.right * ShortDistance);

            _currentObject.MoveRotation(_currentObject.rotation + Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime * RotationSpeed);


            if (Math.Abs(Input.GetAxisRaw("Fire1")) > 0.1f)
            {
                _currentObject.velocity = Vector2.zero;
                _currentObject.angularVelocity = 0.0f;
                _currentObject.AddForce(transform.right * PuntForce, ForceMode2D.Impulse);
                _currentObject = null;
                _holdTimer = 0;
            }

            if (_holdTimer > MinHoldTime && Math.Abs(Input.GetAxisRaw("Fire2")) > 0.1f && _currentObject != null)
            {
                _currentObject.velocity = Vector2.zero;
                _currentObject.angularVelocity = 0.0f;
                _currentObject = null;
                _holdTimer = 0;
            }
        }

    }
}
