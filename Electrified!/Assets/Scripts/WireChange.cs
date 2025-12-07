using UnityEngine;

public class WireChange : MonoBehaviour
{
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
                    Debug.Log("Wire is on");
                }
                if (item.CompareTag("Capacitor"))
                {
                    Animator capacitorAnim = item.GetComponent<Animator>();
                    capacitorAnim.SetBool("isOn", true);
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
                    Debug.Log("Wire is off");
                }
                if (item.CompareTag("Capacitor"))
                {
                    Animator capacitorAnim = item.GetComponent<Animator>();
                    
                    
                    if(Time.time >= newCooldown)
                    {
                        cooledDown = true;
                        Debug.Log("off");
                    }
                    else
                    {
                        cooledDown = false;
                    }

                    if (cooledDown == true)
                    {
                        capacitorAnim.SetBool("isOn", false);
                    }
                }

            }
        }
    }

    private void StartCooldown()
    {
        newCooldown = Time.time + cooldown;
    }
}
