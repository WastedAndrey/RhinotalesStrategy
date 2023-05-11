using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private float _cameraSpeed = 5;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 position = this.transform.position;
            position.x += Input.GetAxis("Horizontal") * Time.deltaTime * _cameraSpeed;
            position.z += Input.GetAxis("Vertical") * Time.deltaTime * _cameraSpeed;
            
            if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E) == false) 
                position.y += Time.deltaTime * _cameraSpeed;
            if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Q) == false)
                position.y -= Time.deltaTime * _cameraSpeed;

            this.transform.position = position;
        }
    }
}

