using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class BoxPool  {

    static BoxPool mPool = null;
    public static BoxPool instance
    {
        get
        {
            if (mPool == null)
            {
                mPool = new BoxPool();
            }
            return mPool;
        }
    }

    List<GameObject> objList = new List<GameObject>();
    List<GameObject> useList = new List<GameObject>();
    public GameObject Get() {
        GameObject box;
        if (objList.Count <= 0)
        {
            box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        else
        {
            box = objList[0];
            objList.RemoveAt(0);
            if (box == null)
            {
                return Get();
            }
        }
        box.SetActive(true);
        useList.Add(box);
        return box;
    }

    public void Put(GameObject obj)
    {
        useList.Remove(obj);
        objList.Add(obj);
        obj.SetActive(false);
    }
}
