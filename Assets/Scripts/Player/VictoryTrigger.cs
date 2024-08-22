using Unity.VisualScripting;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private AudioClip victorySound;

    private void Start()
    {
        victoryScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            TriggerVictory();
    }

    private void TriggerVictory()
    {
        victoryScreen.SetActive(true);

        Time.timeScale = 0;

        if (victorySound != null)
            SoundManager.instance.PlaySound(victorySound);
    }
}
