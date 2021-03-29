using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPredictor : MonoBehaviour
{
    [SerializeField] private GameObject _leftSword;

    [SerializeField] private GameObject _rightSword;


    private SpriteRenderer _leftSwordSpriteRenderer;
    private SpriteRenderer _rightSwordSpriteRenderer;

    void Awake()
    {
        MessagingSystem.Register(MessagingSystemMessages.SliderMoved, OnSliderMoved);
        MessagingSystem.Register(MessagingSystemMessages.ToggleDebugView, OnToggleDebugView);
    }

    private void Start()
    {
        Vector2 leftSwordOffset = GeneralSettings.instance.GetLeftSwordOffset();
        Vector2 rightSwordOffset = GeneralSettings.instance.GetRightSowrdOffset();

        _leftSword.transform.localPosition += new Vector3(leftSwordOffset.x, leftSwordOffset.y, 0);
        _rightSword.transform.localPosition += new Vector3(rightSwordOffset.x, rightSwordOffset.y, 0);

        _leftSwordSpriteRenderer = _leftSword.GetComponent<SpriteRenderer>();
        _rightSwordSpriteRenderer = _rightSword.GetComponent<SpriteRenderer>();
    }

    private void OnSliderMoved(MessageData obj)
    {
        Enum.TryParse(obj.data["SliderEff"].Value, out SliderEff sliderUsed);
        float sliderValue = -obj.data["SliderValue"].AsFloat;

        if (sliderUsed == SliderEff.LeftSlider)
        {
            _leftSword.transform.localRotation = Quaternion.Euler(0, 0, sliderValue);
        }
        else if (sliderUsed == SliderEff.RightSlider)
        {
            _rightSword.transform.localRotation = Quaternion.Euler(0, 0, sliderValue);
        }
    }

    private void OnToggleDebugView(MessageData obj)
    {
        bool debugView = obj.data["DebugView"].AsBool;

        _leftSwordSpriteRenderer.enabled = debugView;
        _rightSwordSpriteRenderer.enabled = debugView;
    }
}