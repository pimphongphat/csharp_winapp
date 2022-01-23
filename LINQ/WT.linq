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
	dtPO.Columns.Add("Qty", typeof(Double));  
	dtPO.Columns.Add("BranchID");
	
	DataRow rowPO = dtPO.NewRow();
	rowPO["PONumber"] = "PO1";
	rowPO["POLine"] = "1";
	rowPO["ItemName"] = "Item A";
	rowPO["Qty"] = 20;
	rowPO["BranchID"] = "B1";
	dtPO.Rows.Add(rowPO);
	dtPO.Rows.Add(new object[]{"PO1","2","Item B",2,"B1"});
	dtPO.Rows.Add(new object[]{"PO1","3","Item B",5,"B1"});
	
	dtPO.Rows.Add(new object[]{"PO2","2","Item A",3,"B2"});
	dtPO.Rows.Add(new object[]{"PO2","3","Item B",3,"B2"});
	dtPO.Rows.Add(new object[]{"PO2","2","Item C",4,"B2"});
	dtPO.Rows.Add(new object[]{"PO2","3","Item D",8,"B2"});	
	
	dtPO.Rows.Add(new object[]{"PO3","1","Item C",2,"B2"});
	dtPO.Rows.Add(new object[]{"PO3","2","Item D",2,"B2"});
	
	DataRow[] poRows = dtPO.Select();
	/*
	foreach(DataRow row in poRows){
		Console.WriteLine(row["PONumber"]+" : "+row["POLine"]+" : "+row["ItemName"]+" : "+row["Qty"]+" : "+row["BranchID"]);
	}
	*/

	DataTable dtBranch = new DataTable();
	dtBranch.Clear();	
	dtBranch.Columns.Add("BranchID");
	dtBranch.Columns.Add("Name");
	
	DataRow rowBranch = dtBranch.NewRow();	
	rowBranch["BranchID"] = "B1";
	rowBranch["Name"] = "B1_Name";
	dtBranch.Rows.Add(rowBranch);
	
	DataRow rowBranch1 = dtBranch.NewRow();
	rowBranch1["BranchID"] = "B2";
	rowBranch1["Name"] = "B2_Name";
	dtBranch.Rows.Add(rowBranch1);	
	
	dtBranch.Rows.Add(new object[]{"B3","B3_Name"});
	
    DataRow[] branchRows = dtBranch.Select();
	
	/*
	foreach(DataRow row in branchRows){
		Console.WriteLine(row["BranchID"]+" : "+row["Name"]);
	}
	*/
	
/*
		//Summary
		B1 :B1_Name : 1 : 3 : 27
		B2 :B2_Name : 2 : 6 : 22
*/

/*
	var resultPO = from po in dtPO.AsEnumerable() select po.Field<string>("BranchID");
	foreach(var row in resultPO){
		Console.WriteLine(row);
	}
	*/
	
	/*
	var resultPO2 = from po in dtPO.AsEnumerable() select new {BranchID = po.Field<string>("BranchID"),PONumber = po.Field<string>("PONumber"),Qty = po.Field<string>("Qty")};
	foreach(var row in resultPO2){
		Console.WriteLine(row.BranchID+" : "+row.PONumber+" : "+row.Qty);
	}
	*/
	
	var resultPO2 = from po in dtPO.AsEnumerable() 
		        	group po by po.Field<string>("BranchID") into newGroup
        			orderby newGroup.Key	
					select newGroup;
					
	foreach(var nameGroup in resultPO2){
		//Console.WriteLine(nameGroup);
		//Console.WriteLine($"Key: {nameGroup.Key}");
        foreach (var po in nameGroup)
        {
            //Console.WriteLine($"\t{po.PONumber}, {po.Qty}");
			//Console.WriteLine(po);
			//Console.WriteLine($"\t{nameGroup.Key} : {po["PONumber"]}, {po["Qty"]}");
        }
	}	


	DataTable dtSumBranch = new DataTable();
	dtSumBranch.Clear();
	dtSumBranch.Columns.Add("BranchId");
	dtSumBranch.Columns.Add("NumPO", typeof(Double));  
	dtSumBranch.Columns.Add("NumLine", typeof(Double));  
	dtSumBranch.Columns.Add("SumQty", typeof(Double));  
	
	foreach(var nameGroup in resultPO2){
		//Console.WriteLine(nameGroup);
		//Console.WriteLine($"{nameGroup.Key}");
		
		/*
		var groupPONumber = from poNumber in nameGroup
							group poNumber by poNumber.PONumber
							*/
		var counts = nameGroup.GroupBy(x => x.Field<string>("PONumber"))
                     .Select(g => new { g.Key, Count = g.Count() });		
		//Console.WriteLine($"{nameGroup.Key} : {counts.Count()}");	
		

		//Console.WriteLine(counts);
		var sumBranch = counts.Sum(x => x.Count);
		//Console.WriteLine(sumBranch);
		//Console.WriteLine($"{nameGroup.Key} : {counts.Count()} : {sumBranch}");
		
		var summaries = nameGroup.Sum(ol => ol.Field<Double>("Qty"));
		//Console.WriteLine(summaries);
		//Console.WriteLine($"{nameGroup.Key} : {counts.Count()} : {sumBranch} : {summaries}");
		dtSumBranch.Rows.Add(new object[]{nameGroup.Key,counts.Count(),sumBranch,summaries});
	}

	var joinResult = from a in dtSumBranch.AsEnumerable()
	                 join b in dtBranch.AsEnumerable() on a.Field<string>("BranchId") equals b.Field<string>("BranchId") into poSummary
                      from c in poSummary.DefaultIfEmpty()
					  select new{BranchID = c.Field<string>("BranchID"),BranchName = c.Field<string>("Name"),NumPO = a.Field<double>("NumPO"),NumLine = a.Field<double>("NumLine"),SumQty = a.Field<double>("SumQty")};
						
	Console.WriteLine(joinResult);
}

// Define other methods and classes here
