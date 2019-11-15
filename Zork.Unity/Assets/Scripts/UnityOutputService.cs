using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zork.Common;
using TMPro;
public class UnityOutputService : MonoBehaviour, IOutputService
{
    [SerializeField]
    private TextMeshProUGUI TextLinePrefab;
    [SerializeField]
    private Transform Container;
    [SerializeField]
    private int MaxLines = 60;
    
    private List<GameObject> Textlines = new List<GameObject>();
    public UnityOutputService()
    {
        Textlines = new List<GameObject>();
    }
    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    public void Write(string value)
    {
        throw new System.NotImplementedException();
    }

    public void Write(object value)
    {
        throw new System.NotImplementedException();
    }

    public void WriteLine(string value)
    {
        if (Textlines.Count > MaxLines)
        {
            Destroy(Textlines[0]);
            Textlines.RemoveAt(0);
        }
        var textLine = Instantiate(TextLinePrefab);
        textLine.transform.SetParent(Container);
        textLine.text = value;
        Textlines.Add(textLine.gameObject);
    }

    public void WriteLine(object value)
    {
        if (Textlines.Count > MaxLines)
        {
            Destroy(Textlines[0]);
            Textlines.RemoveAt(0);
        }
        var textLine = Instantiate(TextLinePrefab);
        textLine.transform.SetParent(Container);
        textLine.text = value.ToString();
        Textlines.Add(textLine.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
