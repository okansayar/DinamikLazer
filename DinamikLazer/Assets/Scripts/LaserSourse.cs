using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSourse : MonoBehaviour
{
    LineRenderer lr;
    Vector3 direction; //lazerin yönünü belirlemek için
    GameObject tempReflector;

    [SerializeField] Transform laserStartPoint;

    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        direction = laserStartPoint.forward;
        lr.positionCount = 2;
        lr.SetPosition(0, laserStartPoint.position); // laserstartpointin  0ýna eþit olucak.
    }

    // RaycastHit = lazer sistemi Raycast bir objeden belirlediðimiz yönde ve uzunlukta bir ýþýn yayabiliriz.
    // Böylece yaydýðýmýz ýþýnýn hangi objelere çarptýðýný tespit edebiliyoruz. Bu sistemi eskiden herkesin
    // elinden düþünmediði lazer sistemi gibi düþünebilirsiniz. 
   
    
    void Update()   
    {
        RaycastHit hit;
        if (Physics.Raycast(laserStartPoint.position, direction, out hit, Mathf.Infinity))  //
        {
            if (hit.collider.CompareTag("Reflector")) // lazer etiketi reflector olandan yansýma için.
            {
                tempReflector = hit.collider.gameObject; // çaptýðýmýz objeye ýþýn yollamadýðýmýz da da eriþelim
                Vector3 temp = Vector3.Reflect(direction, hit.normal);
                hit.collider.gameObject.GetComponent<LaserReflector>().OpenRay(hit.point,temp); // çarptýðýmýz obje reflector ise laserreflector eriþ openrayi gerçekleþtir.
            }
            lr.SetPosition(1, hit.point);  //çarptýðýmýz yere ýþýk çarpýcak.
        }       
        else
        {
            if(tempReflector)
            {
               tempReflector.gameObject.GetComponent<LaserReflector>().CloseRay();
            }
            lr.SetPosition(1, direction * 200); // burda da ýþýk sadece 100m gider
        }
    }
}
