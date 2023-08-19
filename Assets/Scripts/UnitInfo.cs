using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitInfo {
    public int id;
    public string tag;
    public Vector3 position;
    public UnitSize size;
    //public Vector3 size;    // TODO: �������� ������� ��������

    public UnitInfo(GameObject unit) {
        id = unit.GetInstanceID();
        unit.name = id.ToString();
        tag = unit.tag;
        position = unit.transform.position;
        // size.height = unit.GetComponent<CapsuleCollider>().height;
        // size.radius = unit.GetComponent<CapsuleCollider>().radius;
    }

    public void refresh() {
        GameObject obj = GameObject.Find(id.ToString());
        position = obj.transform.position;
        // size.height = unit.GetComponent<CapsuleCollider>().height;
        // size.radius = unit.GetComponent<CapsuleCollider>().radius;
    }
}


[Serializable]
public class UnitInfoCollection {
    public UnitInfo[] data;
}

[Serializable]
public class UnitSize
{
    public float height;
    public float radius;
}