using UnityEditor.Rendering;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField]
    private float spinningSpeed;

    private void Update()
    {
        transform.Rotate(0, 0, spinningSpeed * Time.deltaTime);
    }
}
