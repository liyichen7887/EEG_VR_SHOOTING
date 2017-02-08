using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObjects : Interactable {
    public string Objectname;
    public bool alignWithWall = false;
    public float yValue = 1.0f;
    public Color selectionColor = new Color(0f, 1f, 1f, 1f);
    public Color PivotColor = Color.red;
    [HideInInspector]
    public bool selected = false;
    [HideInInspector]
    public Collider col;
    private MeshRenderer[] m_renderers;

    // Use this for initialization
    void Start () {
        Vector3 pos = transform.position;
        pos.y = yValue;
        transform.position = pos;
        m_renderers = GetComponentsInChildren<MeshRenderer>();
        col = GetComponent<Collider>();
        ObjectSpawner.Instance.spawnedItems.Add(this.gameObject);
    }
	

    public override void Start_Interaction()
    {
        if (selected)
        {
            SetMaterialColor("_Color", Color.black);
            selected = !selected;
            col.enabled = true;

        }
        else
        {
            SetMaterialColor("_Color", selectionColor);
            selected = !selected;
            col.enabled = false;
        }
    }


    public void Start_Interaction(bool Pivot)
    {

        if (selected)
        {
            SetMaterialColor("_Color", Color.black);
            selected = !selected;
            col.enabled = true;
        }
        else
        {
            Color c = (Pivot) ? PivotColor : selectionColor;
            SetMaterialColor("_Color",c);
            selected = !selected;
          //  col.enabled = false;
        }
    }

    private void SetColActive(bool active)
    {
        col.enabled = active;
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
