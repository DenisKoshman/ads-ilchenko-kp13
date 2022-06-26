using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab07
{
    internal class Program
    {

        static Hashtable Network = new Hashtable();

        static public void ControlFill()
        {
            Network.insertEntry(new Key("Anatolii", "Akimenko"), new Value(HashPassword("000"), "email"));
            Network.insertEntry(new Key("Maria", "Viktorenko"), new Value(HashPassword("001"), "email"));
            Network.insertEntry(new Key("Olga", "Vovk"), new Value(HashPassword("010"), "email"));
            Network.insertEntry(new Key("Julia", "Gorenko"), new Value(HashPassword("011"), "email"));
            Network.insertEntry(new Key("Tetiana", "Gavrilyuk"), new Value(HashPassword("100"), "email"));
            Network.insertEntry(new Key("Nadiya", "Daragan"), new Value(HashPassword("101"), "email"));
            Network.insertEntry(new Key("Viacheslav", "D'yachenko"), new Value(HashPassword("110"), "email"));
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            Console.WriteLine("1. Контрольне заповнення\n" +
                "2. Додати користувача до бази\n" +
                "3. Видалити елемент за ключем\n" +
                "4. Знайти елемент за ключем\n" +
                "5. Авторизуватися\n" +
                "6. Вивести таблицю\n" +
                "7. Завершити\n");

            string result = Console.ReadLine();
            Entry user = null;

            while(result != "Завершити")
            {
                switch(result)
                {
                    case "1":
                        ControlFill();
                        Console.WriteLine("Заповнено");
                        break;

                    case "2":
                        Entry newUser = GetUser();
                        Network.insertEntry(newUser.key, newUser.value);
                        Console.WriteLine("Користувача успішно додано");
                        break ;

                    case "3":
                        Key key = GetKey();
                        Network.removeEntry(key);
                        Console.WriteLine("Видалено");
                        break;

                    case "4":
                        Key key1 = GetKey();
                        Entry entry = Network.findEntry(key1);
                        Console.WriteLine($"{Convert.ToString(entry.key.FirstName),15}\t  {Convert.ToString(entry.key.Surname),15}\t|   {Convert.ToString(entry.value.EmailAddress),10},\t {Convert.ToString(entry.value.HahedPass),10}");
                        break;

                    case "5":
                        user = LogIn();
                        Console.WriteLine("Ви успішно авторизувались");
                        Console.WriteLine("1. Контрольне заповнення\n" +
                "2. Додати користувача до бази\n" +
                "3. Видалити елемент за ключем\n" +
                "4. Знайти елемент за ключем\n" +
                "5. Авторизуватися\n" +
                "6. Вивести таблицю\n" +
                "7. Завершити\n"+
                "8. Додати друга\n"+
                "9. Видалити друга\n"+
                "10. Показати список друзів\n");
                        break;

                    case "6":
                        Network.Print();
                        break;

                    case "7":
                        return;
                        break;

                    case "8":
                        Key friend = GetKey();
                        Entry findedEntry = Network.findEntry(friend);
                            user.value.Friends.Add(findedEntry);
                        Console.WriteLine("Друга додано");
                        break;

                    case "9":
                        Key friend1 = GetKey();
                        Entry findedEntry1 = Network.findEntry(friend1);
                        if ( user.value.Friends.Contains(findedEntry1))
                            user.value.Friends.Remove(findedEntry1);
                        else
                            Console.WriteLine("Користувача не знайдено");
                        Console.WriteLine("Друга видалено");

                        break;

                    case "10":
                        user.value.ShowFriendList();
                        break;
                }
                result = Console.ReadLine();
            }

            Console.WriteLine(Network.findEntry(user).key.FirstName);
            Console.ReadLine();
        }

        static void PrintUserInterface()
        {

        }

        static Entry LogIn()
        {
            Key key = new Key();
            Value value = new Value();

            Console.Write("Введіть дані про себе щоб авторизуватися:\n");

            key.FirstName = getInput("FirstName");
            key.Surname = getInput("Surname");
            value.EmailAddress = getInput("Email");
            value.HahedPass = HashPassword(getInput("Password"));


            string getInput(string Field)
            {
                switch(Field) 
                {
                    case "FirstName":
                        Console.Write("Ім'я: ");
                        break;

                    case "Surname":
                        Console.Write("Прізвище: ");
                        break;

                    case "Email":
                        Console.Write("Email: ");
                        break;

                    case "Password":
                        Console.Write("Пароль: ");
                        break;
                }
                
                
                string result = Console.ReadLine();

                if (Check(result))
                    return result;
                else
                {
                    Console.WriteLine("Ви нічого не ввели, спробуйте ще раз");
                    return getInput(Field);
                }

                
            }

            Entry entry = new Entry(key, value);

            if(CheckLogIn(entry) == false)
            {
                Console.WriteLine("Користувача не знайдено, спробуйте ще раз");
                return LogIn();
            }
            else
            {
                return entry;
            }

        }
        static public bool Check(string s)
        {
            return (s == "" ? false : true);
        }
        static public Entry GetUser()
        {
            Key key = new Key();
            Value value = new Value();

            Console.Write("Введіть необхідні дані:\n");

            key.FirstName = getInput("FirstName");
            key.Surname = getInput("Surname");
            value.EmailAddress = getInput("Email");
            value.HahedPass = HashPassword(getInput("Password"));


            string getInput(string Field)
            {
                switch (Field)
                {
                    case "FirstName":
                        Console.Write("Ім'я: ");
                        break;

                    case "Surname":
                        Console.Write("Прізвище: ");
                        break;

                    case "Email":
                        Console.Write("Email: ");
                        break;

                    case "Password":
                        Console.Write("Пароль: ");
                        break;
                }


                string result = Console.ReadLine();

                if (Check(result))
                    return result;
                else
                {
                    Console.WriteLine("Ви нічого не ввели, спробуйте ще раз");
                    return getInput(Field);
                }
            }



            Entry entry = new Entry(key, value);
            return entry;
        }

        static public Key GetKey()
        {
            Key key = new Key();
            Console.Write("Введіть необхідні дані:\n");

            key.FirstName = getInput("FirstName");
            key.Surname = getInput("Surname");

            string getInput(string Field)
            {
                switch (Field)
                {
                    case "FirstName":
                        Console.Write("Ім'я: ");
                        break;

                    case "Surname":
                        Console.Write("Прізвище: ");
                        break;
                }


                string result = Console.ReadLine();

                if (Check(result))
                    return result;
                else
                {
                    Console.WriteLine("Ви нічого не ввели, спробуйте ще раз");
                    return getInput(Field);
                }
            }

            return key;
        }
        static long HashPassword(string password)
        {
            long hash = 0;

            for (int i = 0; i < password.Length; i++)
                hash += (long)((int)password[i] * Math.Pow(4, password.Length - (i + 1)));

            return hash;
        }

        static public bool CheckLogIn(Entry entry)
        {
            return Network.CheckEntry(entry);
        }
    }
}
