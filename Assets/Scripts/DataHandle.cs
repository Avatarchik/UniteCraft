using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataHandle : MonoBehaviour
{

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (Directory.Exists(@"/Datas/"))
        {
            
        }
        else
        {
            Directory.CreateDirectory(@"/Datas/");
        }
    }
}