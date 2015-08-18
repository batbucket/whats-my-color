using UnityEngine;
using System.Collections;

public class Helper {

	static Color orange = new Color(255, 102, 0);

	public static Color translate(Chroma chroma) {
		if (chroma.Equals(ColorWord.Chroma.RED)) {
			return Color.red;
		} else if (chroma.Equals(ColorWord.Chroma.ORANGE)) {
			return orange;
		} else if (chroma.Equals(ColorWord.Chroma.GREEN)) {
			return Color.green;
		} else if (chroma.Equals(ColorWord.Chroma.BLUE)) {
			return Color.blue;
		} else if (chroma.Equals(ColorWord.Chroma.PURPLE)) {
			return Color.magenta;
		} else {
			return Color.yellow;
		}
	}
	
}
