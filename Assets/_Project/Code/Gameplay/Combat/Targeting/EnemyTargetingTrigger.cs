using Unity.VisualScripting;
using UnityEngine;

public class EnemyTargetingTrigger : MonoBehaviour
{
    public Gun gun;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gun.isShooting = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gun.isShooting = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision .CompareTag("Enemy"))
        {
            gun.isShooting = false;
        }
    }

}
