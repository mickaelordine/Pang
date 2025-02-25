using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleRegistrationToManager : MonoBehaviour, IBubble
{
    IEnumerator registerTimer()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.AddBubble(gameObject);
    }
    private void Start()
    {
        //start the timer 
        StartCoroutine(registerTimer());
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveBubble(gameObject);
    }
}
