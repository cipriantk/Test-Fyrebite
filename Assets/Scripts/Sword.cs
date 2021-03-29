using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
   [SerializeField] private TrailRenderer _trailRenderer;
   public void ResetPosition()
   {
      _trailRenderer.gameObject.SetActive(false);
      transform.rotation = Quaternion.Euler(0,0,0);
      _trailRenderer.gameObject.SetActive(true);
   }
}
