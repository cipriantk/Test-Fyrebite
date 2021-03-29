using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{

   private bool _collisionState = false;

   private void OnTriggerStay2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("PredictionSword"))
      {
         _collisionState = true;
         SendCollisionState();
      }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("PredictionSword"))
      {
         _collisionState = false;
         SendCollisionState();
      }
   }

   private void SendCollisionState()
   {
      JSONObject data = new JSONObject();
      data.Add("CollisionState", _collisionState);

      MessageData messageData = new MessageData();
      messageData.data = data;
      
      MessagingSystem.SendMsg(MessagingSystemMessages.CollisionStateChanged, messageData);
   }
  
}
