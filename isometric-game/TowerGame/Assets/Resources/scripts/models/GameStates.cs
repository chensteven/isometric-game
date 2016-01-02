using System.Collections;

public static class GameStates 
{
	
	private static bool drawing = false;	
	private static Enums.Tool currentTool = Enums.Tool.Select;
	private static Enums.CraftableItems currentItem = Enums.CraftableItems.GrassPatch4x4;

	public static bool Drawing {
		get {
			return drawing;
		}
		set {
			drawing = value;
		}
	}

	public static Enums.Tool CurrentTool {
		get {
			return currentTool;
		}
		set {
			currentTool = value;
		}
	}

	public static Enums.CraftableItems CurrentItem {
		get {
			return currentItem;
		}
		set {
			currentItem = value;
		}
	}

}
