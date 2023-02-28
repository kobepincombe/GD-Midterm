using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

      private GameObject player1, player2;
      public static int player1Health = 100;
      public static int player2Health = 100;
      public int StartPlayerHealth = 100;
      public GameObject P1healthText, P2healthText;

      public bool isDefending = false;

      public static bool stairCaseUnlocked = false;
      //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

      private string sceneName;

      void Start(){
            player1 = GameObject.FindWithTag("Player1");
            player2 = GameObject.FindWithTag("Player2");
            sceneName = SceneManager.GetActiveScene().name;
            //if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
                  player1Health = player2Health = StartPlayerHealth;
            //}
            updateStatsDisplay();
      }


      public void player1GetHit(int damage){
           if (isDefending == false){
                  player1Health -= damage;
                  if (player1Health >=0){
                        updateStatsDisplay();
                  }
                  if (damage > 0){
                        player1.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation
                  }
            }

           if (player1Health > StartPlayerHealth){
                  player1Health = StartPlayerHealth;
                  updateStatsDisplay();
            }

           if (player1Health <= 0){
                  player1Health = 0;
                  updateStatsDisplay();
                  playerDies();
            }
      }

      public void player2GetHit(int damage){
             if (isDefending == false){
                    player2Health -= damage;
                    if (player2Health >=0){
                          updateStatsDisplay();
                    }
                    if (damage > 0){
                          player2.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation
                    }
              }

             if (player2Health > StartPlayerHealth){
                    player2Health = StartPlayerHealth;
                    updateStatsDisplay();
              }

             if (player2Health <= 0){
                    player2Health = 0;
                    updateStatsDisplay();
                    playerDies();
              }
        }

      public void updateStatsDisplay(){
            Text P1healthTextTemp = P1healthText.GetComponent<Text>();
            P1healthTextTemp.text = "P1 HEALTH:\n" + player1Health;
            Text P2healthTextTemp = P2healthText.GetComponent<Text>();
            P2healthTextTemp.text = "P2 HEALTH:\n" + player2Health;
      }

      public void playerDies(){
            player1.GetComponent<PlayerHurt>().playerDead();       //play Death animation
            StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
            player1.GetComponent<PlayerMove>().isAlive = false;
            player1.GetComponent<PlayerJump>().isAlive = false;
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("EndLose");
      }

      public void StartGame() {
            SceneManager.LoadScene("Level1");
      }

      public void RestartGame() {
            SceneManager.LoadScene("MainMenu");
            player1Health = player2Health = StartPlayerHealth;
      }

      public void QuitGame() {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }
}