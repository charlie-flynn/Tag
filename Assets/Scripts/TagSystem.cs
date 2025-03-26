using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TagSystem : MonoBehaviour
{
    [SerializeField]
    private bool _startTagged = false;

    [SerializeField]
    private float _tagImmunityDuration = 1.0f;

    private bool _tagImmune = false;

    private bool _tagged = false;

    public bool Tagged { get { return _tagged; } }

    private void Start()
    {
        _tagged = _startTagged;
        if (TryGetComponent(out TrailRenderer renderer))
            renderer.emitting = _startTagged;
    }

    private void SetTagImmuneFalse() { _tagImmune = false; }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Tagged) return;

        if (collision.gameObject.TryGetComponent(out TagSystem tagSystem))
        {
            if (tagSystem.Tag())
            {
                _tagged = false;
                _tagImmune = true;
                if (TryGetComponent(out TrailRenderer renderer))
                    renderer.emitting = false;
                Invoke(nameof(SetTagImmuneFalse), _tagImmunityDuration);
            }
        }
    }

    public bool Tag()
    {
        // if already it, do nothing
        if (Tagged) return false;

        // if immune to tag, return
        if (_tagImmune) return false;

        _tagged = true;

        if (TryGetComponent(out TrailRenderer renderer))
            renderer.emitting = true;

        return true;
    }
}
