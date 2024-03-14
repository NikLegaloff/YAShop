namespace YAShop.Common;

public class Dog
{
    public Dog(){}
    public Dog(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Name { get; set; }
    public int Age{ get; set; }
}
public class Сrib
{
    public void Numbers()
    {
        var sum = "1,2,3,4,5,6,7,7,8,8,8".Split(',').Distinct().Select(s=>int.Parse(s)).Where(n=>n>2).Sum(n=>n);
    }

    public void Dogs()
    {
        var dogs = "Nik,2;Josef,3".Split(';').Select(s => new Dog { Name = s.Split(',')[0], Age = int.Parse(s.Split(',')[0])}).ToList();
        dogs.Add(new Dog{Name = "Bobic",Age = 12});
        dogs.Insert(0,new Dog("Tuzic",4));
        var first = dogs.First();
        var first2 = dogs.Take(2).ToArray();
        var last3 = dogs.TakeLast(3).ToArray();
        var allExceptFirst2 = dogs.Skip(2);

        dogs.Sort((d1, d2) => d1.Name.CompareTo(d2.Name));
        dogs.Reverse();

        dogs.RemoveAll(d => d.Age > 10);

        var nik = dogs.Find(d => d.Name == "Nik");
        var oldestName = (from dog in dogs where dog.Age == dogs.Select(d=>d.Age).Max() select dog.Name).First();
        var ageSum = dogs.Sum(d => d.Age);
    }
}