using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerTools;

namespace PowerTools.Quest
{


public class GuiDialogTreeComponent : MonoBehaviour 
{
	[SerializeField, Tooltip("The padding between each option")] float m_itemSpacing = 2;
	[SerializeField] Color m_colorDefault = Color.white;
	[SerializeField] Color m_colorUsed = Color.white;
	[SerializeField] Color m_colorHover = Color.white;
	[SerializeField] GameObject m_textInstance = null;
	[SerializeField] GUIContain m_background = null;

	List<QuestText> m_items = new List<QuestText>();

	// Use this for initialization
	void Start() 
	{
		m_textInstance.SetActive(false);
	}


	void OnEnable()
	{
		UpdateItems();
	}
	
	// Update is called once per frame
	void Update() 
	{
		UpdateItems();
	}

	void UpdateItems()
	{

		DialogTree dialog = PowerQuest.Get.GetCurrentDialog();
		if ( dialog == null )
			return;
		
		List<DialogOption> options = dialog.Options.FindAll((item)=>item.Visible);
		int numOptions = options.Count;

		while ( m_items.Count < numOptions )
		{
			// Add items (bottom up)
			Vector3 pos = m_textInstance.transform.position + new Vector3( 0, m_items.Count*m_itemSpacing, 0);
			GameObject obj = Instantiate(m_textInstance.gameObject, pos,Quaternion.identity, m_textInstance.transform.parent); 
			m_items.Add(obj.GetComponent<QuestText>());
			m_background.ContainY(obj);
		}

		Vector2 mousePos = PowerQuest.Get.GetCameraGui().ScreenToWorldPoint( Input.mousePosition.WithZ(0) );
		DialogOption selectedOption = null;
		float yOffset = 0;
		for ( int i = numOptions-1; i >= 0; --i)
		{
			DialogOption option = options[i];
			QuestText item = m_items[i];

			item.gameObject.transform.position = m_textInstance.transform.position + new Vector3( 0, yOffset, 0);

			// Check mouse over
			bool over = ( mousePos.y > item.GetComponent<Renderer>().bounds.min.y && mousePos.y < item.GetComponent<Renderer>().bounds.max.y );
			if ( over )
				selectedOption = option;
			item.gameObject.SetActive(true);
			item.text = option.Text;
			item.color = over ? m_colorHover : ( option.Used ? m_colorUsed : m_colorDefault);
			yOffset += m_itemSpacing;
			Renderer renderer = item.gameObject.GetComponent<Renderer>();
			if ( renderer != null )
				yOffset += item.gameObject.GetComponent<Renderer>().bounds.size.y;
			
		}

		for ( int i = numOptions; i < m_items.Count; ++i )
			m_items[i].gameObject.SetActive(false);

		PowerQuest.Get.SetDialogOptionSelected(selectedOption);
		if ( selectedOption != null && Input.GetMouseButtonDown(0) )
		{
			PowerQuest.Get.OnDialogOptionClick(selectedOption);
		}

	}
}

}