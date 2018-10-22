using UnityEngine;
[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Shooter : MonoBehaviour, IOInterface
{

    public float Interval = 1.0f;

    public GameObject Tip;

    public GameObject Bullet;

    public bool StartsEnabled = true;

    private float _timer = 0;

	// Use this for initialization
	private void Start ()
	{
	    Enabled = StartsEnabled;
	}
	
	// Update is called once per frame
	private void Update ()
	{
	    if (!Enabled) return;

	    _timer += Time.deltaTime;

	    if (_timer > Interval)
	    {
	        _timer = 0;

	        Instantiate(Bullet, Tip.transform.position, Tip.transform.rotation);
	    }
	}

    public bool Enabled { get; private set; }
    public void Enable()
    {
        Enabled = true;
    }

    public void Disable()
    {
        Enabled = false;
    }

    public void Toggle()
    {
        Enabled = !Enabled;
    }
}
