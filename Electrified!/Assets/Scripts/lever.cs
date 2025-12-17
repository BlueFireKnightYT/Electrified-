using UnityEngine;
using UnityEngine.Rendering;
using static Unity.VisualScripting.Member;

public class lever : MonoBehaviour
{
    bool isPowered = false;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip click;
    public GameObject[] itemList;
    public GameManager gm;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;

    [SerializeField] float pitchVariance = 0.5f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Butterfly"))
        {
            if (isPowered == false)
            {
                isPowered = true;
                sr.sprite = sprite1;
            }

            else
            {
                isPowered = false;
                sr.sprite = sprite2;
            }

            float randomPitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);

            if (source != null)
            {
                source.pitch = randomPitch;
                source.PlayOneShot(click);
            }
        }
    }


        private void Update()
    {
        if (isPowered == true)
        {
            foreach (GameObject item in itemList)
            {

                //Checks tag and changes wire colors
                if (item.CompareTag("Wire"))
                {
                    SpriteRenderer srWire = item.GetComponent<SpriteRenderer>();
                    srWire.color = Color.yellow;
                }

                //same for the LED
                if (item.CompareTag("LED"))
                {
                    SpriteRenderer srLED = item.GetComponent<SpriteRenderer>();
                    srLED.color = Color.green;
                    Debug.Log("Light on (lever)");
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
            foreach (GameObject item in itemList)
            {
                // Changes it back
                if (item.CompareTag("Wire"))
                {
                    SpriteRenderer srWire = item.GetComponent<SpriteRenderer>();
                    srWire.color = Color.gray;
                }
                //Same for the LED
                if (item.CompareTag("LED"))
                {
                    SpriteRenderer srLED = item.GetComponent<SpriteRenderer>();
                    srLED.color = Color.white;
                    Debug.Log("Light off (lever)");

                    if (gm != null && gm.countedLEDs.Contains(item))
                    {
                        gm.lightsOn--;
                        gm.countedLEDs.Remove(item);

                        if (gm.lightsOn < 0)
                            gm.lightsOn = 0;
                    }
                }
            }
        }
    }
}
