using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColliderGenerator : MonoBehaviour {


    private MeshRenderer[] m_rends;
	
	void Start () {
        m_rends = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer m in m_rends)
        {
            m.gameObject.AddComponent<MeshCollider>();

        }
	}

}
