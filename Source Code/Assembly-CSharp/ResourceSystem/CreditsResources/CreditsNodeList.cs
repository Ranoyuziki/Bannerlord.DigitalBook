using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TaleWorlds.CompanionBook.ResourceSystem.CreditsResources
{
	// Token: 0x02000035 RID: 53
	public class CreditsNodeList
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00006A2C File Offset: 0x00004C2C
		public IReadOnlyList<CreditsNode> Nodes
		{
			get
			{
				return this._nodes;
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006A34 File Offset: 0x00004C34
		public CreditsNodeList()
		{
			this._nodes = new List<CreditsNode>();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006A48 File Offset: 0x00004C48
		public void PopulateFromFile(TextAsset file)
		{
			XmlDocument xmlDocument = Helper.LoadXmlFile(file);
			if (xmlDocument != null && xmlDocument.ChildNodes.Count == 2 && xmlDocument.ChildNodes.get_ItemOf(1).Name == "Credits")
			{
				this.PopulateFromNode(xmlDocument.ChildNodes.get_ItemOf(1));
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006A9C File Offset: 0x00004C9C
		private void PopulateFromNode(XmlNode node)
		{
			if (node.Name.ToLower() == "LoadFromFile".ToLower())
			{
				string value = node.Attributes.get_ItemOf("Name").Value;
				string text = "";
				if (node.Attributes.get_ItemOf("PlatformSpecific") != null && node.Attributes.get_ItemOf("PlatformSpecific").Value.ToLower() == "true")
				{
					if (SystemInfo.deviceType == 2)
					{
						text = "Console";
					}
					else
					{
						text = "PC";
					}
				}
				if (node.Attributes.get_ItemOf("ConsoleSpecific") != null && node.Attributes.get_ItemOf("ConsoleSpecific").Value.ToLower() == "true")
				{
					if (SystemInfo.deviceType == 3)
					{
						text = "PC";
					}
					else if (Application.platform == 25 || Application.platform == 38)
					{
						text = "PlayStation";
					}
					else
					{
						text = "XBox";
					}
				}
				TextAsset file = Addressables.LoadAssetAsync<TextAsset>("Assets/GameData/Credits/" + value + text + ".xml").WaitForCompletion();
				this.PopulateFromFile(file);
				return;
			}
			string text2 = node.GetXmlNodeAttribute("Text");
			CreditsNodeType type = CreditsNodeType.None;
			CreditsNodeType creditsNodeType;
			if (node.Name.ToLower() == "Credits".ToLower())
			{
				type = CreditsNodeType.None;
			}
			else if (Enum.TryParse<CreditsNodeType>(node.Name, ref creditsNodeType))
			{
				type = creditsNodeType;
			}
			if (!string.IsNullOrEmpty(text2) && text2.Length > 0 && text2.get_Chars(0) == '{')
			{
				int num = text2.IndexOf('}');
				if (num != -1)
				{
					text2 = text2.Substring(num + 1);
				}
			}
			CreditsNode creditsNode = new CreditsNode(text2, type);
			this._nodes.Add(creditsNode);
			foreach (object obj in node.ChildNodes)
			{
				XmlNode node2 = (XmlNode)obj;
				this.PopulateFromNode(node2);
			}
		}

		// Token: 0x04000157 RID: 343
		private List<CreditsNode> _nodes;

		// Token: 0x04000158 RID: 344
		private const char _textIDBeginSymbol = '{';

		// Token: 0x04000159 RID: 345
		private const char _textIDEndSymbol = '}';
	}
}
