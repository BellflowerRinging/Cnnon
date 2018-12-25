using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugLabel : MonoBehaviour
{

    Dictionary<string, string> _dictionary = new Dictionary<string, string>();
    public GameObject _DebugWindow;
    public Text _DebugText;

    public int Decimals = 2;

    // Use this for initialization
    void Start()
    {

    }

    float fps=0f;

    void LateUpdate()
    {
        //Fps
        float interp = Time.deltaTime / (0.5f + Time.deltaTime);
        float currentFPS = 1.0f / Time.deltaTime;
        fps = Mathf.Lerp(fps, currentFPS, interp);
        //textField ="FPS:"+Mathf.RoundToInt(fps);
        
        string str = "";

        str += "FPS:" + Mathf.RoundToInt(fps) + "\n";

        foreach (var item in _dictionary)
        {
            str += item.Key + "=" + item.Value + '\n';
        }

        _DebugText.text = str;

    }

    public void AddItem(string Key, double Value)
    {
        AddItem(Key, Math.Round(Value, Decimals) + "");
    }

    public void AddItem(string Key, bool Value)
    {
        AddItem(Key, Value + "");
    }

    public void AddItem(string Key, Vector2 Value)
    {

        AddItem(Key, "(" + Math.Round(Value.x, Decimals) + "," + Math.Round(Value.y, Decimals) + ")");
    }

    public void AddItem(string Key, Vector3 Value)
    {
        AddItem(Key, "(" + Math.Round(Value.x, Decimals) + "," + Math.Round(Value.y, Decimals) + "," + Math.Round(Value.z, Decimals) + ")");
    }

    public void AddItem(string Key, Vector4 Value)
    {
        AddItem(Key, "(" + Math.Round(Value.x, Decimals) + "," + Math.Round(Value.y, Decimals) + "," + Math.Round(Value.z, Decimals) + "," + Math.Round(Value.w, Decimals) + ")");
    }

    public void AddItem(string Key, Quaternion Value)
    {
        AddItem(Key, "(" + Math.Round(Value.x, Decimals) + "," + Math.Round(Value.y, Decimals) + "," + Math.Round(Value.z, Decimals) + "," + Math.Round(Value.w, Decimals) + ")");
    }

    public void AddItem(string Key, string Value)
    {
        if (!_dictionary.ContainsKey(Key))
        {
            _dictionary.Add(Key, Value);
        }
        else
        {
            _dictionary[Key] = Value;
        }
    }

    public void ToggleWindow()
    {
        _DebugWindow.SetActive(!_DebugWindow.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToggleScenes()
    {
        if (SceneManager.GetActiveScene().name=="scenes_Day")
        {
            SceneManager.LoadScene("Arena");
            SceneManager.UnloadSceneAsync("scenes_Day");
        }
        else
        {
            SceneManager.LoadScene("scenes_Day");
            SceneManager.UnloadSceneAsync("Arena");
        }


    }


    private void OnGUI()
    {

    }
}
