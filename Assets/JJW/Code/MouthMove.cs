using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MouthMove : MonoBehaviour
{
   [SerializeField] private float yiledTime = 0.2f;
   [SerializeField] private Sprite changeImage;
   [SerializeField] private Sprite defaultImage;
   private SpriteRenderer _sr;

   private void Awake()
   {
      _sr = GetComponent<SpriteRenderer>();
      StartCoroutine(CloseAndOpenMouse());
   }

   private IEnumerator CloseAndOpenMouse()
   {
      while (true)
      {
         _sr.sprite = changeImage;
         yield return new WaitForSeconds(yiledTime);
         _sr.sprite = defaultImage;
         yield return new WaitForSeconds(yiledTime);
      }
   }
}
