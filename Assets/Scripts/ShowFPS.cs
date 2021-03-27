using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    private float f_LastInterval;
    private int i_Frames = 0;
    private float f_Fps;
    private GameObject fpstext;
    public Font fps_font;
    public Texture QS_texture;
    public float scare;
    private float size;

    public void Awake()
    {
        Application.targetFrameRate = -1;
    }

    float deltaTime;

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        size = Mathf.Sqrt(Screen.height * Screen.width / scare);
        var style = new GUIStyle
        {
            alignment = TextAnchor.UpperLeft,
            fontSize = Screen.height * 5 / 100,
            font = fps_font,
            normal = {textColor = Color.black}
        };
        var text = $"{deltaTime * 1000.0f:0.0} ms ({1.0f / deltaTime:0.} fps)";
        // ReSharper disable once PossibleLossOfFraction
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height * 2 / 100), text, style);
        GUI.Label(new Rect(Screen.height - Screen.height * 2 / 100, 0, Screen.width, Screen.height * 2 / 100),
            "Type \'Q + ESC\' to quit game.", style);
        // ReSharper disable once PossibleLossOfFraction
        GUI.DrawTexture(new Rect(Screen.width / 2 - size / 2, Screen.height / 2 - size / 2, size, size), QS_texture);
    }
}