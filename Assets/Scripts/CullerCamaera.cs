using UnityEngine;

public class CullerCamaera : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[8] = 150;
        camera.layerCullDistances = distances;
    }
}
