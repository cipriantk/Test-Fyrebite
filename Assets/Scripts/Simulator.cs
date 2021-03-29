using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Simulator : MonoBehaviour
{   
    
    [SerializeField]
    private LeftSword _leftSword;
    [SerializeField]
    private RightSword _rightSword;

    
    private float _leftSwordAngle = 0;
    private float _rightSwordAngle = 0;
    private void Awake()
    {
        MessagingSystem.Register(MessagingSystemMessages.SliderMoved , OnSliderMoved);
        MessagingSystem.Register(MessagingSystemMessages.SimulationStarted , OnSimulationStarted);
        MessagingSystem.Register(MessagingSystemMessages.ToggleDebugView , OnToggleDebugView);
    }

    private void Start()
    {
        Vector2 leftSwordOffset = GeneralSettings.instance.GetLeftSwordOffset();
        Vector2 rightSwordOffset = GeneralSettings.instance.GetRightSowrdOffset();

        _leftSword.transform.localPosition += new Vector3(leftSwordOffset.x, leftSwordOffset.y, 0);
        _rightSword.transform.localPosition += new Vector3(rightSwordOffset.x, rightSwordOffset.y, 0);
    }

    private void OnSliderMoved(MessageData obj)
    {
        Enum.TryParse(obj.data["SliderEff"].Value, out SliderEff sliderUsed);
        float sliderValue =  -obj.data["SliderValue"].AsFloat;
       
        if (sliderUsed == SliderEff.LeftSlider)
        {
            _leftSwordAngle = sliderValue;
        }
        else if (sliderUsed == SliderEff.RightSlider)
        {
            _rightSwordAngle = sliderValue;
        }
    }
    
    
    private void OnSimulationStarted(MessageData obj)
    {   
       
        _leftSword.ResetPosition();
        _rightSword.ResetPosition();

        float animationTime = GeneralSettings.instance.GetAnimationTime();
        
        _leftSword.transform.DORotate(new Vector3(0, 0, _leftSwordAngle), animationTime);
        _rightSword.transform.DORotate(new Vector3(0, 0, _rightSwordAngle), animationTime).OnComplete(()=>
        {
            MessagingSystem.SendMsg(MessagingSystemMessages.SimulationFinished);
        });
    }
    
    
    private void OnToggleDebugView(MessageData obj)
    {   
        bool debugView =  obj.data["DebugView"].AsBool;
        
        _leftSword.gameObject.SetActive(!debugView);
        _rightSword.gameObject.SetActive(!debugView);
        
    }

}
