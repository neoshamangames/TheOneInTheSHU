using UnityEngine;
using System.Collections;

public class FadingTexture : MonoBehaviour 
{
    // Make sure to add a blank white texture on 'texture' for the 'color' to work properly.
    [SerializeField]
    private Texture texture;
    [SerializeField]
    private Color color;
    [SerializeField]
    private float duration = 3;
    [SerializeField]
    private bool beginOnStart;

    private float alpha = 1;
    private float timeBegin;
    private System.Action onEndFade = null;

    public void BeginFade(float duration, System.Action onEndFade = null)
    {
        this.onEndFade = onEndFade;
        this.duration = duration;
        this.enabled = true;
        color.a = 1f;
        timeBegin = Time.time;
    }

    void Start()
    {
        if (beginOnStart)
            BeginFade(duration , null);
        else
            this.enabled = false;
    }

    void Update()
    {
        color.a = 1f - ((Time.time - timeBegin) / duration);

        if (color.a < 0f || Mathf.Approximately(color.a, 0f))
        {
            color.a = 0f;
            
            if (onEndFade != null)
                onEndFade();

            this.enabled = false;
        }
    }

    void OnGUI()
    {
        if (texture != null)
        {
            Color prevColor = GUI.color;
            GUI.color = color;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

            GUI.color = prevColor;
        }
    }

}
