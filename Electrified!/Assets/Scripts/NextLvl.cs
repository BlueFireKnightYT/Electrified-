using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvl : MonoBehaviour
{
    private float neededTime = 3f;
    private float newCooldown = float.PositiveInfinity;

    public Animator SocketAnim;

    private void Start()
    {
        SocketAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Butterfly"))
        {
            StartCooldown();
            SocketAnim.SetBool("IsCharging", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Butterfly"))
        {
            StopCooldown();
            SocketAnim.SetBool("IsCharging", false);
        }
    }

    private void Update()
    {
        if (Time.time > newCooldown)
        {

            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index + 1);
        }
    }

    private void StartCooldown()
    {
        newCooldown = Time.time + neededTime;
    }
    private void StopCooldown()
    {
        newCooldown = Time.time + float.PositiveInfinity;
    }
}
