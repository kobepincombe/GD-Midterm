using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ProjectileVFX_destroy : MonoBehaviour{

      public float destroyTime = 1f;

      void Start(){
           StartCoroutine(DestroyMe());
     }

      IEnumerator DestroyMe(){
           yield return new WaitForSeconds(destroyTime);
           Destroy(gameObject);
      }
}