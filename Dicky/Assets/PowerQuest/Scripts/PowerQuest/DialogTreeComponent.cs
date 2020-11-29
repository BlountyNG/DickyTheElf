using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using PowerScript;
using PowerTools;

namespace PowerTools.Quest
{

#region Class: DialogOption

[System.Serializable]
public partial class DialogOption : IDialogOption, IQuestClickable
{	
	[SerializeField] string m_name = string.Empty;
	[SerializeField,Multiline] string m_text = string.Empty;
	[SerializeField] bool m_visible = true;
	[SerializeField,HideInInspector] bool m_disabled = false;
	[SerializeField,HideInInspector] bool m_used = false;
	int m_inlineId = -1;

	public string Name { get { return m_name;} set { m_name = value; } }
	public string Text { get { return m_text;} set { m_text = value; } }
	/// The description here is identical to Text, but the localization code will replace lines where x.Description = "blah" whereas it won't for x.Text = "blah"
	public string Description { get { return m_text;} set { m_text = value; } } 
	public bool Visible { get { return m_visible;} set { m_visible = value; } }
	public bool Disabled {get { return m_disabled;} set { m_disabled = value; } }
	public bool Used {get { return m_used;} set { m_used = value; } }

	public void Show() { if ( Disabled == false) Visible = true; }
	public void Hide() { Visible = false; }
	public void HideForever() { Visible = false; Disabled = true; }

	public void On() { if ( Disabled == false) Visible = true; }
	public void Off() { Visible = false; }
	public void OffForever() { Visible = false; Disabled = true; }

	// Inline id is used for E.StartInlineDialog();
	public int InlineId { get { return m_inlineId; } set { m_inlineId = value; } }

	// Implementing IQuestClickable just so can get the description automatially... probably not really worth doing that ha ha.
	public eQuestClickableType ClickableType { get { return eQuestClickableType.Gui; } }
	public MonoBehaviour Instance { get { return null; } }
	public string ScriptName { get { return null; } }
	public Vector2 Position { get{ return Vector2.zero; } }
	public Vector2 WalkToPoint { get; set; }
	public Vector2 LookAtPoint { get; set; }
	public float Baseline { get; set; }
	public bool Clickable { get; set; }
	public string Cursor { get { return null; } }
	public void OnInteraction( eQuestVerb verb ){}
	public void OnCancelInteraction( eQuestVerb verb ){}
	public QuestScript GetScript() { return null; }
	public IQuestScriptable GetScriptable() { return null; }
	

}

#endregion

#region Class: DialogTreeScript

// Script class to inherit from for gui scripts
[System.Serializable]
public class DialogTreeScript<T> : QuestScript where T : QuestScript
{
	public static T Script { get { return E.GetScript<T>(); } }

	// Shortcut functions when inside a dialog
	public void OptionOn(int id){ D.Current.OptionOn(id);  }
	public void OptionOff(int id){ D.Current.OptionOff(id); }
	public void OptionOffForever(int id){ D.Current.OptionOffForever(id); }

	public void OptionOn(string id){ D.Current.OptionOn(id);  }
	public void OptionOff(string id){ D.Current.OptionOff(id); }
	public void OptionOffForever(string id){ D.Current.OptionOffForever(id); }

	protected void Stop() { PowerQuest.Get.StopDialog(); }
}

//
// Prop Data and functions. Persistant between scenes, as opposed to GuiComponent which lives on a GameObject in a scene.
//
[System.Serializable] 
public partial class DialogTree : IQuestScriptable, IDialogTree
{
	//
	// Default values set in inspector
	//
	[SerializeField] List<DialogOption> m_options = new List<DialogOption>();
	[ReadOnly][SerializeField] string m_scriptName = "DialogNew";
	[ReadOnly][SerializeField] string m_scriptClass = "DialogNew";

	//
	// Private variables
	//
	QuestScript m_script = null;
	GameObject m_prefab = null;
	DialogTreeComponent m_instance = null;

	//
	//  Properties
	//
	public string ScriptName { get{ return m_scriptName;} }
	public DialogTree Data { get{ return this; } }
	public List<DialogOption> Options { get { return m_options; } }
	public int NumOptionsEnabled { get {
		int result = 0;
		m_options.ForEach(item=> {if (item.Visible) ++result;} );
		return result;
	} }

	//
	// Getters/Setters - These are used by the engine. Scripts mainly use the properties
	//
	public QuestScript GetScript() { return m_script; }
	public IQuestScriptable GetScriptable() { return this; }
	public T GetScript<T>() where T : DialogTreeScript<T> {  return ( m_script != null ) ? m_script as T : null; }
	public string GetScriptName(){ return m_scriptName; }
	public string GetScriptClassName() { return m_scriptClass; }
	public void HotLoadScript(Assembly assembly) { QuestUtils.HotSwapScript( ref m_script, m_scriptClass, assembly ); }
	public GameObject GetPrefab() { return m_prefab; }

	/* NB: this is maybe not used? */
	public DialogTreeComponent GetInstance() { return m_instance; }
	public void SetInstance(DialogTreeComponent instance) 
	{ 
		m_instance = instance; 
		m_instance.SetData(this);
	}

	//
	// Public Functions
	//

	// AGS style option on/off functions
	public void OptionOn(int id){ this[id.ToString()].Show(); }
	public void OptionOff(int id){this[id.ToString()].Hide(); }
	public void OptionOffForever(int id){ this[id.ToString()].HideForever(); }

	// AGS style option on/off functions	   
	public void OptionOn(string id){ this[id].Show(); }
	public void OptionOff(string id){this[id].Hide(); }
	public void OptionOffForever(string id){ this[id].HideForever(); }

	// Shortcut to Start/Stop dialog
	public void Start() { PowerQuest.Get.StartDialog(ScriptName); }
	public void Stop() { PowerQuest.Get.StopDialog(); }

	// Shortcut access to options
	public DialogOption this[int index]
	{
	    get { return m_options[index]; }
		set { m_options[index] = value; }
	}

	// Shortcut access to options
	public DialogOption this[string name]
	{
		get 
		{ 
			DialogOption result = m_options.Find(item=>string.Compare( item.Name,name,true)== 0);
			if ( result == null )
				Debug.LogError("Failed to find option "+name+" in dialog "+m_scriptName);
			return result; 
		}
	}

	//
	// Internal Functions
	//

	//
	// Initialisation
	//

	public void EditorInitialise( string name )
	{
		m_scriptName = name;
		m_scriptClass = "Dialog"+name;
	}
	public void EditorRename(string name)
	{
		m_scriptName = name;
		m_scriptClass = "Dialog"+name;	
	}

	public void OnPostRestore( int version, GameObject prefab )
	{
		m_prefab = prefab;
		if ( m_script == null ) // script could be null if it didn't exist in old save game, but does now.
			m_script = QuestUtils.ConstructByName<QuestScript>(m_scriptClass);
	}

	public void Initialise( GameObject prefab )
	{
		m_prefab = prefab;

		// Construct the script
		m_script = QuestUtils.ConstructByName<QuestScript>(m_scriptClass);

		// Deep copy Options
		List<DialogOption> defaultItems = m_options;
		m_options = new List<DialogOption>(defaultItems.Count);
		for ( int i = 0; i < defaultItems.Count; ++i )
		{
			m_options.Add( new DialogOption() );
			QuestUtils.CopyFields(m_options[i], defaultItems[i]);
		}
	}

	// Handles setting up defaults incase items have been added or removed since last loading a save file
	[System.Runtime.Serialization.OnDeserializing]
	void CopyDefaults( System.Runtime.Serialization.StreamingContext sc )
	{
		QuestUtils.InitWithDefaults(this);
	}
}

#endregion
#region Class: DialogTreeComponent

//
// The Dialog data from the editor
//
[SelectionBase]
public class DialogTreeComponent : MonoBehaviour 
{
	[SerializeField] DialogTree m_data = new DialogTree();

	public DialogTree GetData() { return m_data; }
	public void SetData(DialogTree data) { m_data = data; }
}

#endregion
}