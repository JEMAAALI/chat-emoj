using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextureDictionary : MonoBehaviour {

	[System.Serializable] 
	public class TextureContainer
	{
		public string key;
		public Sprite texture;
	}
	[SerializeField]
	public List<TextureContainer> textureList;

	public Dictionary<string, Sprite> textures;



	public void Adding()
	{   
		TextTest  gett = GetComponent<TextTest>();

		GameObject.Find("TextContainer").GetComponent<Text>().text+="\n"+GameObject.Find("InputField").GetComponent<InputField>().text;
		gett.GOO();
		gett.GetVersion();
	}

}