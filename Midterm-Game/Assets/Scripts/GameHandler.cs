using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour {

      private GameObject player1, player2;
      public static int player1Health = 500;
      public static int player2Health = 500;
      public int StartPlayerHealth = 500;
      public GameObject P1healthText, P2healthText;
      private bool P1_isInvincible = false;
      private bool P2_isInvincible = false;
      private float invincibilityDurationSeconds = 0.25f;
      public bool P1_isDefending = false;
      public bool P2_isDefending = false;
      public Sprite[] player1Sprites;
      public Sprite[] player2Sprites;
      public SpriteRenderer player1SpriteRenderer;
      public SpriteRenderer player2SpriteRenderer;
      public static bool GameisPaused = false;
      public GameObject pauseMenuUI;
      public AudioMixer mixer;
      public static float volumeLevel = 1.0f;
      
      private Slider sliderVolumeCtrl;
      private string sceneName;

      void Awake () {
            SetLevel (volumeLevel);
            GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
            if (sliderTemp != null){
                  sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                  sliderVolumeCtrl.value = volumeLevel;
            }
      }

      void Start() {
            player1 = GameObject.FindWithTag("Player1");
            player2 = GameObject.FindWithTag("Player2");
            sceneName = SceneManager.GetActiveScene().name;
            if (sceneName=="MainMenu")
                  player1Health = player2Health = StartPlayerHealth;

            updateStatsDisplay();

            pauseMenuUI.SetActive(false);
            GameisPaused = false;
      }

      void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                  if (GameisPaused) {
                        Resume();
                  } else {
                        Pause();
                  }
            }
      }

      private void UpdatePlayerSprites() {
            float player1HealthPercentage = (float)player1Health / (float)StartPlayerHealth;
            float player2HealthPercentage = (float)player2Health / (float)StartPlayerHealth;

            int player1SpriteIndex = Mathf.CeilToInt(player1HealthPercentage / 0.25f);
            int player2SpriteIndex = Mathf.CeilToInt(player2HealthPercentage / 0.25f);

            if (player1Sprites.Length > player1SpriteIndex && player1SpriteRenderer != null) {
                player1SpriteRenderer.sprite = player1Sprites[player1SpriteIndex];
            }
            if (player2Sprites.Length > player2SpriteIndex && player2SpriteRenderer != null) {
                player2SpriteRenderer.sprite = player2Sprites[player2SpriteIndex];
            }
      }

      public void player1GetHit(int damage) {
            if (P1_isDefending == false){
                  if (P1_isInvincible) return;
                  player1Health -= damage;
                  if (player1Health >=0){
                        updateStatsDisplay();
                  }
                  if (damage > 0){
                        player1.GetComponent<PlayerHurt>().playerHit(); 
                  }
            }

            if (player1Health > StartPlayerHealth) {
                  player1Health = StartPlayerHealth;
                  updateStatsDisplay();
            }

            if (player1Health <= 0) {
                  player1Health = 0;
                  updateStatsDisplay();
                  playerDies(1);
            }
            StartCoroutine(BecomeTemporarilyInvincible(1));
            UpdatePlayerSprites();
      }

      public void player2GetHit(int damage) {
            if (P2_isDefending == false) {
                   if (P2_isInvincible) return;
                   player2Health -= damage;
                   if (player2Health >=0){
                         updateStatsDisplay();
                   }
                   if (damage > 0){
                         player2.GetComponent<PlayerHurt>().playerHit(); 
                   }
            }

            if (player2Health > StartPlayerHealth){
                   player2Health = StartPlayerHealth;
                   updateStatsDisplay();
            }

            if (player2Health <= 0){
                   player2Health = 0;
                   updateStatsDisplay();
                   playerDies(2);
            }
            StartCoroutine(BecomeTemporarilyInvincible(2));
            UpdatePlayerSprites();
      }

      private IEnumerator BecomeTemporarilyInvincible(int num)
      {
          if (num == 1) {
              P1_isInvincible = true;
              yield return new WaitForSeconds(invincibilityDurationSeconds);
              P1_isInvincible = false;
          } else {
              P2_isInvincible = true;
              yield return new WaitForSeconds(invincibilityDurationSeconds);
              P2_isInvincible = false;
          }
      }


      public void updateStatsDisplay(){
            Text P1healthTextTemp = P1healthText.GetComponent<Text>();
            P1healthTextTemp.text = "P1 HEALTH:\n" + player1Health;
            Text P2healthTextTemp = P2healthText.GetComponent<Text>();
            P2healthTextTemp.text = "P2 HEALTH:\n" + player2Health;
      }

      public void playerDies(int num){
            if (num == 1) {
                player1.GetComponent<PlayerHurt>().playerDead();
                StartCoroutine(DeathPause(1));
            } else {
                player2.GetComponent<PlayerHurt>().playerDead();
                StartCoroutine(DeathPause(2));
            }
      }

      IEnumerator DeathPause(int num){
            if (num == 1) {
                player1.GetComponent<PlayerMove>().isAlive = false;
                player1.GetComponent<PlayerJump>().isAlive = false;
                yield return new WaitForSeconds(1.0f);
                SceneManager.LoadScene("P1_Lose");
            } else {
                player2.GetComponent<PlayerMove>().isAlive = false;
                player2.GetComponent<PlayerJump>().isAlive = false;
                yield return new WaitForSeconds(1.0f);
                SceneManager.LoadScene("P2_Lose");
            }
      }

      public void StartGame() {
            SceneManager.LoadScene("MainMenuLEGIT");
      }

      public void RestartGame() {
            Time.timeScale = 1f;
            player1Health = player2Health = StartPlayerHealth;
            SceneManager.LoadScene("JCC");
      }

      void Pause() {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameisPaused = true;
      }

      public void Resume() {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameisPaused = false;
      }

      public void SetLevel (float sliderValue) {
            mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
            volumeLevel = sliderValue;
      }

      public void QuitGame() {
            Time.timeScale = 1f;
            player1Health = player2Health = StartPlayerHealth;
            SceneManager.LoadScene("MainMenuLEGIT");
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }
}
