// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;

// public class PlayerAttackShoot : MonoBehaviour{

//       public Transform firePoint;
//       public GameObject projectilePrefab;
//       public float projectileSpeed = 10f;
//       public float attackRate = 2f;
//       public string input = "Player1_Shoot";
//       private float nextAttackTime = 0f;

//       void Update(){
//            if (Time.time >= nextAttackTime){
//                  if (Input.GetAxis(input) > 0){
//                         playerFire();
//                         nextAttackTime = Time.time + 1f / attackRate;
//                   }
//             }
//       }

//       void playerFire(){
//             Vector2 fwd = (firePoint.position - this.transform.position).normalized;
//             GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
//             projectile.GetComponent<Rigidbody2D>().AddForce(fwd * projectileSpeed, ForceMode2D.Impulse);
//       }
// }

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackShoot : MonoBehaviour{

      public Transform firePoint;
      public GameObject projectilePrefab;
      public float projectileSpeed = 10f;
      public float attackRate = 2f;
      public string input = "Player1_Shoot";
      public float gravity = -9.81f; // acceleration due to gravity
      private float nextAttackTime = 0f;

      void Update(){
           if (Time.time >= nextAttackTime){
                 if (Input.GetAxis(input) > 0){
                        playerFire();
                        nextAttackTime = Time.time + 1f / attackRate;
                  }
            }
      }

      void playerFire(){
            Vector2 fwd = (firePoint.position - this.transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            
            // add the physics components to the projectile
            Rigidbody2D rb2d = projectile.GetComponent<Rigidbody2D>();
            rb2d.gravityScale = 1f; // enable gravity
            rb2d.AddForce(fwd * projectileSpeed, ForceMode2D.Impulse);
            rb2d.AddForce(Vector2.up * gravity, ForceMode2D.Impulse); // add upward force to create an arch
      }
}
