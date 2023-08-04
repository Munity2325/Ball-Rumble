using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitInfo {
    public int id;
    public string tag;
    public Vector3 position;
    //public Vector3 size;    // TODO: добавить размеры объектов

    public UnitInfo(GameObject unit) {
        id = unit.GetInstanceID();
        unit.name = id.ToString();
        tag = unit.tag;
        position = unit.transform.position;
    }

    public void refresh() {
        GameObject obj = GameObject.Find(id.ToString());
        position = obj.transform.position;
    }
}


[Serializable]
public class UnitInfoCollection {
    public UnitInfo[] data;

    public UnitInfoCollection(uint size) {
        data = new UnitInfo[size];
    }

    public void refresh() {
        foreach (UnitInfo unit in data) {
            unit.refresh();
        }
    }
}
