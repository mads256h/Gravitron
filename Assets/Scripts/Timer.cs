using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : IOBase
{
    public bool StartsEnabled = true;

    public float Interval = 1.0f;

    public Transform TimerHeadRigidbody2D;

    private float _timer = 0.0f;

	// Use this for initialization
	void Start ()
	{
	    Enabled = StartsEnabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;
        StartCoroutine(EnumeratorUpdate());
    }

    private IEnumerator EnumeratorUpdate()
    {
        _timer += Time.deltaTime / Interval;

        if (_timer >= 1.0f)
        {
            _timer = 0.0f;

            OutputEnable();

            yield return new WaitForSeconds(0.16f);

            OutputDisable();
        }

        TimerHeadRigidbody2D.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Mathf.Lerp(0.0f, -360.0f, _timer)));
    }
}
