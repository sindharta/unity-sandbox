using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;

public class AllocatedMemoryTest : MonoBehaviour {

    List<byte[]> m_list = new List<byte[]>();

//---------------------------------------------------------------------------------------------------------------------


    // Update is called once per frame
    void Update () {
        m_list.Add(new byte[1024 * 1024]);  
    }

//---------------------------------------------------------------------------------------------------------------------

}
