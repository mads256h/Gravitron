using JetBrains.Annotations;
using UnityEngine;

namespace IO
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
    public sealed class IOShooter : IOInputBase
    {
        [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public GameObject Tip;

        [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public GameObject Bullet;


        protected override void InputUpdate()
        {
            base.InputUpdate();

            if (InputEnabled) Instantiate(Bullet, Tip.transform.position, Tip.transform.rotation);
        }
    }
}
