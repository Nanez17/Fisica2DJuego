using UnityEngine;

public class Bullseye : MonoBehaviour
{
    public int doorID;
    public int points = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            DoorController.UnlockDoor(doorID);

            Destroy(gameObject);
            Destroy(collision.gameObject);

            ScoreManager.instance.AddPoints(points);
        }
    }
}
