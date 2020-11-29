using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using PowerTools.Quest;
using PowerTools;

namespace PowerTools.Quest
{

public partial class PowerQuestEditor
{

	#region Variables: Static definitions

	#endregion
	#region Variables: Serialized

	[SerializeField] bool m_showPowerQuestEdit = false;
	[SerializeField] bool m_showTextManagerEdit = false;
	[SerializeField] bool m_showSystemAudioEdit = false;
	[SerializeField] bool m_showQuestCursorEdit = false;
	[SerializeField] bool m_showQuestCameraEdit = false;
	[SerializeField] bool m_showEditorSettings = false;

	#endregion
	#region Variables: Private

	bool m_runningRhubarb = false;
	int m_rhubarbLineId = -1;
	System.Diagnostics.Process m_rhubarbProcess = null;

	static Vector2 m_mousePos = Vector2.zero;
	static string m_mousePosCopied = "";


	#endregion
	#region GUI Layout: Tools

	[SerializeField]
	Editor m_textManagerEditor = null;

	void GuiLine( int i_height = 1 )
	{
		Rect rect = EditorGUILayout.GetControlRect(false, i_height );
		rect.height = i_height; 
		EditorGUI.DrawRect(rect, new Color ( 0.5f,0.5f,0.5f, 1 ) );
	}

	void OnGuiTools()
	{
		m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition);
		

		//
		// System quest object
		//		
		GUILayout.Space(5);	
		if ( m_powerQuest != null )
		{
			GUILayout.BeginHorizontal();

			m_showPowerQuestEdit = EditorGUILayout.Foldout(m_showPowerQuestEdit,"PowerQuest Project Settings", true, m_showPowerQuestEdit ? new GUIStyle(EditorStyles.foldout) { fontStyle = FontStyle.Bold } : EditorStyles.foldout );
			if ( GUILayout.Button( "Select Prefab", EditorStyles.miniButton, GUILayout.MaxWidth(80) ) ) { Selection.activeObject = m_powerQuest.gameObject; }
			GUILayout.EndHorizontal();
			if ( m_showPowerQuestEdit && m_powerQuest != null ) 
			{
				GUILayout.Space(5);
				Editor componentEditor = Editor.CreateEditor(m_powerQuest);
				componentEditor.DrawDefaultInspector();
				GUILayout.Space(15);		
			}
		}
		GuiLine();
		GUILayout.Space(5);

		//
		// Text manager object
		//
		if ( m_systemText != null )
		{
			GUILayout.BeginHorizontal();
			m_showTextManagerEdit = EditorGUILayout.Foldout(m_showTextManagerEdit,"Game Text Tools", true, m_showTextManagerEdit ? new GUIStyle(EditorStyles.foldout) { fontStyle = FontStyle.Bold } : EditorStyles.foldout );
			if ( GUILayout.Button( "Select Prefab", EditorStyles.miniButton, GUILayout.MaxWidth(80) ) ) { Selection.activeObject = m_systemText.gameObject; }
			GUILayout.EndHorizontal();
		
			if ( m_showTextManagerEdit && m_systemText != null ) 
			{			
				GUILayout.Space(5);	
				if ( m_textManagerEditor == null )
					m_textManagerEditor = Editor.CreateEditor(m_systemText);
				if ( m_textManagerEditor != null && m_textManagerEditor.target != null )
				{
					m_textManagerEditor.OnInspectorGUI();
					GUILayout.Space(15);
				}

			}
		}

		GuiLine();
		GUILayout.Space(5);

		//
		// Cursor Object
		//
		if ( m_powerQuest != null && m_powerQuest.GetCursorPrefab() )
		{
			GUILayout.BeginHorizontal();
			m_showQuestCursorEdit = EditorGUILayout.Foldout(m_showQuestCursorEdit,"Mouse Cursor", true, m_showQuestCursorEdit ? new GUIStyle(EditorStyles.foldout) { fontStyle = FontStyle.Bold } : EditorStyles.foldout );
			if ( GUILayout.Button( "Select Prefab", EditorStyles.miniButton, GUILayout.MaxWidth(80) ) ) { Selection.activeObject =m_powerQuest.GetCursorPrefab().gameObject; }
			GUILayout.EndHorizontal();

			if ( m_showQuestCursorEdit && m_powerQuest.GetCursorPrefab() != null ) 
			{
				GUILayout.Space(5);
				Editor componentEditor = Editor.CreateEditor(m_powerQuest.GetCursorPrefab());
				componentEditor.DrawDefaultInspector();
				GUILayout.Space(15);
			}
		}
		else  
		{
			EditorGUILayout.HelpBox("QuestCursor prefab needs to be hooked in the PowerQuest prefab", MessageType.Warning);
		}

		GuiLine();
		GUILayout.Space(5);

		//
		// Camera Object
		//
		if ( m_powerQuest != null && m_powerQuest.GetCameraPrefab() )
		{
			GUILayout.BeginHorizontal();
			m_showQuestCameraEdit = EditorGUILayout.Foldout(m_showQuestCameraEdit,"Game Camera", true, m_showQuestCameraEdit ? new GUIStyle(EditorStyles.foldout) { fontStyle = FontStyle.Bold } : EditorStyles.foldout );
			if ( GUILayout.Button( "Select Prefab", EditorStyles.miniButton, GUILayout.MaxWidth(80) ) ) { Selection.activeObject = m_powerQuest.GetCameraPrefab().gameObject; }
			GUILayout.EndHorizontal();

			if ( m_showQuestCameraEdit && m_powerQuest.GetCameraPrefab() != null ) 
			{
				GUILayout.Space(5);
				Editor componentEditor = Editor.CreateEditor(m_powerQuest.GetCameraPrefab());
				componentEditor.DrawDefaultInspector();
				GUILayout.Space(15);
			}
		}
		else  
		{
			EditorGUILayout.HelpBox("QuestCamera prefab needs to be hooked in the PowerQuest prefab", MessageType.Warning);
		}

		GuiLine();
		GUILayout.Space(5);

		//
		// Audio System
		//
		if ( m_systemAudio != null )
		{
			GUILayout.BeginHorizontal();
			m_showSystemAudioEdit = EditorGUILayout.Foldout(m_showSystemAudioEdit,"Audio Settings", true, m_showSystemAudioEdit ? new GUIStyle(EditorStyles.foldout) { fontStyle = FontStyle.Bold } : EditorStyles.foldout );
			if ( GUILayout.Button( "Select Prefab", EditorStyles.miniButton, GUILayout.MaxWidth(80) ) ) { Selection.activeObject =m_systemAudio.gameObject; }
			GUILayout.EndHorizontal();
			if ( m_showSystemAudioEdit && m_systemAudio != null ) 
			{
				GUILayout.Space(5);
				Editor componentEditor = Editor.CreateEditor(m_systemAudio);
				componentEditor.DrawDefaultInspector();
				GUILayout.Space(15);
			}
		}

		GuiLine();
		GUILayout.Space(5);

		//
		// Dialog Text Object
		//

		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Dialog Text Object");
		if ( GUILayout.Button( "Select Prefab", EditorStyles.miniButton, GUILayout.MaxWidth(80) ) ) { Selection.activeObject = m_powerQuest.GetDialogTextPrefabEditor().gameObject; }
		GUILayout.EndHorizontal();

		GuiLine();
		GUILayout.Space(5);

		//
		// Editor settings
		//
		m_showEditorSettings = EditorGUILayout.Foldout(m_showEditorSettings,"Editor Settings", true, m_showEditorSettings ? new GUIStyle(EditorStyles.foldout) { fontStyle = FontStyle.Bold } : EditorStyles.foldout );
		if ( m_showEditorSettings )
		{		
			
			
			// Auto Compile
			//GUILayout.BeginHorizontal();
			bool toggle = GUILayout.Toggle(m_smartCompile, "Smart Compile");
			if ( toggle != m_smartCompile )
				SetSmartCompileEnabled(toggle);
			if ( m_smartCompile )
			{
				EditorGUILayout.HelpBox( "When ticked, unity won't compile whenever you Save a QuestScript or add a Quest Object.\n Instead it'll wait until you hit play, or save the project.\n\nThe drawback is that unity won't import other files that you may have placed in your project directory.\n\nTo manually refresh, press the 'Compile'(F7) button in the script editor, or the refresh button below. If it's grey, then there's nothing to refresh.", 
					MessageType.Info);

				EditorGUI.BeginDisabledGroup(GetSmartCompileRequired()==false);
				if ( GUILayout.Button("Compile & Refresh Assets (Ctrl+R)") )
					PerformSmartCompile();
				EditorGUI.EndDisabledGroup();
			}

			//GUILayout.EndHorizontal();

			GUILayout.Space(5);				
		}

		GuiLine();


		//
		// Cursor pos
		//

		GUILayout.Space(5);	
		GUILayout.Label("Tools", EditorStyles.boldLabel);
		GUILayout.BeginHorizontal();
		GUILayout.Label("Cursor Pos in Scene (Ctrl-M):");
		GUILayout.TextField( m_mousePosCopied );
		GUILayout.EndHorizontal();

		if ( GetSmartCompileRequired() && GUILayout.Button("Compile & Refresh Assets (Ctrl+R)") )
			PerformSmartCompile();

		//
		// Version
		//
		LayoutVersion();

		EditorGUILayout.EndScrollView();
	}


	#endregion
	#region Functions: Private

	void UpdateQuestEditorTools()
	{
		if ( m_runningRhubarb && m_systemText != null )
		{
			if ( m_rhubarbProcess == null || m_rhubarbProcess.HasExited )
			{
				// Rhubarb process finished

				// Read rhubarb data
				if ( m_rhubarbProcess != null )
					SystemTextEditor.ReadRhubarbData(m_systemText, m_rhubarbLineId);

				// start next rhubarb process
				m_rhubarbLineId++;
				if ( m_rhubarbLineId < m_systemText.EditorGetTextDataOrdered().Count )
				{
					m_rhubarbProcess = SystemTextEditor.StartRhubarb(m_systemText, m_rhubarbLineId);
				}
				else 
				{
					// Finished!
					m_runningRhubarb = false;
					EditorUtility.SetDirty(m_systemText);	
				}

				Repaint();

			}
		}
	}

	public void RunRhubarb( )
	{
		m_runningRhubarb = true;
		m_rhubarbLineId = 0;
		m_rhubarbProcess = SystemTextEditor.StartRhubarb(m_systemText, m_rhubarbLineId);
	}


	#endregion
	
}

}