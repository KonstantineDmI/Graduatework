using System.Collections;
using UnityEngine;
using TMPro;
public class StoryController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private ControllerStoryScene controller;
    [SerializeField] private StoryText currentText;
    [SerializeField] private float textSpeed;

    public int sentenceIndex = -1;
    private State state = State.COMPLETED;
    private Animator animator;
    private bool isHidden = false;

    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Hide()
    {
        if(!isHidden)
        {
            animator.SetTrigger("Hide");
            isHidden = true;
        }
    }

    public void Show()
    {
        animator.SetTrigger("Show");
        isHidden = false;
    }

    public void ClearText()
    {
        storyText.text = "";
    }
    public void PlayScene(StoryText text)
    {
        currentText = text;
        sentenceIndex = -1;
        controller.currentTextIndex += 1;
        controller.ChangeBackgroundImage();
        PlayNextSentence();
    }

    public void PlayNextSentence()
    {
        StartCoroutine(TypeText(currentText.sentences[++sentenceIndex].text));
    }
    
    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentText.sentences.Count;
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    private IEnumerator TypeText(string text)
    {
        storyText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            storyText.text += text[wordIndex];
            yield return new WaitForSeconds(textSpeed);
            if(++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }
}
