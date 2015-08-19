using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChromaWord : MonoBehaviour {

	Text text;
	Chroma chroma;
	Word word;
	
	// Use this for initialization
	void Start () {
		this.text = GetComponent<Text>();
		randomizeChromaWord();
	}
	
	//Overloaded Start
	void Start(Chroma chroma, Word word) {
		this.chroma = chroma;
		this.word = word;
	}
	
	public Chroma getChroma() {
		return chroma;
	}
	
	public Word getWord() {
		return word;
	}
	
	public bool isChroma(Chroma chroma) {
		return this.chroma.Equals(chroma);
	}
	
	public bool isWord(Word word) {
		return this.word.Equals(word);
	}
	
	public bool isChromaAndWord(Chroma chroma, Word word) {
		return isChroma(chroma) && isWord(word);
	}
	
	public override bool Equals(object obj) {
		
		// Check for null values and compare run-time types.
		if (obj == null || GetType() != obj.GetType()) {
			return false;
		}
		
		ChromaWord cw = (ChromaWord) obj;
		
		return this.chroma.Equals(cw.getChroma()) && this.word.Equals(cw.getWord());
	}
	
	public bool isChromaWord(ChromaWord cw) {
		return isChromaAndWord(cw.getChroma(), cw.getWord());
	}

	public static Chroma generateChroma() {
		return (Chroma) Random.Range(0, System.Enum.GetNames(typeof(Chroma)).Length);
	}

	public static Word generateWord() {
		return (Word) Random.Range (0, System.Enum.GetNames(typeof(Word)).Length);
	}

	public void randomizeChromaWord(){
		this.chroma = generateChroma();
		this.word = generateWord();
	}

	// Update is called once per frame
	void Update () {
		text.color = Helper.translate(chroma);
		text.text = word.ToString();
	}
}
