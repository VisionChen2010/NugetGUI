using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
class ListViewColumnSorter: IComparer
  {      
    private int ColumnToSort;
    private SortOrder OrderOfSort;
    private CaseInsensitiveComparer ObjectCompare;
    public ListViewColumnSorter()// 构造函数
    {        
    	ColumnToSort = 2;
    	OrderOfSort = SortOrder.None;
    	ObjectCompare = new CaseInsensitiveComparer();
    }  
    public int Compare(object x, object y)
    { 
    	int compareResult;
        ListViewItem listviewX, listviewY;
        listviewX = (ListViewItem)x;
        listviewY = (ListViewItem)y;
        int number;
        if(int.TryParse(listviewX.SubItems[ColumnToSort].Text,out number))
        {
	        int m=int.Parse(listviewX.SubItems[ColumnToSort].Text);
	        int n=int.Parse(listviewY.SubItems[ColumnToSort].Text);
	        compareResult=m.CompareTo(n);
        }
        else
        {
        	compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
        }
        if (OrderOfSort == SortOrder.Ascending)
        {
            return compareResult;
        }
        else if (OrderOfSort == SortOrder.Descending)
        {
            return (-compareResult);
        }
        else
        {
            return 0;
        }
    }    
    public int SortColumn
    {   
    	set
        {     ColumnToSort = value;
        }
        get
        {
            return ColumnToSort;
        }
    }    
    public SortOrder Order
    {   set
        {
            OrderOfSort = value;
        }
        get
        {
            return OrderOfSort;
        }
    }   
}
