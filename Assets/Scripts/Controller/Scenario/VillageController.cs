using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VillageController : MonoBehaviour
{
    private Dictionary<GameObject, int> colliderDictionary = new Dictionary<GameObject, int>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!colliderDictionary.ContainsKey(collision.gameObject))
        {
            colliderDictionary.Add(collision.gameObject, collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder);
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
            Debug.Log(collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder);
        }

        Color oldColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliderDictionary.ContainsKey(collision.gameObject))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = colliderDictionary[collision.gameObject];
            colliderDictionary.Remove(collision.gameObject);
            Debug.Log(collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder);
        }

        Color oldColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 1.0f);
    }
}
