// Created by Asriela Aestas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace AiTest
{
    class Input
    {
        public static int whoWeChoseToKiss = 1;
        static void Main(string[] args)
        {
            /* choose who to kiss!                  1- Alshi 2-Tom */
            //string input = Console.ReadLine();            

            Console.WriteLine($"Your choice: {whoWeChoseToKiss}");
            Game game = new Game(whoWeChoseToKiss);
            Console.ReadKey();

        }
    }



    class Game
    {
        enum Group
        {
            Friend,
            Family,
            Crush,
            Enemy
        };
        private int choice;

        const int PEOPLE_COUNT = 9;
        const int ROOMS_COUNT = 3;
        private List<Person>[] whoIsInEachRoom = Enumerable.Range(0, ROOMS_COUNT).Select(i => new List<Person>()).ToArray();
        private Person[] people = Enumerable.Range(0, PEOPLE_COUNT).Select(i => new Person(i)).ToArray();
        private Random rnd = new Random();
        public Game(int p)
        {
            this.choice = p;
            GenerateIndividuals();
            PutPeopleInRooms();
            EveryoneSharingRoomWithPlayerReactToKiss();
            // SimulateInteractions()
        }

        private void GenerateIndividuals()
        {

            //generate each person 
            foreach (Person guest in people)
            {

                Person.GuestList.Add(guest);

            }
            Console.WriteLine($"You chose to kiss {Person.PersonWeKissedInstance.name}");
            Console.WriteLine("\nPerson.GuestList\n===============\n" + $@"{String.Join("\n ", Person.GuestList)}"); //setup everyones relationships

            foreach (Person guest in Person.GuestList)
            {
                Console.WriteLine("\n" + guest + "\n ===============\n");
                SetupRelationships(guest, guest.enemies, Group.Enemy);
                SetupRelationships(guest, guest.friends, Group.Friend);
                SetupRelationships(guest, guest.crushes, Group.Crush);

                SetupRelationships(guest, guest.family, Group.Family);
            }
        }

        private void SetupRelationships(Person subject, List<Person> rList, Group g)
        {

            List<Person> tempList = new List<Person>(Person.GuestList); //add a random amount of people
            int randomPerson;
            for (int i = 0; i < tempList.Count; i++)
            {
                //add a random person from tempList

                randomPerson = rnd.Next(tempList.Count);
                //ANTI - INCESTUOUS CODE
                if (!(subject == Person.PersonWeKissedInstance && tempList[randomPerson] == Person.PlayerInstance && rList == subject.family))
                {
                    rList.Add(tempList[randomPerson]); //remove said person from our list  
                    tempList.RemoveAt(randomPerson);
                }
            }
            Console.WriteLine($@"{g}{"\n\t"}{String.Join("\n\t", rList)}");
        }

        private void PutPeopleInRooms()
        {
            int playersRoom = 0;
            int chosenRoom = 0;
            Person.personWeAreKissingsRoom = 0;
            foreach (Person individual in people)
            {
                chosenRoom = rnd.Next(ROOMS_COUNT);
                whoIsInEachRoom[chosenRoom].Add(individual);
                if (individual.id == 0)
                {
                    playersRoom = chosenRoom;
                }
                if (individual.id == Input.whoWeChoseToKiss)
                {
                    Person.personWeAreKissingsRoom = chosenRoom;
                }
            }

            whoIsInEachRoom[playersRoom].Remove(Person.PlayerInstance);
            whoIsInEachRoom[Person.personWeAreKissingsRoom].Add(Person.PlayerInstance);


            int i = 0;
            foreach (List<Person> room in whoIsInEachRoom)
            {
                i++;
                Console.WriteLine($"Room number {i}\n=====\n\t{String.Join<Person>("\n\t", room)}\n\n");
            }
            //apply fisher-yates shuffle 
            //Shuffle(whoIsInEachRoom.ToList()).CopyTo(whoIsInEachRoom, 0);

        }


        private void EveryoneSharingRoomWithPlayerReactToKiss()
        {
            foreach (Person individual in whoIsInEachRoom[Person.personWeAreKissingsRoom])
            {
                if (individual == Person.PlayerInstance) break;
                //react to player

                ReactTo(individual, Person.PlayerInstance, Person.PersonWeKissedInstance);

                //react to person being kissed

                ReactTo(individual, Person.PersonWeKissedInstance, Person.PlayerInstance);
            }

        }

        private void ReactTo(Person theViewer, Person theSubject, Person theObject)
        {
            if (!(theViewer == Person.PersonWeKissedInstance && theViewer != theSubject))
            {

                string comment = "no comment";
                //if we hate them both
                bool objectIsEnemy = theViewer.enemies.Contains(theObject);
                bool objectIsFriend = theViewer.friends.Contains(theObject);
                bool objectIsFamily = theViewer.family.Contains(theObject);
                bool objecttIsCrush = theViewer.crushes.Contains(theObject);

                if (theViewer == theSubject)
                {

                    if (objectIsEnemy && objecttIsCrush) comment = "I hate you you sexy thing!";
                    else if (objectIsEnemy) comment = "Get the fuck away from you, I hate you, you asshole!";
                    else if (objectIsFamily) comment = "Ug! Gross!! What the hell has gotten into you!?";
                    else if (objectIsFriend && !objecttIsCrush) comment = "Woah there pal, I dont see you like that";
                    else if (objecttIsCrush && objectIsFriend) comment = "Hmm ive been waiting for that for years!";
                    else if (objecttIsCrush) comment = "Oh wow what a pleasent surprise";
                    //if () comment = "";
                }
                else
                {

                    bool subjectIsEnemy = theViewer.enemies.Contains(theSubject);
                    bool subjectIsFriend = theViewer.friends.Contains(theSubject);
                    bool subjectIsFamily = theViewer.family.Contains(theSubject);
                    bool subjectIsCrush = theViewer.crushes.Contains(theSubject);

                    //OBJECT ENEMY
                    if (subjectIsEnemy && objectIsEnemy)
                        comment = "wow i guess the loosers are kissing";
                    if ((subjectIsFriend || subjectIsFamily) && objectIsEnemy)
                        comment = "Oh no i cant believe, you would steep that low to kiss them!";
                    if (subjectIsCrush && objectIsEnemy)
                        comment = "NOOO! NOT THEM!? THEYY GET TO HAVE YOU???";

                    //OBJECT FRIEND
                    if (subjectIsEnemy && objectIsFriend)
                        comment = "You stay away from my friend you looser!";
                    if (subjectIsFriend && objectIsFriend)
                        comment = "Aww my best buds budding together!!";
                    if (subjectIsCrush && objectIsFriend)
                        comment = "Im not gonna lie I was gonna make the move on you but I guess its ok that you are with my friend...";
                    if (subjectIsFamily && objectIsFriend)
                        comment = "Heyyyy siblinggg good to see you getting with my bud, just dont make it weird for us ok...";


                    // if ()
                    //    comment = "";
                }
                if (theViewer == theSubject)
                    Console.WriteLine($"==============\n{theViewer.name} reacts to your kiss: \n\t{comment}");
                else
                    Console.WriteLine($"==============\n{theViewer.name} says to {theSubject.name} \n\t{comment}");

            }
        }
    }

    class Person
    {

        public string name;
        public int id;
        public static Person PlayerInstance;
        public static Person PersonWeKissedInstance;
        public static int personWeAreKissingsRoom;
        public override string ToString()
        {
            return $"{name}";
        }
        public List<Person> friends = new List<Person>();
        public List<Person> enemies = new List<Person>();
        public List<Person> crushes = new List<Person>();
        public List<Person> family = new List<Person>();

        static public List<Person> GuestList = new List<Person>();
        public Person(int i)
        {
            id = i;
            name = Names.NamesArray[id];


            if (i == 0)
            {
                PlayerInstance = this;
                Console.WriteLine($"???????\n {PlayerInstance.name}");
            }

            if (i == Input.whoWeChoseToKiss) PersonWeKissedInstance = this;
        }


    }

    static class Names
    {
        public static string[] NamesArray =
        {
            "You",
            "Bob",
            "Sam",
            "Sarah",
            "Keen",
            "Mara",
            "Arine",
            "John",
            "Michael",
            "Nichole",
            "Nemi"
        };
    }
}




