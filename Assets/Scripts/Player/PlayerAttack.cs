using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private int attackDamage;
    [SerializeField] private AudioClip attackSound;

    private Animator anim;
    private PlayerController playerController;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        {
            if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerController.canAttack())
            {
                Attack();
            }
        } 
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(attackSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        { 
        Health enemyHealth = collision.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }
}