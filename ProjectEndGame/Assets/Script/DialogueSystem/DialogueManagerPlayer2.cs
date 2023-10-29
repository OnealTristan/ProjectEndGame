using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerPlayer2 : MonoBehaviour
{

	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	private Queue<string> sentences;

	// Start is called before the first frame update
	void Start()
	{
		sentences = new Queue<string>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			DisplayNextSentence();
		}
	}

	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		Debug.Log("Starting conversation with " + dialogue.name);
		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{

		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
		Debug.Log(sentence);
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		Debug.Log("End of conversation.");
	}

}