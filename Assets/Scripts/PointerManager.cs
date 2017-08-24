using UnityEngine;
using UnityEngine.UI;

public class PointerManager : Button
{
    private float timeToClick;
    private bool isActive;

    public void OnPointerEnter()
    {
        timeToClick = Time.realtimeSinceStartup + 1;
        isActive = true;
    }

    public void OnPointerExit()
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive && timeToClick <= Time.realtimeSinceStartup)
        {
            isActive = false;
            onClick.Invoke();
        }
    }
}
