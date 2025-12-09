using UnityEngine;
using static UnityEditor.Progress;

public class WireChange : MonoBehaviour
{
    public capacitorPower cP;

    [SerializeField] public GameObject[] connections;
    [SerializeField] Animator EpointAnim;
    public bool isPowered;
    private float cooldown = 3f;
    private float newCooldown;
    private bool cooledDown = false;
        
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Butterfly"))
        {
            EpointAnim.SetBool("IsOn", true);
            isPowered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Butterfly"))
        {
            EpointAnim.SetBool("IsOn", false);
            isPowered = false;
            StartCooldown();
        }

    }


    private void Update()
    {
        if (isPowered == true)
        {
            foreach (GameObject item in connections)
            {
                if(item.CompareTag("Wire"))
                {
                    SpriteRenderer srWire = item.GetComponent<SpriteRenderer>();
                    srWire.color = Color.yellow;
                }
                if (item.CompareTag("Capacitor"))
                {
                    Animator capacitorAnim = item.GetComponent<Animator>();
                    capacitorAnim.SetBool("isOn", true);
                    cP.isPowered = true;
                    Debug.Log("on!");
                }
                if (item.CompareTag("LED"))
                {
                    SpriteRenderer srLED = item.GetComponent<SpriteRenderer>();
                    srLED.color = Color.green;
                }

            }
        }
        else
        {
            foreach (GameObject item in connections)
            {
                if(item.CompareTag("Wire"))
                {
                    SpriteRenderer srWire = item.GetComponent<SpriteRenderer>();
                    srWire.color = Color.gray;
                }
                if (item.CompareTag("Capacitor"))
                {
                    Animator capacitorAnim = item.GetComponent<Animator>();
                    
                    
                    if(Time.time >= newCooldown)
                    {
                        cooledDown = true;
                    }
                    else
                    {
                        cooledDown = false;
                    }

                    if (cooledDown == true)
                    {
                        capacitorAnim.SetBool("isOn", false);
                        cP.isPowered = false;
                    }
                }
                if (item.CompareTag("LED"))
                {
                    SpriteRenderer srLED = item.GetComponent<SpriteRenderer>();
                    srLED.color = Color.white;
                }

            }
        }
    }

    private void StartCooldown()
    {
        newCooldown = Time.time + cooldown;
    }
}
