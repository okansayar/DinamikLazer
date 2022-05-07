using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflector : MonoBehaviour
{
    LineRenderer lr;

    Vector3 direction;
    Vector3 position;
    GameObject tempReflector;

    bool isOpen;

    void Start()
    {
        isOpen = false;
        lr = gameObject.GetComponent<LineRenderer>();
    }


    void Update()
    {
        if (isOpen) // açýksa bu iþlemleri yap
        {
            lr.positionCount = 2;
            lr.SetPosition(0, position);
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Reflector")) // lazer etiketi reflector olandan yansýma için.
                {
                    tempReflector = hit.collider.gameObject;
                    Vector3 temp = Vector3.Reflect(direction, hit.normal);
                    hit.collider.gameObject.GetComponent<LaserReflector>().OpenRay(hit.point, temp);
                }
                lr.SetPosition(1, hit.point);  //çarptýðýmýz yere ýþýk çarpýcak.            
            }
            else
            {
                if (tempReflector)
                {
                    tempReflector.gameObject.GetComponent<LaserReflector>().CloseRay();
                }
                lr.SetPosition(1, direction * 100);
            }
        }
        else
        {
            
            lr.positionCount = 0;
        }
    }

    public void OpenRay(Vector3 pos,Vector3 dir) // fonksiyona 2 adet parametre belirttik
    {
        isOpen = true; 
        position = pos;
        direction = dir;
    }

    public void CloseRay()
    {
        isOpen = false;
    }
}
