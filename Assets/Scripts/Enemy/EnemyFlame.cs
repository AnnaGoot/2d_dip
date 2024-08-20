using UnityEngine;

public class EnemyFlame : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private AudioClip flameSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            SoundManager.instance.PlaySound(flameSound);
        }
    }

}
