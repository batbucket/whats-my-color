using UnityEngine;
using System.Collections;

public class Helper {

	public static Color translate(Chroma chroma) {


		if (chroma.Equals(Chroma.RED)) {
			return Color.red;
		} else if (chroma.Equals(Chroma.CYAN)) {
			return Color.cyan;
		} else if (chroma.Equals(Chroma.GREEN)) {
			return Color.green;
		} else if (chroma.Equals(Chroma.BLUE)) {
			return Color.blue;
		} else if (chroma.Equals(Chroma.MAGENTA)) {
			return Color.magenta;
		} else if (chroma.Equals(Chroma.YELLOW))
			return Color.yellow;
		 else {
			throw new UnityException("What did you do?!");
		}
	}
}
