/*
 *   This file was generated by a tool.
 *   Do not edit it, otherwise the changes will be overwritten.
 */

using System;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using WJExcelDataClass;

[Serializable]
public class DataManager //: SingletonTemplate<DataManager>
{
	public WJExcelDataClass.sdftt p_sdftt;
	public WJExcelDataClass.mmm p_mmm;
	public WJExcelDataClass.adtttt p_adtttt;
	public WJExcelDataClass.ScenesJump p_ScenesJump;
	public WJExcelDataClass.Maps1 p_Maps1;
	public WJExcelDataClass.Dialog1 p_Dialog1;
	public WJExcelDataClass.Characters p_Characters;
	public WJExcelDataClass.Props p_Props;
	public WJExcelDataClass.Crops p_Crops;

	public sdfttItem GetSdfttItemByID(Int32 id)
	{
		sdfttItem t = null;
		p_sdftt.Dict.TryGetValue(id, out t);
		if (t == null) Debug.LogWarning("can't find the id " + id + " in sdftt");
		return t;
	}

	public mmmItem GetMmmItemByID(Int32 id)
	{
		mmmItem t = null;
		p_mmm.Dict.TryGetValue(id, out t);
		if (t == null) Debug.LogWarning("can't find the id " + id + " in mmm");
		return t;
	}

	public adttttItem GetAdttttItemByID(Int32 id)
	{
		adttttItem t = null;
		p_adtttt.Dict.TryGetValue(id, out t);
		if (t == null) Debug.LogWarning("can't find the id " + id + " in adtttt");
		return t;
	}

	public ScenesJumpItem GetScenesJumpItemByID(Int32 id)
	{
		ScenesJumpItem t = null;
		p_ScenesJump.Dict.TryGetValue(id, out t);
		if (t == null) Debug.LogWarning("can't find the id " + id + " in ScenesJump");
		return t;
	}

	public Maps1Item GetMaps1ItemByID(Int32 id)
	{
		Maps1Item t = null;
		p_Maps1.Dict.TryGetValue(id, out t);
		if (t == null) Debug.LogWarning("can't find the id " + id + " in Maps1");
		return t;
	}

	public Dialog1Item GetDialog1ItemByID(Int32 id)
	{
		Dialog1Item t = null;
		p_Dialog1.Dict.TryGetValue(id, out t);
		if (t == null) Debug.LogWarning("can't find the id " + id + " in Dialog1");
		return t;
	}

	public CharactersItem GetCharactersItemByID(Int32 id)
	{
		CharactersItem t = null;
		p_Characters.Dict.TryGetValue(id, out t);
		if (t == null) Debug.LogWarning("can't find the id " + id + " in Characters");
		return t;
	}

	public PropsItem GetPropsItemByID(Int32 id)
	{
		PropsItem t = null;
		p_Props.Dict.TryGetValue(id, out t);
		if (t == null) Debug.LogWarning("can't find the id " + id + " in Props");
		return t;
	}

	public CropsItem GetCropsItemByID(Int32 id)
	{
		CropsItem t = null;
		p_Crops.Dict.TryGetValue(id, out t);
		if (t == null) Debug.LogWarning("can't find the id " + id + " in Crops");
		return t;
	}

	public void LoadAll()
	{
		p_sdftt = Load("sdftt") as WJExcelDataClass.sdftt;
		p_mmm = Load("mmm") as WJExcelDataClass.mmm;
		p_adtttt = Load("adtttt") as WJExcelDataClass.adtttt;
		p_ScenesJump = Load("ScenesJump") as WJExcelDataClass.ScenesJump;
		p_Maps1 = Load("Maps1") as WJExcelDataClass.Maps1;
		p_Dialog1 = Load("Dialog1") as WJExcelDataClass.Dialog1;
		p_Characters = Load("Characters") as WJExcelDataClass.Characters;
		p_Props = Load("Props") as WJExcelDataClass.Props;
		p_Crops = Load("Crops") as WJExcelDataClass.Crops;
	}

	private System.Object Load(string name)
	{
		IFormatter f = new BinaryFormatter();
		TextAsset text = Resources.Load<TextAsset>("BinConfigData/" + name);
		Stream s = new MemoryStream(text.bytes);
		System.Object obj = f.Deserialize(s);
		s.Close();
		return obj;
	}
}

