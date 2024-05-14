using System.Diagnostics;

public class CDebuggerUser {
    [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
    public string Login { get; set; }
 
    //indicates that the member itself (root) is not shown, but its constituent objects are displayed if it is an array or collection.
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public CDebuggerFullName FullName {
        get => m_fullName;
        set => m_fullName = m_collapsedFullName = value;
    }
    
    //indicates that the member is displayed but not expanded by default (shown as a collapsed tree node)
    [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
    public CDebuggerFullName CollapsedName { get => m_collapsedFullName; }
    
    //indicates that the member is not displayed in the data window
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string HashedPassword { get; set; }
    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    private CDebuggerFullName m_fullName = null;
    private CDebuggerFullName m_collapsedFullName = null;
}