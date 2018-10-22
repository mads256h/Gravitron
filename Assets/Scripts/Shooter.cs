using UnityEngine;
[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Shooter : IOBase
{
    public float Interval = 1.0f;

    public GameObject Tip;

    public GameObject Bullet;


    public override void Enable()
    {
        base.Enable();
        Instantiate(Bullet, Tip.transform.position, Tip.transform.rotation);
    }
}
