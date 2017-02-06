using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObjects : Interactable {

    public float yValue = 1.0f;
    public bool selected = false;
    public MeshRenderer[] m_renderers;
    public Color selectionColor = new Color(0f, 1f, 1f, 1f);
    public Color PivotColor = Color.red;
	// Use this for initialization
	void Start () {
        Vector3 pos = transform.position;
        pos.y = yValue;
        transform.position = pos;
        m_renderers = GetComponentsInChildren<MeshRenderer>();
        //Debug.Log("Number of renderes  in " + transform.name + " = " + m_renderers.Length);
    }
	

    public override void Start_Interaction()
    {
        if (selected)
        {
            SetMaterialColor("_Color", Color.black);
            selected = !selected;
        }
        else
        {
            SetMaterialColor("_Color", selectionColor);
            selected = !selected;
        }
    }


    public  void Start_Interaction(bool Pivot)
    {

        if (selected)
        {
            SetMaterialColor("_Color", Color.black);
            selected = !selected;
        }
        else
        {
            Color c = (Pivot) ? PivotColor : selectionColor;
            SetMaterialColor("_Color",c);
            selected = !selected;
        }
    }

    public void End_Interaction()
    {
        
    }

    public void SetMaterialColor(string field, Color color)
    {
        
        for (int i = 0; i < m_renderers.Length; ++i)
        {
            Material[] m = m_renderers[i].materials;
            for (int j = 0; j < m.Length; ++j)
            {
                m[j].SetColor(field, color);
            }
        }
    }

}
