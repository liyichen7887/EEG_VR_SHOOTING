using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionColorTest : MonoBehaviour {
    public MeshRenderer[] m_renderers;
    public Color c = Color.cyan;
    // Use this for initialization
    void Start () {
        m_renderers = GetComponentsInChildren<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < m_renderers.Length; ++i)
        {
            m_renderers[i].enabled = false;
            m_renderers[i].enabled = true;
            Material[] m = m_renderers[i].materials;
            for (int j = 0; j < m.Length; ++j)
            {
                m[j].SetColor("_EmissionColor", c);
            }
        }
    }
}
