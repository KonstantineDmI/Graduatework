using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "Data/Story")]
[System.Serializable]
public class StoryText : ScriptableObject
{
    public List<Sentence> sentences;
    public StoryText nextText;

    [System.Serializable]
    public struct Sentence
    {
        public string text;
    }
}
