using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public Transform camera;
    public void Start()
    {
        camera = GameObject.Find("Main Camera").transform;

    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }

}
