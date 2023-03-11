using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour{

      public int damage = 1;
      public GameObject hitEffectAnim;
      public float SelfDestructTime = 0.5f;
      public float SelfDestructVFX = 0.25f;
      public SpriteRenderer projectileArt;
      private GameHandler gameHandler;

      void Start(){
           gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
           projectileArt = GetComponentInChildren<SpriteRenderer>();
           selfDestruct();
      }

      void OnTriggerEnter2D(Collider2D other){
              GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
              StartCoroutine(selfDestructHit(animEffect));
              Destroy (animEffect, 0.5f);
              projectileArt.enabled = false;
              if (other.gameObject.tag == "Player1") {
                  gameHandler.player1GetHit(damage);
              } else if (other.gameObject.tag == "Player2") {
                  gameHandler.player2GetHit(damage);
              }
      }

      IEnumerator selfDestructHit(GameObject VFX){
            yield return new WaitForSeconds(SelfDestructVFX);
            Destroy (VFX);
            Destroy (gameObject);
      }

      IEnumerator selfDestruct(){
            yield return new WaitForSeconds(SelfDestructTime);
            Destroy (gameObject);
      }
}