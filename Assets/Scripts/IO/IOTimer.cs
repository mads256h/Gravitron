using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace IO
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
    public sealed class IOTimer : IOBase
    {
        [UsedImplicitly(ImplicitUseKindFlags.Assign)] public bool StartsEnabled = true;

        [UsedImplicitly(ImplicitUseKindFlags.Assign)] public float Interval = 1.0f;

        [CanBeNull] [UsedImplicitly(ImplicitUseKindFlags.Assign)] public Transform TimerHeadRigidbody2D;

        private float _timer = 0.0f;

        // Use this for initialization
        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        private void Start ()
        {
            InputEnabled = StartsEnabled;
        }

        // Update is called once per frame
        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        private void Update()
        {
            if (!InputEnabled) return;
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
}
