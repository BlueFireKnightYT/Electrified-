using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvl : MonoBehaviour
{
    private float neededTime = 3f;
    private float newCooldown = float.PositiveInfinity;

    [SerializeField] AudioSource src;
    [SerializeField] AudioClip clip;
    public Animator SocketAnim;

    private void Start()
    {
        SocketAnim = GetComponent<Animator>();
        src = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Butterfly"))
        {
            StartCooldown();
            SocketAnim.SetBool("IsCharging", true);
            src.PlayOneShot(clip);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Butterfly"))
        {
            StopCooldown();
            SocketAnim.SetBool("IsCharging", false);
            src.Stop();
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
