using UnityEngine;
using UnityEngine.AI;

public class Area : MonoBehaviour
{
    public float wanderRadius = 3f;
    public float wanderTimer = 0.5f;
    public float detectionRadius = 8f;

    
    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        

        
        
        // Tenta seguir o inimigo mais próximo
        Area closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            agent.SetDestination(closestEnemy.transform.position);
        }
        else if (timer >= wanderTimer)
        {
            // Se não encontrar nenhum inimigo próximo, anda aleatoriamente
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }
    

    Area FindClosestEnemy()
    {
        Area[] allEnemies = FindObjectsByType<Area>(FindObjectsSortMode.None);
        Area closest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Area enemy in allEnemies)
        {
            if (enemy == this) continue;

            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                closest = enemy;
            }
        }

        return closest;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
