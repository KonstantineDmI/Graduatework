using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ControllerStoryScene : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Animator animator;
    public StoryText currentScene;
    public StoryController storyControllerScript;
    public int currentTextIndex;

    private State state = State.IDLE;
    private enum State
    {
        IDLE, ANIMATE
    }

    void Start()
    {
        currentTextIndex = -1;
        storyControllerScript.PlayScene(currentScene);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (state == State.IDLE && storyControllerScript.IsCompleted())
            {
                if (storyControllerScript.IsLastSentence())
                {
                    {
                        PlayScene(currentScene.nextText);
                    }
                }
                else
                {
                    storyControllerScript.PlayNextSentence();
                }
            }
        }
    }

    public void ChangeBackgroundImage()
    {
        background.sprite = backgrounds[currentTextIndex];
    }

    private void PlayScene(StoryText scene)
    {
        StartCoroutine(SwitchScene(scene));
    }
    private IEnumerator SwitchScene(StoryText scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        storyControllerScript.Hide();
        yield return new WaitForSeconds(0.4f);
        ChangeBackgroundImage();
        yield return new WaitForSeconds(0.4f);
        storyControllerScript.ClearText();
        storyControllerScript.Show();
        yield return new WaitForSeconds(0.6f);
        storyControllerScript.PlayScene(scene);
        state = State.IDLE;
    }
}
