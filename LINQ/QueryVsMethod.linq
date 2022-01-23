<Query Kind="Program" />

void Main()
{
	List<Person> persons = new List<Person>();
	persons.Add( new Person { PersonID = 1, car = "Ferrari" }); 
	persons.Add( new Person { PersonID = 1, car = "BMW"     }); 
	persons.Add( new Person { PersonID = 2, car = "Audi"    });
	
	var results = from p in persons
              group p.car by p.PersonID into g
              select new { PersonId = g.Key, Cars = g.ToList() };
			  
			  
	var results2 = persons.GroupBy(
    p => p.PersonID, 
    p => p.car,
    (key, g) => new { PersonId = key, Cars = g.ToList() });
	
	Console.WriteLine(results);
    Console.WriteLine(results2);
}

// Define other methods and classes here
class Person { 
    internal int PersonID; 
    internal string car; 
}