using ConsoleEntityFramework;

internal class Program
{
    private static void Main(string[] args)
    {
        PeopleDatabaseContext context = new PeopleDatabaseContext();

        Person person = new Person() { Age= new Random().Next(5, 100), Name = "Moe", Salary = new Random().Next(5,60) };
        context.people.Add(person);

        Home home = new Home()
        {
            Address = "main street"
        };
         context.homes.Add(home);

        context.SaveChanges();

        Person fetchedPerson = (from p in context.people where p.Id == 1 select p).FirstOrDefault<Person>();

        if (fetchedPerson != null )
        {
            Console.WriteLine(fetchedPerson.Name);
            fetchedPerson.Salary = 1000;
            context.SaveChanges();

            Console.WriteLine("It is updated");
        }

        var personDelete =(from p in context.people where p.Id == 2 select p).FirstOrDefault<Person>();
        var personDelete2 = context.people.Where(p=> p.Id ==3).FirstOrDefault<Person>();

        if (personDelete2 != null )
        {
            context.people.Remove(personDelete2);
            context.SaveChanges() ;
        }
        else
        {
            Console.WriteLine("No record of ID");
        }

        // Fetch all the records
        List<Person> peoples = (from p in context.people select p).ToList();
        List<Person> peoples2 = context.people.ToList();

        peoples2.ForEach(p => { Console.WriteLine($"{p.Name} Id: {p.Id} Salary: {p.Salary}"); });
    }
}