using UnityEngine;

public class MatkotMinigame : MonoBehaviour
{
    [SerializeField] GameObject ball;
    public float speed = 5f; // Speed at which the object will move
    private bool direction;

    void Update()
    {
        if (!direction)
        {
            ball.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else
        {
            ball.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Matka"))
        {
            Debug.Log("Ball collided with Matka");
            // Reverse the direction for example
            direction = !direction;
        }
    }
}
