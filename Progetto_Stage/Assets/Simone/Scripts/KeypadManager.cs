using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadManager : MonoBehaviour
{
    TMP_Text m_Text;
    [SerializeField] TMP_Text m_InputField;
    private void Awake()
    {
        m_Text= GetComponentInChildren<TMP_Text>();
        if (m_Text.gameObject.name != "DEL" && m_Text.gameObject.name != "ENT")
        {
            m_Text.fontSize = 0.8f;
        }
        m_Text.text = m_Text.gameObject.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintChar()
    {
        if (m_InputField != null)
        {
            m_InputField.text += m_Text.text;
        }
    }

    public void PrintSpace()
    {
        if(m_InputField != null)
        {
            if (m_InputField.text.Length > 0)
            {
                m_InputField.text+=" ";
            }
        }
    }

    public void DeleteChar()
    {
        if (m_InputField.text.Length > 0)
        {
            m_InputField.text=m_InputField.text.Substring(0,m_InputField.text.Length-1);
        }
    }

    public void Enter()
    {
        if(m_InputField != null && m_InputField.text!="")
        {
            APIManager.instance.GetCountryData(m_InputField.text);
        }
    }
}
