using UnityEngine;

public class CullerCamaera : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 150;

    private void Start()
    {
        Camera camera = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[8] = maxDistance;
        camera.layerCullDistances = distances;
    }
}
