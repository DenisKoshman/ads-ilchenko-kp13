using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab07
{
    internal class Hashtable
    {
        const int AlphabetPower = 27;

        int size = 0;
        float loadness = 0f;
        int capacity = 16;

        Entry[] table;
        
        public Hashtable()
        { 
            this.table = new Entry[this.capacity];
            for (int i = 0; i < this.capacity; i++)
                table[i] = new Entry();
        }

        public void insertEntry(Key key, Value value)
        {
            int possition = GetHash(key);
            var currentState = table[possition].state.Value;

            if(currentState == State.Empty || currentState == State.Deleted)
            {
                this.table[possition].SetFields(key, value);
                this.table[possition].state.Value = State.Filled;
                this.size++;
                this.loadness = (float)size / table.Length;
                this.capacity--;

                if (loadness > 0.6)
                    rehashing();

                return;
            }
                
            if(currentState == State.Filled && this.capacity != 0)
            {
                while(currentState == State.Filled)
                {
                    if(possition == (table.Length - 1) )
                        possition = 0;
                    else
                        possition++;

                    currentState = this.table[possition].state.Value;
                }
                table[possition].SetFields(key, value);
                this.table[possition].state.Value = State.Filled;
                this.size++;
                this.loadness = (float)size / table.Length;
                this.capacity--;
                if (loadness > 0.6)
                    rehashing();
            }
        }

        public void insertEntry(Key key, Value value, Entry[] customTable)
        {
            int possition = GetHash(key, customTable.Length);

            if (customTable[possition].state.Value == State.Filled)
            {
                while (customTable[possition].state.Value != State.Empty)
                {
                    if (possition == (customTable.Length - 1))
                        possition = 0;
                    else
                        possition++;
                }
            }

            customTable[possition].SetFields(key, value);
            customTable[possition].state.Value = State.Filled;

            this.size++;
            this.loadness = (float)size / customTable.Length;
            this.capacity--;

        }

        public void removeEntry(Key key)
        {

            int position = Array.IndexOf(this.table, findEntry(key));

            if (position != -1)
                this.table[position].RemoveFields();


        }

        public Entry findEntry(Key key)
        {
            int possition = GetHash(key);

            int currentPosition = possition;

            if(GetHash(this.table[currentPosition].key) != GetHash(key))
            {
                while(GetHash(this.table[currentPosition].key) != possition)
                {
                    if (currentPosition == (table.Length - 1))
                        currentPosition = 0;
                    else
                        currentPosition++;

                    if(currentPosition == possition)
                    {
                        Console.WriteLine("User was not found");
                        return null;
                    }
                }
            }

            return this.table[currentPosition];
        }

        public Entry findEntry(Entry entry)
        {
            int possition = GetHash(entry.key);

            int currentPosition = possition;

            if (GetHash(this.table[currentPosition].key) != GetHash(entry.key))
            {
                while (GetHash(this.table[currentPosition].key) != possition && this.table[currentPosition].value != entry.value)
                {
                    if (currentPosition == (table.Length - 1))
                        currentPosition = 0;
                    else
                        currentPosition++;

                    if (currentPosition == possition)
                    {
                        Console.WriteLine("User was not found");
                        return null;
                    }
                }
            }

            return this.table[currentPosition];
        }

        public bool CheckEntry(Entry entry)
        {
            for(int i = 0; i < table.Length; i++)
            {
                if (table[i] == entry)
                    return true;
            }

            return false;
        }

        void rehashing()
        {
            Entry[] newTable = new Entry[this.table.Length*2];
            for(int i =0;i<newTable.Length;i++)
                newTable[i] = new Entry();
                
            this.capacity = newTable.Length;
            this.size = 0;
            this.loadness = 0;
            

            for(int i = 0; i < this.table.Length; i++)
            {
                if(this.table[i].key != new Key())
                {
                    Entry currentEntry = this.table[i];
                    insertEntry(currentEntry.key, currentEntry.value, newTable);
                }
            }

            this.table = newTable;
        }



        long hashCode(Key key)
        {
            long hash = 0;
            string hashable = key.FirstName + key.Surname;

            for (int i = 0; i < hashable.Length; i++)
                hash += ((long)(int)hashable[i]) * (long)Math.Pow(4, hashable.Length - (i + 1));

            return hash;
        }

        public int GetHash(Key key)
        {
            return (int)(hashCode(key) % table.Length);
        }

        int GetHash(Key key, int customTableLength)
        {
            return (int)(hashCode(key) % customTableLength);
        }

        public void Print()
        {
            for(int i = 0; i < this.table.Length; i++)
            { 
                Entry entry = this.table[i];
                Console.WriteLine($"{Convert.ToString(entry.key.FirstName), 15}\t  {Convert.ToString(entry.key.Surname),15}\t|   {Convert.ToString(entry.value.EmailAddress),10},\t {Convert.ToString(entry.value.HahedPass),10}");
            }
        }

    }

    class Entry
    {
        public Key key = new Key();
        public Value value = new Value();

        public State state = new State();

        public Entry() { }

        public Entry(Key key, Value value)
        {
            this.key.SetFields(key);
            this.value.SetFields(value);
        }

        public void SetFields (Key key, Value value)
        {
            this.key.SetFields(key);
            this.value.SetFields(value);
        }

        public void RemoveFields()
        {
            this.key = new Key();
            this.value = new Value();

            this.state.Value = State.Deleted;
        }

        public void SetFields(Entry entry)
        {
            this.SetFields(entry.key, entry.value);
        }

        public static bool operator ==(Entry entry1, Entry entry2)
        {
            if (entry1.key == entry2.key && entry1.value == entry2.value)
                return true;
            else
                return false;
        }

        public static bool operator != (Entry entry1, Entry entry2)
        {
            if (entry1.key != entry2.key || entry1.value != entry2.value)
                return true;
            else
                return false;
        }

    }

    class Key
    {
        public string FirstName;
        public string Surname;


        public Key() {}
        public Key(string fn, string sur)
        {
            this.FirstName = fn;
            this.Surname = sur;
        }

        public void SetFields(Key anotherKey)
        {
            this.FirstName = anotherKey.FirstName;
            this.Surname = anotherKey.Surname;
        }

        public static bool operator ==(Key key1, Key key2)
        {
            if (key1.FirstName == key2.FirstName && key1.Surname == key2.Surname)
                return true;
            else
                return false;
        }

        public static bool operator !=(Key key1, Key key2)
        {
            if (key1.FirstName != key2.FirstName || key1.Surname != key2.Surname)
                return true;
            else
                return false;
        }
    }

    class Value
    {
        public long HahedPass;
        public string EmailAddress;

        public List<Entry> Friends = new List<Entry>();

        public void ShowFriendList()
        {
            foreach (Entry entry in Friends)
            {
                Console.WriteLine($"{Convert.ToString(entry.key.FirstName),15}\t  {Convert.ToString(entry.key.Surname),15}\t|   {Convert.ToString(entry.value.EmailAddress),10},\t {Convert.ToString(entry.value.HahedPass),10}");
            }
        }

        public void SetFields(Value anotherValue)
        {
            this.HahedPass = anotherValue.HahedPass;
            this.EmailAddress = anotherValue.EmailAddress;
        }

        public Value() { }
        public Value(long hashpsss, string emailAdd)
        {
            this.HahedPass = hashpsss;
            this.EmailAddress = emailAdd;
        }
        public static bool operator ==(Value value1, Value value2)
        {
            if (value1.HahedPass == value2.HahedPass && value1.EmailAddress == value2.EmailAddress)
                return true;
            else
                return false;
        }

        public static bool operator !=(Value value1, Value value2)
        {
            if (value1.HahedPass != value2.HahedPass || value1.EmailAddress != value2.EmailAddress)
                return true;
            else
                return false;
        }
    }

    class State
    {
        public string Value;

        public State()
        {
            this.Value = State.Empty;
        }

        const string empty = "Empty";
        const string deleted = "Deleted";
        const string filled = "Filled";

        public static string Empty { get { return empty; } }
        public static string Deleted { get { return deleted; } }
        public static string Filled { get { return filled; } }
    }
}
