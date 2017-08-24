using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectTimeDeactivator : MonoBehaviour
{
    public UnityEvent DeactivatedDelegate;

    [SerializeField]
    private float timeToDeactivate;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private bool deactivateOnStart;

    public void Activate()
    {
        content.SetActive(true);
        StartCoroutine(StartTimer());
    }

    private void Start()
    {
        if(deactivateOnStart)
            StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSecondsRealtime(timeToDeactivate);
        DeactivatedDelegate.Invoke();
        content.SetActive(false);
    }
}
