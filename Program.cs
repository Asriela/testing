
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sololearn
{

    class Program
    {
        static void Main(string[] args)
        {
        /*
        choose who to kiss!
        1- Alshi
        2-Tom
        */
        int input = 1;
        //Convert.ToInt32(Console.ReadLine())
        Generate generate = new Generate(
            input
          );
           
           
        }
    }
    
    class Generate
    {
    enum group{
friends,
family,
crushes,
enemies
}
    private int userInput;
    public const int NO_OF_PEOPLE = 5;
    public const int NO_OF_ROOMS = 4;
    private List <Person> [] roomsArr = new List <Person>[NO_OF_ROOMS ];
    private Person[] people = new Person[NO_OF_PEOPLE];
   private Random rnd = new Random();
    
    public Generate(int userInput)
    {
    
    this.userInput = userInput;
    
    
    
    GeneratePeople();
    PlacePeople();
  //  SimulateInteractions();
        
    }
    
    private void GeneratePeople(){
    int i=1;
    //generate each person
        foreach(Person person in people){
            person.id=i;
            person.name= GetName(i);
           Person.personList.Add(person); 
           i++;
            }
            
            //setup everyones relationships
       foreach(Person person in Person.personList)
       {
           DeterminePersonsRelationships(person.enemiesList );
           DeterminePersonsRelationships(person.friendsList );
           DeterminePersonsRelationships(person.crushList );
           DeterminePersonsRelationships(person.familyList  );
           
       }
            
            
        }
        
        private void DeterminePersonsRelationships ( List <Person> relationshipList){
        //make a temp list
        List <Person> tempList = new List <Person> (Person.personList);
        
        //add a random amount of people to this relationship list
        int randomPerson;
        for(int i = 0; i<rnd.Next(NO_OF_PEOPLE); i++)
        {
        //add a random person from our temp list
           randomPerson =rnd.Next(tempList.Count); relationshipList.Add(tempList[randomPerson]);
            
            //remove said person from our list
            tempList 
            .RemoveAt(randomPerson);
            
            
        }
        }
    //place the guests in different rooms so that only some witness the player kissing the chosen person
    
      private void PlacePeople(){
       for( int i =0 ; i< NO_OF_ROOMS; i++ ){
       roomsArr[i] = new List <Person>() ;
       
        }
        List <Person> tempList = new List <Person> (Person.personList);
        int j = 0;
        foreach(Person guest in tempList )
        {
            roomsArr[rnd.Next(tempList.Count)].Add(guest);
            tempList.RemoveAt(j);
            j++;
            
        }
            
        
            
        
            
        
    
        }
        
        private string GetName(int id){
        string name= "unknown";
        
         name= (id==1)? "Sam":
               (id==2)? "John":
               (id==3)? "Keen":
               (id==4)? "Sarah":
               (id==5)? "Mana":
               "Arine";
        return name;
            
        }
        
    }
    
    class Person{
    //track persons 
    static public List <Person> personList = new List <Person> ();
    //name
    public string name;
    //id<Person>
    public int id;
    //list functionality for friends
    public List <Person> friendsList= new List <Person> (); 
    //list functionality for enemies
    public List <Person> enemiesList= new List <Person> ();
    //for those im in love with 
    public List <Person> crushList= new List <Person> ();
    //for those im family 
    public List <Person> familyList= new List <Person> ();
    }
}

