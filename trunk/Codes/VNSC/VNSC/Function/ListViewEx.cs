#region Header
// ----------------------------------------------------------------------------
// File Name:	    ListView.cs
// Contents : 		ListViewEx class Implementation	
// Originator: 	    Madhu Raykar.
// Date:		    06.12.05 
// Version:		    1.00.
// ----------------------------------------------------------------------------
#endregion

#region Namespace
using System;
using System.Windows.Forms;
using System.Collections;
#endregion

namespace VNSC {
    /// <summary>
    /// Represents a Windows list view control, 
    /// which displays a collection of items that can be 
    /// displayed using one of four different views.
    /// This is extened to support column hiding.
    /// This control has a built in context menu
    /// which helps in hiding the columns.
    /// </summary>
    public class ListViewEx : ListView {
       
        #region Private members
        private ColumnHeaderCollectionEx columnHeadersEx = null;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the collection of all column headers 
        /// that appear in the control.
        /// </summary>
        public new ColumnHeaderCollectionEx Columns {
            get{return columnHeadersEx;}
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ListViewEx() {
            // Create a new collection to hold the columns
            columnHeadersEx = new ColumnHeaderCollectionEx(this);
            // Create the context menu.
            base.ContextMenu = new ContextMenu();
            base.ContextMenu.MenuItems.Add(columnHeadersEx.ContextMenu);
            base.ContextMenu.Popup += new EventHandler(ContextMenuPopup);
            
            base.View = System.Windows.Forms.View.Details;

        }
        #endregion
        
        #region Private methods
        private void ContextMenuPopup(object sender, EventArgs e) {
        }
        #endregion

    }

    /// <summary>
    /// This class Represents the collection of 
    /// ColumnHeaderEx in a ListViewEx control.
    /// This class stores the column headers
    /// that are displayed in the ListViewEx control when the View 
    /// property is set to View.Details. 
    /// This class stores ColumnHeaderEx objects that define the text
    /// to display for a column as well as how the column header is 
    /// displayed in the ListViewEx control when displaying columns.
    /// When a ListViewEx displays columns, the items and their subitems 
    /// are displayed in their own columns. 
    /// </summary>
    public class ColumnHeaderCollectionEx : ListView.ColumnHeaderCollection{

        #region Private members
        /// <summary>
        /// This is to maintain the list of columns added
        /// to the ListViewEx control, this will contain both
        /// visible and hidden columns.
        /// </summary>
        private SortedList columnList = new SortedList();
        /// <summary>
        /// MenuItem which contains all columns menuitem as
        /// Subitems. This menuitem can be used to add to the 
        /// context menu, which inturn can be used to
        /// Hide/Show the columns.
        /// </summary>
        private MenuItem contextMenu = null;
        #endregion
        
        #region Properties
        /// <summary>
        /// Indexer to get columns by index
        /// </summary>
        public new ColumnHeaderEx this[int index] {
            get {
                return (ColumnHeaderEx)columnList.GetByIndex(index);
            }
        }

        /// MenuItem which contains all columns menuitem as
        /// Subitems. This menuitem can be used to add to the 
        /// context menu, which inturn can be used to
        /// Hide/Show the columns.
        public MenuItem ContextMenu {
            get {return contextMenu;}
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// You cannot create an instance of this class 
        /// without associating it with a ListView control.
        /// </summary>
        /// <param name="owner"></param>
        public ColumnHeaderCollectionEx(ListView owner) : base (owner) {
            // Create a menu item to add submenus for each column added
            contextMenu = new MenuItem("Columns");
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Method adds a single column header to the collection.
        /// </summary>
        /// <param name="str">Text to display</param>
        /// <param name="width">Width of column</param>
        /// <param name="textAlign">Alignment</param>
        /// <returns>new ColumnHeaderEx added</returns>
        public override ColumnHeader Add(string str, int width, HorizontalAlignment textAlign) {
            ColumnHeaderEx column = new ColumnHeaderEx(str, width, textAlign);
            this.Add (column);
            return column;
        }
        
        /// <summary>
        /// Method adds a single column header to the collection.
        /// </summary>
        /// <param name="column"></param>
        /// <returns>The zero-based index into the collection 
        /// where the item was added.</returns>
        public override int Add(ColumnHeader column) {
            return this.Add (new ColumnHeaderEx(column));
        }

        /// <summary>
        /// Adds an array of column headers to the collection.
        /// </summary>
        /// <param name="values">An array of ColumnHeader 
        /// objects to add to the collection. </param>
        public override void AddRange(ColumnHeader[] values) {
            // Add range of column headers
            for(int index = 0; index < values.Length; index++) {
                this.Add (new ColumnHeaderEx(values[index]));
            }
        }

        /// <summary>
        /// Adds an existing ColumnHeader to the collection.
        /// </summary>
        /// <param name="column">The ColumnHeader to 
        /// add to the collection. </param>
        /// <returns>The zero-based index into the collection 
        /// where the item was added.</returns>
        public int Add(ColumnHeaderEx column) {
            // Add the column to the base
            int retValue = base.Add (column);
            // Keep a refrence in columnList
            columnList.Add(column.ColumnID, column);
            // Add the its menu to main menu
            ContextMenu.MenuItems.Add(column.ColumnMenuItem);
            // Subscribe to the visiblity change event of the column
            column.VisibleChanged += new EventHandler(ColumnVisibleChanged);
            return retValue;
        }

        /// <summary>
        /// Removes the specified column header from the collection.
        /// </summary>
        /// <param name="column">A ColumnHeader representing the 
        /// column header to remove from the collection.</param>
        public new void Remove(ColumnHeader column) {
            // Remove from base
            base.Remove (column);
            // Remove the reference in columnList
            columnList.Remove(((ColumnHeaderEx)column).ColumnID);
            // remove the menu item associated with it
            ContextMenu.MenuItems.Remove(((ColumnHeaderEx)column).ColumnMenuItem);
        }

        /// <summary>
        /// Removes the column header at the specified index within the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the 
        /// column header to remove</param>
        public new void RemoveAt(int index) {
            ColumnHeader column  = this[index];
            this.Remove(column);
        }

        /// <summary>
        /// Removes all column headers from the collection.
        /// </summary>
        public new void Clear() {
            // Clear all columns in base
            base.Clear();
            // Remove all references
            columnList.Clear();
            // Clear all menu items
            ContextMenu.MenuItems.Clear();  
        }

        #endregion

        #region Private methods
        /// <summary>
        /// This method is used to find the first visible column
        /// which is present in front of the column specified
        /// </summary>
        /// <param name="column">refrence columns for search</param>
        /// <returns>null if no visible colums are in front of
        /// the column specified, else previous columns returned</returns>
        private ColumnHeaderEx FindPreviousVisibleColumn(ColumnHeaderEx column) {
            // Get the position of the search column
            int index = columnList.IndexOfKey(column.ColumnID);
            if(index > 0) {
                // Start a recursive search for a visible column
                ColumnHeaderEx prevColumn = (ColumnHeaderEx)columnList.GetByIndex(index - 1);
                if((prevColumn != null) && (prevColumn.Visible == false)) {
                    prevColumn = FindPreviousVisibleColumn(prevColumn);
                }
                return prevColumn;
            }
            // No visible columns found in font of specified column
            return null;
        }
        
        /// <summary>
        /// Handler to handel the visiblity change of columns
        /// </summary>
        /// <param name="sender">ColumnHeaderEx</param>
        /// <param name="e"></param>
        private void ColumnVisibleChanged(object sender, EventArgs e) {
            ColumnHeaderEx column = (ColumnHeaderEx)sender;

            if(column.Visible == true) {
                // Show the hidden column
                // Get the position where the hidden column has to be shown
                ColumnHeaderEx prevHeader = FindPreviousVisibleColumn(column);
                if(prevHeader == null) {
                    // This is the first column, so add it at 0 location
                    base.Insert(0, column);
                }
                else {
                    // Got the location, place it their.
                    base.Insert(prevHeader.Index + 1, column);
                }
            }
            else {
                // Hide the column.
                // Remove it from the base, dont worry we have the 
                // refrence in columnList to get it back
                base.Remove(column);
            }
        }

        #endregion
    }

    /// <summary>
    /// This class object represents a single column header in a ListViewEx control.
    /// This class is extended from ColumnHeader, inorder to support column hiding.
    /// </summary>
    public class ColumnHeaderEx : ColumnHeader {
        
        #region Private members
        private MenuItem    menuItem = null;
        private bool        columnVisible = true;
        private int         columnID = 0;
        private static int  autoColumnID = 0;
        #endregion 

        #region Events
        /// <summary>
        /// This event is raised when the visibility of column
        /// is changed.
        /// </summary>
        public event EventHandler VisibleChanged;
        #endregion

        #region Properties
        /// <summary>
        /// A unique identifier for a Column
        /// </summary>
        public int ColumnID {
            get{return columnID;}
        }

        /// <summary>
        /// Property to change the visibility of the column
        /// </summary>
        public bool Visible {
            get{return columnVisible;}
            set{ShowColumn(value);}
        }

        /// <summary>
        /// Menu item which represents the column.
        /// This menuitem can be used to add to the 
        /// context menu, which can inturn used to
        /// Hide/Show the column
        /// </summary>
        public MenuItem ColumnMenuItem {
            get{return menuItem;}
        }
        
        /// <summary>
        /// Column Text to be displayed
        /// </summary>
        public new string Text {
            get{return base.Text;}
            set{
                base.Text = value;
                // Ensure that menu name is same as column name
                menuItem.Text = value;
            }
        }

        /// <summary>
        /// Gets the ListViewEx control the 
        /// ColumnHeader is located in.
        /// </summary>
        public new ListViewEx ListView {
            get{return (ListViewEx)base.ListView;}
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ColumnHeaderEx() {
            Initialize("", 0, HorizontalAlignment.Left);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="str">Text to display</param>
        /// <param name="width">Width of column</param>
        /// <param name="textAlign">Alignment</param>
        public ColumnHeaderEx(string str, int width, HorizontalAlignment textAlign) {
            Initialize(str, width, textAlign);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="column"></param>
        public ColumnHeaderEx(ColumnHeader column){
            Initialize(column.Text, column.Width, column.TextAlign);
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Column Initialization
        /// </summary>
        /// <param name="str">Text to display</param>
        /// <param name="width">Width of column</param>
        /// <param name="textAlign">Alignment</param>
        private void Initialize(string str, int width, HorizontalAlignment textAlign) {
            base.Text = str;
            base.Width = width;
            base.TextAlign = textAlign;
            columnID = autoColumnID++;   

            // Create the menu item associated with this column
            menuItem = new MenuItem(Text, new System.EventHandler(this.MenuItemClick));
            menuItem.Checked = true;    
        }

        /// <summary>
        /// Method to show/hide column
        /// </summary>
        /// <param name="visible">visibility</param>
        private void ShowColumn(bool visible) {
            if(columnVisible != visible) {
                columnVisible = visible;
                menuItem.Checked = visible;
                if(VisibleChanged != null) {
                    VisibleChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Handler to handel toggel of menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemClick(Object sender, System.EventArgs e) {
            MenuItem menuItem = (MenuItem)sender;
            // Ensure Column is hidden/shown accordingly
            ShowColumn(!menuItem.Checked);
        }
        #endregion

    }
}
