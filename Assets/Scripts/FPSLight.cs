using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSLight : MonoBehaviour
{
    Text m_editableText;
    // Start is called before the first frame update
    void Start()
    {
        m_editableText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_editableText.text = (1f / Time.deltaTime).ToString();
    }
}
