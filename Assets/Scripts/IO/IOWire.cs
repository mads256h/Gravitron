using JetBrains.Annotations;
using UnityEngine;

namespace IO
{
    [RequireComponent(typeof(SpriteRenderer))]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
    public sealed class IOWire : IOInputBase
    {
        [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Sprite EnabledSprite;
        [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Sprite DisabledSprite;


        private SpriteRenderer _spriteRenderer;

        // Use this for initialization
        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        private void Start ()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            InputUpdate();
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        protected override void InputUpdate()
        {
            base.InputUpdate();
            _spriteRenderer.sprite = InputEnabled ? EnabledSprite : DisabledSprite;
        }
    }
}
