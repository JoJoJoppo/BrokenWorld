using UnityEngine;
using System.Collections;

public interface IItemSystem_Object{
	string Name{ get; set;}
	int ObjectValue{ get; set;}
	Sprite Icon{ get; set;}
	int Weight{ get; set;}
}
