using System.Collections.Generic;
using UnityEngine;
public class WireChange : MonoBehaviour
{
    public capacitorPower cP;
    public GameManager gm;

    [SerializeField] public GameObject[] connections;
    [SerializeField] Animator EpointAnim;
    public bool isPowered;
    private float capacitorDuration = 5f;
    private float newCooldown;
    private bool cooledDown = false;

        
    // Checks if something walks onto the items trigger and checks for the 'butterfly tag'
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Butterfly"))
        {
            EpointAnim.SetBool("IsOn", true);
            isPowered = true;
        }
    }

    // Checks if something walks out of the object and checks for the 'butterfly' tag
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
            //turns on the wire, capacitor and led
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

                    // Only happens once per LED GameObject
                    if (gm != null && !gm.countedLEDs.Contains(item))
                    {
                        gm.lightsOn++;
                        gm.countedLEDs.Add(item);
                    }
                }

            }
        }
        else
        {
            // Turns off the wire, capacitor and LED
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
                    
                    //turns off capacitor after 5 seconds
                    if(Time.time >= newCooldown)
                    {
                        cooledDown = true;
                    }
                    else
                    {
                        cooledDown = false;
                    }

                    if(cP != null)
                    { 
                    if (cooledDown == true)
                        {
                            capacitorAnim.SetBool("isOn", false);
                            cP.isPowered = false;
                        }
                    }
                }

                if (item.CompareTag("LED"))
                {
                    SpriteRenderer srLED = item.GetComponent<SpriteRenderer>();
                    srLED.color = Color.white;

                    // Only decrement if this LED was previously counted
                    if (gm != null && gm.countedLEDs.Contains(item))
                    {
                        gm.lightsOn--;
                        gm.countedLEDs.Remove(item);

                        // safety: prevent negative counts
                        if (gm.lightsOn < 0)
                            gm.lightsOn = 0;
                    }
                }

            }
        }
    }

    // starts the duration for the capacitor
    private void StartCooldown()
    {
        newCooldown = Time.time + capacitorDuration;
    }
}
