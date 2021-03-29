using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwordSettings", menuName = "SwordSettings")]
public class SwordSettings : ScriptableObject
{
    [SerializeField] private Vector2 _leftSwordOffset = new Vector2(0, 0);
    [SerializeField] private Vector2 _rightSwordOffset = new Vector2(0, 0);

    [Space] [SerializeField] private float _animationTime;

    [Space] [SerializeField] private float _minAngleLeftSword;
    [SerializeField] private float _maxAngleLeftSword;

    [Space] [SerializeField] private float _minAngleRightSword;
    [SerializeField] private float _maxAngleRightSword;

    public Vector3 GetLeftSowrdOffset()
    {
        return _leftSwordOffset;
    }

    public Vector3 GetRightSwordOffset()
    {
        return _rightSwordOffset;
    }

    public float GetAnimationTime()
    {
        return _animationTime;
    }

    public Vector2 GetAnglesLeftSword()
    {
        return new Vector2(_minAngleLeftSword, _maxAngleLeftSword);
    }

    public Vector2 GetAnglesRightSword()
    {
        return new Vector2(_minAngleRightSword, _maxAngleRightSword);
    }


    private void OnValidate()
    {
        _minAngleLeftSword = Mathf.Clamp(_minAngleLeftSword, -80, -20);
        _maxAngleLeftSword = Mathf.Clamp(_maxAngleLeftSword, 20, 80);
        
        _minAngleRightSword = Mathf.Clamp(_minAngleRightSword, -80, -20);
        _maxAngleRightSword = Mathf.Clamp(_maxAngleRightSword, 20, 80);
    }
}