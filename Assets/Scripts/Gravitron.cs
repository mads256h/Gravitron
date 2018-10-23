using System;
using JetBrains.Annotations;
using UnityEngine;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class Gravitron : MonoBehaviour
{
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Camera Camera;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public LayerMask TargetLayer;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float ShortDistance = 2.0f;
    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float LongDistance = 1000.0f;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float LongForce = 10.0f;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float PuntForce = 10.0f;
    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float RotationSpeed = 10.0f;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float MinHoldTime = 0.5f;

    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Transform Tip;

    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float LineRenderWidth = 0.02f;
    [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Color LineRenderColor = new Color(1.0f, 0.415f, 0.0f);

    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Rigidbody2D CurrentObject;

    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Rigidbody2D PlayerRigidbody;

    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public AudioClip ChargeClip;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public AudioClip ClawsCloseClip;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public AudioClip ClawsOpenClip;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public AudioClip DropClip;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public AudioClip DryFireClip;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public AudioClip PickupClip;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public AudioClip TooHeavyClip;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public AudioClip FireClip;
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public AudioClip HoldLoopClip;

    [CanBeNull] private AudioSource _audioSource;

    [CanBeNull] private LineRenderer _corner1Renderer;
    [CanBeNull] private LineRenderer _corner2Renderer;
    [CanBeNull] private LineRenderer _corner3Renderer;
    [CanBeNull] private LineRenderer _corner4Renderer;

    private float _holdTimer = 0.0f;
    private bool _isHoldingDown = false;

	// Use this for initialization
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
	private void Start ()
	{
	    _audioSource = GetComponent<AudioSource>();

	    var lineRenderMaterial = new Material(Shader.Find("Unlit/Color")) {color = LineRenderColor};


	    var corner1Object = new GameObject("LineRenderer");
	    corner1Object.transform.parent = Tip;
	    _corner1Renderer = corner1Object.AddComponent<LineRenderer>();
	    _corner1Renderer.useWorldSpace = true;
	    _corner1Renderer.material = lineRenderMaterial;
	    _corner1Renderer.startWidth = LineRenderWidth;
	    _corner1Renderer.endWidth = LineRenderWidth;

        var corner2Object = new GameObject("LineRenderer");
	    corner2Object.transform.parent = Tip;
        _corner2Renderer = corner2Object.AddComponent<LineRenderer>();
	    _corner2Renderer.useWorldSpace = true;
	    _corner2Renderer.material = lineRenderMaterial;
	    _corner2Renderer.startWidth = LineRenderWidth;
	    _corner2Renderer.endWidth = LineRenderWidth;

        var corner3Object = new GameObject("LineRenderer");
	    corner3Object.transform.parent = Tip;
        _corner3Renderer = corner3Object.AddComponent<LineRenderer>();
	    _corner3Renderer.useWorldSpace = true;
	    _corner3Renderer.material = lineRenderMaterial;
	    _corner3Renderer.startWidth = LineRenderWidth;
	    _corner3Renderer.endWidth = LineRenderWidth;

        var corner4Object = new GameObject("LineRenderer");
	    corner4Object.transform.parent = Tip;
        _corner4Renderer = corner4Object.AddComponent<LineRenderer>();
	    _corner4Renderer.useWorldSpace = true;
	    _corner4Renderer.material = lineRenderMaterial;
	    _corner4Renderer.startWidth = LineRenderWidth;
	    _corner4Renderer.endWidth = LineRenderWidth;

	}

    // Update is called once per frame
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    private void Update ()
	{
	    var mousePos = Input.mousePosition;

	    Vector3 objectPos = Camera.WorldToScreenPoint(transform.position);
	    mousePos.x = mousePos.x - objectPos.x;
	    mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
	    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    void FixedUpdate()
    {
        if (CurrentObject == null)
        {
            _holdTimer += Time.fixedDeltaTime;
            if (_holdTimer > MinHoldTime)
            {
                if (Math.Abs(Input.GetAxisRaw("Fire1")) > 0.1f)
                {
                    var shortHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                        transform.right, ShortDistance, TargetLayer);

                    if (shortHit && shortHit.collider.gameObject.layer == LayerMask.NameToLayer("GravAble"))
                    {
                        _audioSource.PlayOneShot(FireClip);
                        //shortHit.rigidbody.velocity = (Vector2)transform.right * PuntForce + PlayerRigidbody.velocity;
                        shortHit.rigidbody.AddForceAtPosition((Vector2)transform.right * PuntForce, shortHit.point, ForceMode2D.Impulse);
                        _holdTimer = 0;
                    }
                }

                if (Math.Abs(Input.GetAxisRaw("Fire2")) > 0.1f)
                {
                    if (!_isHoldingDown)
                    {
                        _audioSource.PlayOneShot(ClawsOpenClip);
                    }

                    _isHoldingDown = true;


                    var shortHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                        transform.right, ShortDistance, TargetLayer);

                    if (shortHit && shortHit.collider.gameObject.layer == LayerMask.NameToLayer("GravAble"))
                    {
                        _corner1Renderer.enabled = true;
                        _corner2Renderer.enabled = true;
                        _corner3Renderer.enabled = true;
                        _corner4Renderer.enabled = true;
                        _audioSource.loop = true;
                        _audioSource.clip = HoldLoopClip;
                        _audioSource.Play();
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
                else
                {
                    _isHoldingDown = false;
                }


            }
        }

        if (CurrentObject != null)
        {
            
            _holdTimer += Time.fixedDeltaTime;
            

            if (Math.Abs(Input.GetAxisRaw("Fire1")) > 0.1f)
            {
                _corner1Renderer.enabled = false;
                _corner2Renderer.enabled = false;
                _corner3Renderer.enabled = false;
                _corner4Renderer.enabled = false;
                _audioSource.loop = false;
                _audioSource.Stop();
                _audioSource.PlayOneShot(FireClip);
                //CurrentObject.velocity = (Vector2)transform.right * PuntForce + PlayerRigidbody.velocity;
                CurrentObject.AddForce((Vector2)transform.right * PuntForce, ForceMode2D.Impulse);
                CurrentObject.tag = "Untagged";
                CurrentObject = null;
                _holdTimer = 0;
            }
            else
            {
                CurrentObject.MovePosition(transform.position + transform.right * ShortDistance);

                CurrentObject.MoveRotation(CurrentObject.rotation + Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime * RotationSpeed);

                GetBoxCorners();
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
        {
            _audioSource.loop = false;
            _audioSource.Stop();
            _audioSource.PlayOneShot(DropClip);
            CurrentObject.tag = "Untagged";
            CurrentObject.velocity = PlayerRigidbody.velocity;
            CurrentObject = null;
        }

        _corner1Renderer.enabled = false;
        _corner2Renderer.enabled = false;
        _corner3Renderer.enabled = false;
        _corner4Renderer.enabled = false;
        _holdTimer = 0;
    }
    

    private void GetBoxCorners()
    {

        Transform bcTransform = CurrentObject.transform;
        BoxCollider2D box = CurrentObject.GetComponent<BoxCollider2D>();
        // The collider's centre point in the world
        Vector3 worldPosition = bcTransform.TransformPoint(0, 0, 0);

        // The collider's local width and height, accounting for scale, divided by 2
        Vector2 size = new Vector2(box.size.x * bcTransform.lossyScale.x * 0.5f, box.size.y * bcTransform.lossyScale.y * 0.5f);

        // STEP 1: FIND LOCAL, UN-ROTATED CORNERS
        // Find the 4 corners of the BoxCollider2D in LOCAL space, if the BoxCollider2D had never been rotated
        Vector3 corner1 = new Vector2(-size.x, -size.y);
        Vector3 corner2 = new Vector2(-size.x, size.y);
        Vector3 corner3 = new Vector2(size.x, -size.y);
        Vector3 corner4 = new Vector2(size.x, size.y);

        // STEP 2: ROTATE CORNERS
        // Rotate those 4 corners around the centre of the collider to match its transform.rotation
        corner1 = RotatePointAroundPivot(corner1, Vector3.zero, bcTransform.eulerAngles);
        corner2 = RotatePointAroundPivot(corner2, Vector3.zero, bcTransform.eulerAngles);
        corner3 = RotatePointAroundPivot(corner3, Vector3.zero, bcTransform.eulerAngles);
        corner4 = RotatePointAroundPivot(corner4, Vector3.zero, bcTransform.eulerAngles);

        // STEP 3: FIND WORLD POSITION OF CORNERS
        // Add the 4 rotated corners above to our centre position in WORLD space - and we're done!
        corner1 = worldPosition + corner1;
        corner2 = worldPosition + corner2;
        corner3 = worldPosition + corner3;
        corner4 = worldPosition + corner4;


        _corner1Renderer.SetPositions(new[] { Tip.position, corner1 });
        _corner2Renderer.SetPositions(new[] { Tip.position, corner2 });
        _corner3Renderer.SetPositions(new[] { Tip.position, corner3 });
        _corner4Renderer.SetPositions(new[] { Tip.position, corner4 });
    }

    // Helper method courtesy of @aldonaletto
    // http://answers.unity3d.com/questions/532297/rotate-a-vector-around-a-certain-point.html
    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }
}
