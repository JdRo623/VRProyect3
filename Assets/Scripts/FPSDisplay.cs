using UnityEngine;

namespace Util
{
    public class FPSDisplay : MonoBehaviour
    {
        private float deltaTime;
        private int fps;
        private float width;
        private float height;
        private GUIStyle style = new GUIStyle();
        private Rect rect;
        private string text;

        private void Start()
        {
            width = Screen.width;
            height = Screen.height;

            rect = new Rect(0, 0, width, height*2/100);

            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = (int)(height * 2 / 100);
            style.normal.textColor = Color.white;

        }

        private void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }

        private void OnGUI()
        {
            fps = (int)(1 / deltaTime);
            text = "FPS: " + fps;
            GUI.Label(rect, text, style);
        }
    }
}
