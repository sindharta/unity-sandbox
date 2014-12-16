using UnityEngine;

public static class UnityExtensions {

//---------------------------------------------------------------------------------------------------------------------		
    public static bool IsKeyPressed(this Event e, KeyCode keyCode, string controlName) {		
        if (e.isKey && e.keyCode == keyCode && GUI.GetNameOfFocusedControl() == controlName) {		
            return true;		
        }		
        
        return false;		
    }

}
