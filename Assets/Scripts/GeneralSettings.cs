using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSettings : MonoBehaviour
{
    public static GeneralSettings instance = null;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private SwordSettings swordSettings;


    public Vector2 GetLeftSwordOffset()
    {
        return swordSettings.GetLeftSowrdOffset();
    }

    public Vector2 GetRightSowrdOffset()
    {
        return swordSettings.GetRightSwordOffset();
    }

    public float GetAnimationTime()
    {
        return swordSettings.GetAnimationTime();
    }

    public Vector2 GetAnglesLeftSword()
    {
        return swordSettings.GetAnglesLeftSword();
    }

    public Vector2 GetAnglesRightSword()
    {
        return swordSettings.GetAnglesRightSword();
    }
}