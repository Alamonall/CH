using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {
	public TextAsset xmla;
	public Text biography;
	public Text data;

	public string characterSelected(string name, string type)
	{
		UIManager._instanceUIM.selectedCharacter = name;
		XmlDocument xmld = new XmlDocument ();
		xmld.LoadXml (xmla.text);
		XmlNodeList charactersList = xmld.GetElementsByTagName ("character");
		foreach (XmlNode charInfo in charactersList) {
			if(charInfo.Name == "character")
			{
				if (charInfo.Attributes ["name"].Value == name) 
				{
					return charInfo.SelectSingleNode (type).InnerText;
				}
			}
		}
		return "None";
	}

	public void GetCharBio(string name){
		biography.text = characterSelected (name, "bio");	
		data.text = characterSelected (name, "data");	
	}

}
