using System;
using UnityEngine;
using static UnityEngine.Screen;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour
{
    private readonly GUIStyle _style = new GUIStyle();
    private RaycastHit _hit;
    private Ray _ray;
    private Camera _cam;
    public Font font;
    private string _choosed;
    private GameObject _cap;
    private GameObject _blockoverlay;
    private float _stime;

    public Texture[] icons = new Texture[3];

    string ToUpper(string str)
    {
        return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str);
    }

    public void Awake()
    {
        _stime = Time.time;
    }

    // Start is called before the first frame update
    public void Start()
    {
        _style.alignment = TextAnchor.UpperLeft;
        _style.fontSize = 16;
        _style.font = font;
        _style.normal.textColor = Color.black;
        _cap = GameObject.Find("Steve");
        _blockoverlay = GameObject.Find("BlockOverLay");
        _blockoverlay.GetComponent<Renderer>().enabled = false;
        transform.position = new Vector3(5, 6, 5);
        _cam = Camera.main;
    }

    // Update is called once per frame
    public void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (transform.position.y <= -7)
        {
            transform.position = new Vector3(5, 6, 5);
        }

        Actions();
        if (Time.time - _stime >= 30)
        {
            GC.Collect();
            _stime = Time.time;
        }

        Shooter();
    }

    private void Shooter()
    {
        // spawn a camera lines
        _ray = new Ray();
        if (Camera.main is { }) _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit) && _hit.transform.CompareTag("Block") &&
            Vector3.Distance(_hit.transform.position, _cam.transform.position) <= 5.5f)
        {
            _blockoverlay.transform.position = _hit.transform.position;
            _blockoverlay.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            _blockoverlay.GetComponent<Renderer>().enabled = false;
        }

        // select block
        if (Input.GetMouseButtonDown(2) && Physics.Raycast(_ray, out _hit) &&
            Vector3.Distance(_hit.transform.position, _cam.transform.position) <= 5f)
        {
            _choosed = _hit.transform.name;
        }

        // put block
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(_ray, out _hit) && _hit.transform.CompareTag("Block") &&
                Vector3.Distance(_cap.transform.position, _hit.transform.position) <= 5.5f &&
                Vector3.Distance(_cap.transform.position, _hit.transform.position) >= 1.5f &&
                Vector3.Distance(_cam.transform.position, _hit.transform.position) >= 0)
            {
                Vector3 finallyTemp = _hit.transform.position + _hit.normal.normalized;
                if (_choosed != null)
                {
                    Transform putBlock = Instantiate(GameObject.Find(_choosed).transform);
                    putBlock.name = _choosed.Split(new[] {'('}, 2)[0] + finallyTemp;
                    putBlock.parent = GameObject.Find(ToUpper(_choosed.Split(new[] {'('}, 2)[0]) + "s").transform;
                    putBlock.position = finallyTemp;
                }
                else
                {
                    Transform putBlock = Instantiate(_hit.transform);
                    putBlock.name = _hit.transform.name.Split(new[] {'('}, 2)[0] + finallyTemp;
                    putBlock.parent = GameObject.Find(ToUpper(_hit.transform.name.Split(new[] {'('}, 2)[0]) + "s")
                        .transform;
                    putBlock.position = finallyTemp;
                }
            }
        }

        // destroy block
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_ray, out _hit) && _hit.transform.CompareTag("Block") &&
                Vector3.Distance(_hit.transform.position, _cam.transform.position) <= 5f)
            {
                Destroy(GameObject.Find(_hit.transform.name));
            }
        }
    }

    // show icon
    public void OnGUI()
    {
        try
        {
            switch (_choosed.Split(new[] {'('}, 2)[0])
            {
                case "grass":
                    GUI.DrawTexture(new Rect(width - 50, height / 2 - 50, 50, 50), icons[0]);
                    // ReSharper disable once PossibleLossOfFraction
                    GUI.Label(new Rect(width - 60, height / 2, 50, 10), "grass", _style);
                    break;
                case "dirt":
                    GUI.DrawTexture(new Rect(width - 50, height / 2 - 50, 50, 50), icons[1]);
                    // ReSharper disable once PossibleLossOfFraction
                    GUI.Label(new Rect(width - 60, height / 2, 50, 10), "dirt", _style);
                    break;
                case "bedrock":
                    GUI.DrawTexture(new Rect(width - 50, height / 2 - 50, 50, 50), icons[2]);
                    // ReSharper disable once PossibleLossOfFraction
                    GUI.Label(new Rect(width - 60, height / 2, 50, 10), "bedrock", _style);
                    break;
            }
        }
        catch
        {
            // ignored
        }
    }

    private static void Actions()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("StartUI");
            }
        }
    }
}