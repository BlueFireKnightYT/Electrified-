using UnityEngine;

public class capacitorPower : MonoBehaviour
{
    [SerializeField] private GameObject[] itemList;
    public bool isPowered;

    private void Update()
    {
        if(isPowered == true)
        { 
            foreach(GameObject item in itemList)
            {
                if(item.CompareTag("Wire"))
                {
                    SpriteRenderer srWire = item.GetComponent<SpriteRenderer>();
                    srWire.color = Color.yellow;
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
            foreach (GameObject item in itemList)
            {
                if (item.CompareTag("Wire"))
                {
                    SpriteRenderer srWire = item.GetComponent<SpriteRenderer>();
                    srWire.color = Color.gray;
                }
                if (item.CompareTag("LED"))
                {
                    SpriteRenderer srLED = item.GetComponent<SpriteRenderer>();
                    srLED.color = Color.white;
                }
            }
        }

    }
}
