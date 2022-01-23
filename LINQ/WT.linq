<Query Kind="Program" />

void Main()
{
	/*
		//Orders transactions
		PO1 : 1 : Item A : 20 : B1
		PO1 : 2 : Item B : 2 : B1
		PO1 : 3 : Item C : 5 : B1
		PO2 : 1 : Item A : 3 : B2
		PO2 : 2 : Item B : 3 : B2
		PO2 : 3 : Item C : 4 : B2
		PO2 : 4 : Item D : 8 : B2
		PO3 : 1 : Item C : 2 : B2
		PO3 : 2 : Item D : 2 : B2
		
		//Summary
		B1 :B1_Name : 1 : 3 : 27
		B2 :B2_Name : 2 : 6 : 22
	*/
	
	DataTable dtPO = new DataTable();
	dtPO.Clear();
	dtPO.Columns.Add("PONumber");
	dtPO.Columns.Add("POLine");
	dtPO.Columns.Add("ItemName");
	dtPO.Columns.Add("Qty");
	dtPO.Columns.Add("BranchID");
	
	DataRow rowPO = dtPO.NewRow();
	rowPO["PONumber"] = "PO1";
	rowPO["POLine"] = "1";
	rowPO["ItemName"] = "Item A";
	rowPO["Qty"] = "20";
	rowPO["BranchID"] = "B1";
	dtPO.Rows.Add(rowPO);
	dtPO.Rows.Add(new object[]{"PO1","2","Item B","2","B1"});
	dtPO.Rows.Add(new object[]{"PO1","3","Item B","5","B1"});
	
	dtPO.Rows.Add(new object[]{"PO2","2","Item A","3","B2"});
	dtPO.Rows.Add(new object[]{"PO2","3","Item B","3","B2"});
	dtPO.Rows.Add(new object[]{"PO2","2","Item C","4","B2"});
	dtPO.Rows.Add(new object[]{"PO2","3","Item D","8","B2"});	
	
	dtPO.Rows.Add(new object[]{"PO3","1","Item C","2","B2"});
	dtPO.Rows.Add(new object[]{"PO3","2","Item D","2","B2"});
		
    DataRow[] poRows = dtPO.Select();
	foreach(DataRow row in poRows){
		Console.WriteLine(row["PONumber"]+" : "+row["POLine"]+" : "+row["ItemName"]+" : "+row["Qty"]+" : "+row["BranchID"]);
	}

	DataTable dtBranch = new DataTable();
	dtBranch.Clear();	
	dtBranch.Columns.Add("BranchID");
	dtBranch.Columns.Add("Name");
	
	DataRow rowBranch = dtBranch.NewRow();	
	rowBranch["BranchID"] = "B1";
	rowBranch["Name"] = "B1_Name_";
	dtBranch.Rows.Add(rowBranch);
	
	DataRow rowBranch1 = dtBranch.NewRow();
	rowBranch1["BranchID"] = "B2";
	rowBranch1["Name"] = "B2_Name";
	dtBranch.Rows.Add(rowBranch1);	
	
	dtBranch.Rows.Add(new object[]{"B3","B3_Name"});
	
    DataRow[] branchRows = dtBranch.Select();
	foreach(DataRow row in branchRows){
		Console.WriteLine(row["BranchID"]+" : "+row["Name"]);
	}
	
}

// Define other methods and classes here