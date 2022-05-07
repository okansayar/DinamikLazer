using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSourse : MonoBehaviour
{
    LineRenderer lr;
    Vector3 direction; //lazerin y�n�n� belirlemek i�in
    GameObject tempReflector;

    [SerializeField] Transform laserStartPoint;

    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        direction = laserStartPoint.forward;
        lr.positionCount = 2;
        lr.SetPosition(0, laserStartPoint.position); // laserstartpointin  0�na e�it olucak.
    }

    // RaycastHit = lazer sistemi Raycast bir objeden belirledi�imiz y�nde ve uzunlukta bir ���n yayabiliriz.
    // B�ylece yayd���m�z ���n�n hangi objelere �arpt���n� tespit edebiliyoruz. Bu sistemi eskiden herkesin
    // elinden d���nmedi�i lazer sistemi gibi d���nebilirsiniz. 
   
    
    void Update()   
    {
        RaycastHit hit;
        if (Physics.Raycast(laserStartPoint.position, direction, out hit, Mathf.Infinity))  //
        {
            if (hit.collider.CompareTag("Reflector")) // lazer etiketi reflector olandan yans�ma i�in.
            {
                tempReflector = hit.collider.gameObject; // �apt���m�z objeye ���n yollamad���m�z da da eri�elim
                Vector3 temp = Vector3.Reflect(direction, hit.normal);
                hit.collider.gameObject.GetComponent<LaserReflector>().OpenRay(hit.point,temp); // �arpt���m�z obje reflector ise laserreflector eri� openrayi ger�ekle�tir.
            }
            lr.SetPosition(1, hit.point);  //�arpt���m�z yere ���k �arp�cak.
        }       
        else
        {
            if(tempReflector)
            {
               tempReflector.gameObject.GetComponent<LaserReflector>().CloseRay();
            }
            lr.SetPosition(1, direction * 200); // burda da ���k sadece 100m gider
        }
    }
}
