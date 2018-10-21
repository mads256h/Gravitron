using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Shooter : MonoBehaviour
{

    public float Interval = 1.0f;

    public GameObject Bullet;

    private float _timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _timer += Time.deltaTime;

	    if (_timer > Interval)
	    {
	        _timer = 0;

	        Instantiate(Bullet, transform.position + transform.right, Bullet.transform.rotation);
	    }
	}
}
