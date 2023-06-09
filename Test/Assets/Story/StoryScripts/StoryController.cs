using System.Collections;
using UnityEngine;
using TMPro;
using Crosstales.RTVoice;
using UnityEngine.Rendering;
using Crosstales.RTVoice.Model;
using Crosstales.RTVoice.Tool;
using Crosstales.RTVoice.Demo;
using UnityEditor;
using UnityEngine.Diagnostics;
using Crosstales;
using System.Linq;

public class StoryController : MonoBehaviour
{
    [Header ("Story texts")]
    [TextArea(1, 10)]
    [SerializeField] private string[] text;

    [Header ("Story")]
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private ControllerStoryScene controller;
    [SerializeField] private StoryText currentText;

    [Header ("Text Speed")]
    [SerializeField] private float textSpeed;

    [Header ("Speaker")]
    [SerializeField] private SpeechText speakerScript;

    [Header ("Audio")]
    [SerializeField] private AudioSource audioSource;
    
    private int _sentenceIndex = -1;
    private int _textIndex = -1;

    private bool _isHidden = false;

    private State state = State.COMPLETED;
    private Animator _animator;

    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Speak(int index)
    {
        speakerScript.text = text[index];
        speakerScript.Speak();
    }

    public void Hide()
    {
        if(!_isHidden)
        {
            _animator.SetTrigger("Hide");
            _isHidden = true;
        }
    }

    public void Show()
    {
        _animator.SetTrigger("Show");
        _isHidden = false;
    }

    public void ClearText()
    {
        storyText.text = "";
    }
    public void PlayScene(StoryText text)
    {
        currentText = text;
        _sentenceIndex = -1;
        controller.currentTextIndex += 1;
        _textIndex += 1;
        controller.ChangeBackgroundImage();
        Speak(_textIndex);
        PlayNextSentence();
        StartCoroutine(TypeSound());
    }

    public void PlayNextSentence()
    {
        StartCoroutine(TypeText(currentText.sentences[++_sentenceIndex].text));
    }
    
    public bool IsLastSentence()
    {
        return _sentenceIndex + 1 == currentText.sentences.Count;
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    private IEnumerator TypeText(string text)
    {
        yield return new WaitForSeconds(0.1f);
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
                audioSource.Stop();
                break;
            }
        }
    }

    private IEnumerator TypeSound()
    {
        yield return new WaitForSeconds(0.1f);
        audioSource.Play();
    }
}
