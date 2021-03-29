using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine;


public class MessageData
{
    public JSONObject data = new JSONObject();
    public object sender;
}

public static class MessagingSystemMessages
{
    public static string SliderMoved = "SliderMoved";
    public static string CollisionStateChanged = "CollisionStateChanged";
    public static string SimulationStarted = "SimulationStarted";
    public static string SimulationFinished = "SimulationFinished";
    public static string ToggleDebugView = "ToggleDebugView";
}

public class MessagingItem
{
    private int _order = -1;
    private Action<MessageData> _action;

    public MessagingItem (Action<MessageData> action , int order =-1)
    {
        _action = action;
        _order = order;
    }

    public void InvokeAtion(MessageData info)
    {
        _action.Invoke(info);
    }

    public Action<MessageData> GetAction()
    {
        return _action;
    }

    public int GetOrder()
    {
        return _order;
    }
}

public static class MessagingSystem
{
    private static Dictionary<string, List<MessagingItem>> _messages = new Dictionary<string, List<MessagingItem>>();
    
    public static void SendMsg(string msg, MessageData info = null)
    { 
        if (_messages.ContainsKey(msg))
        {
            for (int i = 0; i < _messages[msg].Count; i++)
            {
                _messages[msg][i].InvokeAtion(info);
            }
        }
    }

    public static void Register(string msg, Action<MessageData> act, int order = -1)
    {
        if (!_messages.ContainsKey(msg))
            _messages.Add(msg, new List<MessagingItem>());

        if (!ContainsAction(msg, act))
        {
            MessagingItem item = new MessagingItem(act,order);
            _messages[msg].Add(item);
            _messages[msg] = _messages[msg].OrderBy(o => o.GetOrder()).ToList();
        }
    }
    
   

    public static void Unregister(string msg, Action<MessageData> act)
    {
        if (_messages.ContainsKey(msg))
        {
            if (ContainsAction(msg, act))
            {
                MessagingItem msgItem = GetMessageItem(msg, act);
                _messages[msg].Remove(msgItem);
            }
        }
    }

    private static bool ContainsAction(string msg, Action<MessageData> action)
    {
        List<MessagingItem> list = _messages[msg];

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].GetAction() == action)
                return true;
        }

        return false;
    }
    
    private static MessagingItem GetMessageItem(string msg, Action<MessageData> action)
    {
        List<MessagingItem> list = _messages[msg];

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].GetAction() == action)
                return list[i];
        }

        return null;
    }
}
