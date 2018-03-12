using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VillageController : MonoBehaviour
{
    private Dictionary<GameObject, int> colliderDictionary = new Dictionary<GameObject, int>();

    private void OnTriggerStay2D(Collider2D detectedObject)
    {
        if (TagUtils.IsPlayer(detectedObject.transform))
        {
            if (!colliderDictionary.ContainsKey(detectedObject.gameObject))
            {
                colliderDictionary.Add(detectedObject.gameObject, detectedObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder);
                detectedObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
                Debug.Log(detectedObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder);
            }

            Color oldColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D detectedObject)
    {
        if (TagUtils.IsPlayer(detectedObject.transform))
        {
            if (colliderDictionary.ContainsKey(detectedObject.gameObject))
            {
                detectedObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder = colliderDictionary[detectedObject.gameObject];
                colliderDictionary.Remove(detectedObject.gameObject);
                Debug.Log(detectedObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder);
            }

            Color oldColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 1.0f);
        }
    }
}
