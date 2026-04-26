using UnityEngine;

public enum GunDirection
{
    Up,
    Down,
    Left,
    Right
}
public class Gun : MonoBehaviour
{
    public GunDirection gunDirection;
    public GameObject bulletOBJ;
    public Transform firePos;
    public float spawnRate;

    public float spawnTime;
    public bool isShooting;

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnRate)
            {
                SpawnBullet();
                spawnTime = 0;
            }
        }
    }

    void SpawnBullet()
    {
        GameObject bt = Instantiate(bulletOBJ, firePos.position, Quaternion.identity);
        bt.GetComponent<Rigidbody2D>().AddForce((gunDirection== GunDirection.Left? Vector3.left: gunDirection== GunDirection.Right? Vector3.right: gunDirection== GunDirection.Up? Vector3.up: Vector3.zero) * 10, ForceMode2D.Impulse);
        Destroy(bt, 1f);
    }
}
