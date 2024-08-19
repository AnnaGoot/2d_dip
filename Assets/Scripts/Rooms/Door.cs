using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActivateRoon(true);
                previousRoom.GetComponent<Room>().ActivateRoon(false);

            }
            else
            {
                cam.MoveToNewRoom(previousRoom);
                previousRoom.GetComponent<Room>().ActivateRoon(true);
                nextRoom.GetComponent<Room>().ActivateRoon(false);
            }
        }
    }
}
