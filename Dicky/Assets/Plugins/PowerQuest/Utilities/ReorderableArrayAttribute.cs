using UnityEngine;
using System.Collections;

public class ReorderableArrayAttribute : PropertyAttribute 
{
    // Intentionally blank
}


public class WeightedArrayAttribute : PropertyAttribute 
{	
	public string m_weightPropertyName = "m_weight";
	public string m_dataPropertyName = null;

	public WeightedArrayAttribute() 
	{}
	public WeightedArrayAttribute(string propertyName) 
	{
		m_weightPropertyName = propertyName;
	}

	public WeightedArrayAttribute(string propertyName, string dataName) 
	{
		m_weightPropertyName = propertyName;
		m_dataPropertyName = dataName;
	}
}
