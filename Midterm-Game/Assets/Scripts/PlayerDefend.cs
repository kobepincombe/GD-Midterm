using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerDefend : MonoBehaviour{

      private GameHandler gameHandler;
      //public Animator animator;
      public Rigidbody2D rb2D;
      public GameObject shieldArt;
      public float defendTime = 1.0f;
      public float cdTime = 2.0f;
      private bool P1_onCD = false;
      private bool P2_onCD = false;
      public string input = "Player1_Defend";
      public int player_num = 1;

      void Start(){
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
           //animator = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
           shieldArt.SetActive(false);
      }

      void Update(){
            //Defense activation
           if (Input.GetAxis(input) > 0){
                  playerDefend(player_num);
            }
      }

      public void playerDefend(int player_num){
            if (player_num == 1) {
                if (P1_onCD) return;
                gameHandler.P1_isDefending = true;
            } else {
                if (P2_onCD) return;
                gameHandler.P2_isDefending = true;
            }
            shieldArt.SetActive(true);
            //animator.SetBool("Defend", true);
            StartCoroutine(playerNoDefend(player_num));
      }

      IEnumerator playerNoDefend(int player_num){
            yield return new WaitForSeconds(defendTime);
            if (player_num == 1) {
                gameHandler.P1_isDefending = false;
            } else {
                gameHandler.P2_isDefending = false;
            }
            shieldArt.SetActive(false);
            //animator.SetBool("Defend", false);
            StartCoroutine(playerDefendCooldown(player_num));
      }

      IEnumerator playerDefendCooldown(int player_num){
            if (player_num == 1) {
                P1_onCD = true;
                yield return new WaitForSeconds(cdTime);
                P1_onCD = false;
            } else {
                P2_onCD = true;
                yield return new WaitForSeconds(cdTime);
                P2_onCD = false;
            }
      }
}