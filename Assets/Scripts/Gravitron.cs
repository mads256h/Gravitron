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

    public Rigidbody2D CurrentObject;

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
        if (CurrentObject == null)
        {
            _holdTimer += Time.fixedDeltaTime;
            if (_holdTimer > MinHoldTime && Math.Abs(Input.GetAxisRaw("Fire2")) > 0.1f)
            {
                var shortHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                    transform.right, ShortDistance, TargetLayer);

                if (shortHit && shortHit.collider.gameObject.layer == LayerMask.NameToLayer("GravAble"))
                {
                    CurrentObject = shortHit.rigidbody;
                    CurrentObject.velocity = Vector2.zero;
                    CurrentObject.angularVelocity = 0.0f;
                    CurrentObject.tag = "Gravitronned";
                    _holdTimer = 0;
                }
                else
                {
                    var longHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                        transform.right, LongDistance, TargetLayer);

                    if (longHit && longHit.collider.gameObject.layer == LayerMask.NameToLayer("GravAble"))
                    {
                        var vec = transform.position - longHit.transform.position;
                        longHit.rigidbody.AddForce(vec.normalized * (1 / vec.magnitude) * LongForce *
                                                   Time.fixedDeltaTime);
                    }
                }
            }
        }

        if (CurrentObject != null)
        {
            _holdTimer += Time.fixedDeltaTime;
            CurrentObject.MovePosition(transform.position + transform.right * ShortDistance);

            CurrentObject.MoveRotation(CurrentObject.rotation + Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime * RotationSpeed);


            if (Math.Abs(Input.GetAxisRaw("Fire1")) > 0.1f)
            {
                CurrentObject.velocity = transform.right * PuntForce;
                CurrentObject.tag = "Untagged";
                CurrentObject = null;
                _holdTimer = 0;
            }

            if (_holdTimer > MinHoldTime && Math.Abs(Input.GetAxisRaw("Fire2")) > 0.1f && CurrentObject != null)
            {
                DropObject();
            }
        }

    }

    public void DropObject()
    {
        if (CurrentObject != null)
        CurrentObject.tag = "Untagged";
        CurrentObject = null;
        _holdTimer = 0;
    }
}
