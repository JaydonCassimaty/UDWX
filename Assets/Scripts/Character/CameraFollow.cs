using UnityEngine;
using System.Collections;

 public class CameraFollow : MonoBehaviour
 {
     public Transform target;
     private float camHeight = 9f;
     private float camCentre = 5f;

     // Update is called once per frame
     void Update()
     {
       camHeight -= Input.GetAxis("Mouse ScrollWheel");
       camCentre -= Input.GetAxis("Mouse ScrollWheel");
       camHeight = Mathf.Clamp(camHeight, 3f, 9f);
       camCentre = Mathf.Clamp(camCentre, 1.5f, 5f);

       if(target != null)
       {
         transform.position = new Vector3(target.position.x, camHeight, target.position.z - camCentre);
       }
     }

     public void setTarget(Transform target2)
     {
         target = target2;
     }
 }
