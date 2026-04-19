using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("References")]
    public GameObject SpawnGB;
    public SunRise sun;
    [Header("Settings")]
    public float SpawnTime = 3f;
    public float SpawnSpeed = 5f;
    float timer = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sun.State == SunRiseState.Day) 
        { 
             timer += Time.fixedDeltaTime;

            if(timer >= SpawnTime)
            {
                Spawn();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    void Spawn()
    {
        GameObject En = Instantiate(SpawnGB, transform.position, Quaternion.identity);
        En.GetComponent<Enemy>().speed = SpawnSpeed;
    }

   
}
