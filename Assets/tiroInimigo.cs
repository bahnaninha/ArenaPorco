using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiroInimigo : MonoBehaviour
{
    public GameObject bulletInimigoPrefab;
    public Transform bulletInimigoSpawn;
    public float bulletInimigoVida = 2.0f;
    public float bulletInimigoSpeed = 6.0f;
    public float visao = 5.0f;
    public Transform alvo;

    public float fireRate = 0.5F;
    private Area closestEnemy;
    private float nextFire = 0.0F;

    void FixedUpdate()
    {
        if (closestEnemy == null)
        {
            FindClosestEnemy();
        }
        else if(closestEnemy != null)
        {
            alvo = closestEnemy.transform;


            if (Vector3.Distance(transform.position, alvo.position) <= visao)
            {
                InvokeRepeating("FireInimigo", 0.2f, 3f);
            }
            else
            {
                CancelInvoke("FireInimigo");
            }
        }
    }

    void FireInimigo()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            // Cria um Bullet a partir de BulletPrefab
            var bullet = (GameObject)Instantiate(bulletInimigoPrefab, bulletInimigoSpawn.position, bulletInimigoSpawn.rotation);

            // Adiciona velocidade a Bullet
            bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward * bulletInimigoSpeed;

            // Destruir Bullet depois de n segundos
            Destroy(bullet, bulletInimigoVida);
        }
    }


    void FindClosestEnemy()
    {
        Area[] allEnemies = FindObjectsByType<Area>(FindObjectsSortMode.None);
        Area closest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Area enemy in allEnemies)
        {
           if (enemy.gameObject == this.gameObject) 
           {
               Debug.Log("Ingual"); 
               continue;
           }
           Debug.Log("Dinferente"); 
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                closest = enemy;
            }
            
            closestEnemy = closest;
        }

        
    }

}
