using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class DialogChatWithBarney : DialogTreeScript<DialogChatWithBarney>
{
	public IEnumerator Start()
	{
		yield return C.WalkToClicked();
		C.FaceClicked();
		C.Barney.Face(C.Dave);
		yield return C.Barney.Say("What is it?");
		yield return E.Break;
		
	}

	public IEnumerator Option1( IDialogOption option )
	{
		yield return C.Dave.Say("Rad cave");
		yield return E.WaitSkip();
		C.Barney.FaceUp();
		yield return E.WaitSkip();
		yield return C.Barney.Say("Uh-huh");
		yield return E.WaitSkip();
		C.Barney.Face(C.Dave);
		yield return E.Break;
		
	}


	public IEnumerator Option2( IDialogOption option )
	{
		yield return C.Dave.Say("What's with this forest anyway?");
		yield return C.Barney.Say("Whaddaya mean?");
		
		OptionOff(1);
		OptionOff(2);
		OptionOff(3);
		OptionOff("bye");
		
		OptionOn("tree");
		OptionOn("leaf");
		OptionOn("forestdone");
		yield return E.Break;
		
	}


	public IEnumerator Option3( IDialogOption option )
	{
		yield return C.Dave.Say("Nice well there, huh?");
		yield return C.Barney.Say("No. I hate it. Lets never speak of it again");
		yield return C.Dave.Say("Alright");
		option.OffForever();
		yield return E.Break;
		
	}



	public IEnumerator OptionForestDone( IDialogOption option )
	{
		OptionOff("tree");
		OptionOff("leaf");
		OptionOff("forestdone");
		
		OptionOn(1);
		OptionOn(2);
		OptionOn(3);
		OptionOn("bye");
		yield return E.Break;
		
	}


	public IEnumerator OptionTree( IDialogOption option )
	{
		yield return C.Dave.Say("The trees are cool");
		yield return C.Barney.Say(" I guess");
		yield return E.Break;
		
	}


	public IEnumerator OptionLeaf( IDialogOption option )
	{
		yield return C.Dave.Say("I like the foliage");
		yield return C.Barney.Say("Yes. It is pleasant foliage");
		yield return E.Break;
		
	}

	public IEnumerator OptionBye( IDialogOption option )
	{
		yield return C.Dave.Say("Later!");
		yield return E.WaitSkip();
		yield return C.Barney.FaceRight();
		yield return E.WaitSkip();
		yield return C.Barney.Say("Whatever");
		Stop();
		yield return E.Break;
	}

	public IEnumerator OnStart()
	{

		yield return E.Break;
	}

	public IEnumerator OnStop()
	{

		yield return E.Break;
	}
}