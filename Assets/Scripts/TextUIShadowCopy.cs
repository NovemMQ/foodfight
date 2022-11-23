using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUIShadowCopy : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ParentText;
    private TextMeshProUGUI ThisShadowText;
    
    // Start is called before the first frame update
    void Start()
    {
        ThisShadowText = this.GetComponent<TextMeshProUGUI>();
        ThisShadowText.text = ParentText.text;
    }

    // Update is called once per frame
    void Update()
    {
        ThisShadowText.text = ParentText.text;
    }
}
