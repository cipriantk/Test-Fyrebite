using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SliderEff
{
    LeftSlider,
    RightSlider
}

public class UIGameplayMenu : MonoBehaviour
{   
    
    [SerializeField] private GameObject _content;
    [Space]
    [SerializeField] private Slider _leftSlider;
    [SerializeField] private Slider _rightSlider;
    [SerializeField] private Button _simulationButton;
    
    [Space]
    [SerializeField] private TextMeshProUGUI _collisionText;
    
    [Space]
    [SerializeField] private Toggle _debugViewToggle;

    private void Awake()
    {
        MessagingSystem.Register(MessagingSystemMessages.CollisionStateChanged, OnCollisionStateChanged);
        MessagingSystem.Register(MessagingSystemMessages.SimulationFinished, OnSimulationFinished);
    }


    private void Start()
    {
        Vector2 anglesLeftSword = GeneralSettings.instance.GetAnglesLeftSword();
        Vector2 anglesRightSword = GeneralSettings.instance.GetAnglesRightSword();

        _leftSlider.minValue = anglesLeftSword.x;
        _leftSlider.maxValue = anglesLeftSword.y;
        
        _rightSlider.minValue = anglesRightSword.x;
        _rightSlider.maxValue = anglesRightSword.y;
    }

    private void OnCollisionStateChanged(MessageData obj)
    {
        bool collisionState = obj.data["CollisionState"].AsBool;

        if (collisionState)
        {
            _collisionText.text = "Will Collide";
        }
        else
        {
            _collisionText.text = "Will Not Collide";
        }
    }

    public void OnLeftSliderMoved()
    {
        JSONObject data = new JSONObject();
        data.Add("SliderValue", _leftSlider.value);
        data.Add("SliderEff", SliderEff.LeftSlider.ToString());

        MessageData messageData = new MessageData();
        messageData.data = data;

        MessagingSystem.SendMsg(MessagingSystemMessages.SliderMoved, messageData);
    }

    public void OnRightSliderMoved()
    {
        JSONObject data = new JSONObject();
        data.Add("SliderValue", _rightSlider.value);
        data.Add("SliderEff", SliderEff.RightSlider.ToString());

        MessageData messageData = new MessageData();
        messageData.data = data;

        MessagingSystem.SendMsg(MessagingSystemMessages.SliderMoved, messageData);
    }

    public void StartSimulation()
    {
        MessagingSystem.SendMsg(MessagingSystemMessages.SimulationStarted);
        _content.gameObject.SetActive(false);
    }
    
    private void OnSimulationFinished(MessageData obj)
    {
        _content.SetActive(true);
    }

    public void ToggleDebugView()
    {
        JSONObject data = new JSONObject();
        data.Add("DebugView", _debugViewToggle.isOn);
        

        MessageData messageData = new MessageData();
        messageData.data = data;
        
        MessagingSystem.SendMsg(MessagingSystemMessages.ToggleDebugView, messageData);
        
        _simulationButton.gameObject.SetActive(!_debugViewToggle.isOn);
    }
}