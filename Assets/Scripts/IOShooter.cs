using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class IOShooter : IOInputBase
{
    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public GameObject Tip;

    [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public GameObject Bullet;


    public override void InputEnable()
    {
        base.InputEnable();
        Instantiate(Bullet, Tip.transform.position, Tip.transform.rotation);
    }
}
