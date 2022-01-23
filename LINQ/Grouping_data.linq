<Query Kind="Program" />

void Main()
{
	var users = new List<User>()    
        {    
        new User { Name = "John Doe", Age = 42, HomeCountry = "USA" },    
        new User { Name = "Jane Doe", Age = 38, HomeCountry = "USA" },    
        new User { Name = "Joe Doe", Age = 19, HomeCountry = "Germany" },    
        new User { Name = "Jenna Doe", Age = 19, HomeCountry = "Germany" },    
        new User { Name = "James Doe", Age = 8, HomeCountry = "USA" },    
        };    
        var usersGroupedByCountry = users.GroupBy(user => user.HomeCountry);    
        foreach(var group in usersGroupedByCountry)    
        {    
        Console.WriteLine("Users from " + group.Key + ":");    
        foreach(var user in group)    
            Console.WriteLine("* " + user.Name);
        } 
			
        var usersGroupedByFirstLetters = users.GroupBy(user => user.Name.Substring(0, 2));
        foreach(var group in usersGroupedByFirstLetters)
        {
        Console.WriteLine("Users starting with " + group.Key + ":");
        foreach(var user in group)
            Console.WriteLine("* " + user.Name);
        }
		
		var usersGroupedByAgeGroup = users.GroupBy(user => user.GetAgeGroup());
        foreach(var group in usersGroupedByAgeGroup)
        {
        Console.WriteLine(group.Key + ":");
        foreach(var user in group)
            Console.WriteLine("* " + user.Name + " [" + user.Age + " years]");
        }
		
		var usersGroupedByCountryAndAge = users.GroupBy(user => new { user.HomeCountry, user.Age });
        foreach(var group in usersGroupedByCountryAndAge)
        {
        Console.WriteLine("Users from " + group.Key.HomeCountry + " at the age of " + group.Key.Age + ":");
        foreach (var user in group)
            Console.WriteLine("* " + user.Name + " [" + user.Age + " years]");
        }
}

// Define other methods and classes here
public class User
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string HomeCountry { get; set; }

        public string GetAgeGroup()
        {
        if (this.Age < 13)
            return "Children";
        if (this.Age < 20)
            return "Teenagers";
        return "Adults";
        }
    }