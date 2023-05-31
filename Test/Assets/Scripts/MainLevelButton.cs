using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLevelButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(NextLevel);
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
