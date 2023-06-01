using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshPro textMesh;
    public Color TextColor;
    public float DamageAmount;
    public string Text_Input;
    public bool isText;

    private float disappearTime = 0.5f, FadeOutSpeed = 3f, MoveYSpeed = 3f;

    public void TextColorInit(Color color_)
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        textMesh.color = color_;
    }
    private void Start()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        TextColor = textMesh.color;
        if (!isText)
        {
            DamageOutput(DamageAmount);
        }
        else
        {
            TextOutput(Text_Input);
        }
    }
    public void DamageOutput(float Amount)
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        TextColor = textMesh.color;

        textMesh.text = "-" + Amount.ToString();
    }
    public void TextOutput(string Text_)
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        TextColor = textMesh.color;

        textMesh.text = Text_;
    }
    private void LateUpdate()
    {
        transform.position += new Vector3(0, MoveYSpeed * Time.deltaTime, 0);

        disappearTime -= Time.deltaTime;
        if (disappearTime <= 0)
        {
            TextColor.a -= FadeOutSpeed * Time.deltaTime;
            textMesh.color = TextColor;
            if (TextColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        
    }
}
