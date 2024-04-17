using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDDamageText : MonoBehaviour
{
    TextMeshProUGUI _text;
    Color textColor;

    public float moveSpeed;
    public float alphaSpeed;
    public float releaseTime;

    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        textColor.a = Mathf.Lerp(textColor.a, 0, Time.deltaTime * alphaSpeed);
        _text.color = textColor;
    }

    public void Initialize()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Play(Transform parent, uint damage)
    {
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        _text.text = damage.ToString();
        textColor = Color.red;
        _text.color = textColor;

        Invoke(nameof(Release), releaseTime);
    }

    void Release()
    {
        HUDManager.Instance.ReleaseDamageText(this);
    }
}
