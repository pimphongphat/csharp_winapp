<Query Kind="Program" />

void Main()
{
	var cats = new[] { "Category 1", "Category 2", "Category 3" };
	var frms = new[] { "Form 1", "Form 2", "Form 3" };
	var title = new[] { "Title 1", "Title 2", "Title 3" };
	var transactions = new List<Transaction>();

	for (var i = 0; i <= 150; i++)
	{
	    transactions.Add(new Transaction
	    {
	        Category = i % 2 == 0 ? cats[0] : i % 3 == 0 ? cats[1] : cats[2],
	        Form = i % 5 == 0 ? frms[0] : i % 7 == 0 ? frms[1] : frms[2],
			Title = i % 5 == 0 ? title[0] : i % 7 == 0 ? title[1] : title[2]
	    });
	}

	var groupedTransactions = transactions.GroupBy(x => x.Category)
    .Select(x => new
    {
        Category = x.Key,
        Forms = x.ToList()
            .GroupBy(y => y.Form)
    });
	
	Console.WriteLine(groupedTransactions);
}

// Define other methods and classes here
public class Transaction
{
    public string Category { get; set; }
    public string Form { get; set; }
	public string Title { get; set; }
}