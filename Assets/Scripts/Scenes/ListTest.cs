#pragma warning disable 219

using UnityEngine;
using System.Collections.Generic;
using System;

public class ListTest : MonoBehaviour 
{

//---------------------------------------------------------------------------------------------------------------------

    void Start () {
        TestGetRange();	
    }

    
//---------------------------------------------------------------------------------------------------------------------
    
    void TestGetRange() {
        List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };

        try {
            List<int> copiedList = list.GetRange(0, 20);
        } catch (ArgumentException e)
        {
            Debug.Log("GetRange() must not exceed the number of elements in the source. " + e.ToString());
        }
    }

}
