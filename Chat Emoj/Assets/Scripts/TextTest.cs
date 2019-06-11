using UnityEngine;
using System.Collections;
using UnityEngine.UI;




public class TextTest : MonoBehaviour
{




	public Text textComp;
	 
	public int charIndex;
	//public Canvas canvas;
 
	public int Count=0;
	public Sprite Smiley_1;

	private string[] Negative;
	private string[] Positive;
	private string[] Successive;


	void Start()
	{
		textComp.text=textComp.text+"  ";
		Negative = new string[textComp.text.Length];
		Positive = new string[textComp.text.Length];
		Successive = new string[textComp.text.Length];
 	}
	public void GOO()
	{
		textComp.text=textComp.text+"  ";
		Negative = new string[textComp.text.Length];
		Positive = new string[textComp.text.Length];
		Successive = new string[textComp.text.Length];
	}


	void PrintPos ()
	{
		string text = textComp.text;
		if (charIndex >= text.Length)
			return;

		TextGenerator textGen = new TextGenerator (text.Length);
		Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
		textGen.Populate (text, textComp.GetGenerationSettings (extents));

		//int newLine = text.Substring(0, charIndex).Split('\n').Length - 1;
		int indexOfTextQuad = (charIndex * 4)   - 4;
		if (indexOfTextQuad < textGen.vertexCount)
		{
			Vector3 avgPos = (textGen.verts[indexOfTextQuad].position + 
			textGen.verts[indexOfTextQuad + 1].position + 
			textGen.verts[indexOfTextQuad + 2].position + 
			textGen.verts[indexOfTextQuad + 3].position) / 4f;

			//print (avgPos);
			PrintWorldPos (avgPos);
		}
		else 
		{
			Debug.LogError ("Out of text bound");
		}
	}




	void PrintWorldPos (Vector3 testPoint)
	{
		Vector3 worldPos = textComp.transform.TransformPoint (testPoint);
		string PX=(worldPos.x).ToString("F0");
		string PY=(worldPos.y).ToString("F0");
		worldPos.x = float.Parse (PX)-((GetComponent<Text>().fontSize*0.15f));
		worldPos.y = float.Parse (PY)-(GetComponent<Text>().fontSize*-0.35f);
		GameObject Image = new GameObject("Image");
		Image.transform.parent = textComp.transform;
		Image.AddComponent<CanvasRenderer>();
		Image i = Image.AddComponent<Image>();
		i.sprite = Smiley_1;
		i.rectTransform.sizeDelta = new Vector2(GetComponent<Text>().fontSize, GetComponent<Text>().fontSize);
		i.transform.position = new Vector3(worldPos.x,worldPos.y,0);
	}


	public string GetVersion()
	{

        TextureDictionary gett = textComp.GetComponent<TextureDictionary>();

		string txt = textComp.text;
		char[] chs = txt.ToCharArray();
		int x;
		int j;
		int k;




		for (x = 0; x <= chs.Length-1; x++) 
		{

			for (j = 0; j < gett.textureList.Capacity; j++) {
				
				string symbol = gett.textureList[j].key;
				char[] chs_symb = symbol.ToCharArray();

				if ((chs [x].ToString () == chs_symb[0].ToString ()) && (x + 1 < chs.Length)) 
				{
				  if (chs [(x + 1)].ToString () == chs_symb [1].ToString ()) 
					{

						Count = Count + 1;
						if (chs [(x - 1)].ToString () != " ") 
						{
							Negative [Count] = "" + x;
						}

						if (chs [(x + 2)].ToString () != " ") 
						{
							Positive [Count] = "" + (x + 2);
						}


						for (k = 0; k < gett.textureList.Capacity; k++) {
							string symbol2 = gett.textureList[k].key;
							char[] chs_symb2 = symbol2.ToCharArray ();
							if (chs [(x + 2)].ToString () == " " && chs [(x + 3)].ToString () == chs_symb2 [0].ToString () && chs [(x + 4)].ToString () == chs_symb2 [1].ToString ()) {
								Successive [Count] = "" + (x + 2);
							}
						}

					}

				}
			}


		
		}
		 
		BSP_Add ();
		return txt;
	}

	 

	void BSP_Add ()
	{
		int y = 0;
		for (int x = 0; x <= Negative.Length-1 ; x++) 
		{
			if (Negative[x] != null) 
			{
				textComp.text = textComp.text.Insert (int.Parse(Negative[x])+y , " ");
				y = y + 1;
			}
			if (Positive[x] != null) 
			{
				textComp.text = textComp.text.Insert (int.Parse(Positive[x])+y , " ");
				y = y + 1;
			}
			if (Successive[x] != null) 
			{
				textComp.text = textComp.text.Insert (int.Parse(Successive[x])+y , " ");
				y = y + 1;
			}

		}

		GetVersion2 ();
	}

	 






	 

	public string GetVersion2()
	{
        TextureDictionary gett = textComp.GetComponent<TextureDictionary>();

		string txt = textComp.text;
		char[] chs = txt.ToCharArray();
		int x;
		int j;
			
	      	

	    for (x = 0; x <= chs.Length-1 ; x++) 
		 {
			for (j = 0; j < gett.textureList.Capacity; j++) 
			{

				string symbol = gett.textureList [j].key;
				char[] chs_symb = symbol.ToCharArray ();

				if ((chs [x].ToString () == chs_symb[0].ToString ()) && (x + 1 < chs.Length)) 
				{

					if (chs [(x + 1)].ToString () == chs_symb[1].ToString ()) 
					{
						Smiley_1 = gett.textureList [j].texture;
						textComp.text = textComp.text.Remove (x, 2); 
						textComp.text = textComp.text.Insert (x, "  ");
						charIndex = x + 2;
						PrintPos ();
					}
				}
		    }
		    	 
		  }


			 
		 
		return txt;
	}



	void OnGUI ()
	{
		if (GUI.Button (new Rect (10, 10, 100, 80), "Test"))
		{
			GetVersion();

		}

		 
	}
}
