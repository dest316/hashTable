using System;

namespace Laba7__HashTable_
{
    public class User
    {
        public string phoneNumber { get; set; }
        public string nickname { get; set; }
        public string password { get; set; }
        public static bool operator ==(User user1, User user2) => !(user1 is null) && !(user2 is null) && user1.phoneNumber == user2.phoneNumber;
        public static bool operator !=(User user1, User user2) => !(user1 is null) && !(user2 is null) && user1.phoneNumber != user2.phoneNumber;
        public User()
        {
            phoneNumber = "";
            nickname = "";
            password = "";
        }
    }
    public class HashTableNode
    {
        public User data { get; set; }
        public int state { get; set; }
        public string key { get; set; }
        public HashTableNode()
        {
            data = new User();
            state = 0;
            key = "";
        }
        public override string ToString()
        {
            return $"{data.nickname} {data.password} {data.phoneNumber}\t {state}";
        }
    }
    public class HashTable
    {
        private int capacity;
        private int length;
        private HashTableNode[] elements;
        public HashTable()
        {
            capacity = 20;
            length = 0;
            elements = new HashTableNode[capacity];
            for (int i = 0; i < capacity; i++)
            {
                elements[i] = new HashTableNode();
            }
        }
        private int TestHash(User number)
        {
            var phone_number = number.phoneNumber;
            var hash = 0;
            for (int i = phone_number.Length - 1; i >= phone_number.Length - 5; i--) { hash += Convert.ToInt32(phone_number[i]); } 
            return hash % capacity;
        }
        private int FirstHash(string number)
        {
            var tmp = 0;
            for (int i = number.Length; i >= number.Length - 5; i--) { tmp += Convert.ToInt32(number[i]); }
            return Convert.ToInt32(Math.Pow(tmp, 2)) % capacity;
        }
        private int SecondHash(int step, int index)
        {

            if (step > capacity || step < 1) { step = 1; }
            while (capacity % step == 0 && step != 1) { step++; }
            index += step;
            if (index > (capacity - 1)) { index -= capacity; }
            return index;
        }
        public void Add(User newUser)
        {
            var hash = TestHash(newUser);
            var step = 2; //пока захардкодил 2
            while (elements[hash].state == 1)
            {
                hash = SecondHash(step, hash);
                if (elements[hash].data == newUser) { return; }
            }
            elements[hash].data = newUser;
            elements[hash].key = newUser.phoneNumber;
            elements[hash].state = 1;
            length++;
            if ((double)length / capacity > 0.75) { Rehash(true); }
        }
        public void Print()
        {
            for (int i = 0; i < capacity; i++)
            {
                Console.WriteLine(elements[i]);
            }
            Console.WriteLine();
        }
        public void Delete(User deletedUser)
        {
            var hash = TestHash(deletedUser);
            var step = 2;
            while (elements[hash].state != 0 || elements[hash].data.phoneNumber !="")
            {
                if (elements[hash].data == deletedUser)
                {
                    if (elements[hash].state == 1) 
                    {
                        elements[hash].state = 0;
                        length--;
                    }
                    if ((double)length / capacity < 0.2 && capacity > 20)
                    { Rehash(false); }
                    return;
                }
                hash = SecondHash(step, hash);
            }
        }
        public int Search(User soughtUser)
        {
            var hash = TestHash(soughtUser);
            var step = 2;
            while (elements[hash].state != 0 || elements[hash].data.phoneNumber != "")
            {
                if (elements[hash].data == soughtUser) { return 0; }
                hash = SecondHash(step, hash);
            }
            return -1;
        }
        private void Rehash(bool increase)
        {
            length = 0;
            int newCapacity = increase ? capacity * 2 : capacity / 2;
            if (newCapacity < 20) { return; }
            var tmp = new HashTableNode[elements.Length];
            Array.Copy(elements, tmp, elements.Length);
            elements = new HashTableNode[newCapacity];
            for (int i = 0; i < newCapacity; i++)
            {
                elements[i] = new HashTableNode();
            }
            capacity = newCapacity;
            foreach (var node in tmp)
            {
                if (!(node.data is null) && node.data.phoneNumber != "" && node.state == 1)
                    Add(node.data);
            }
            
        }
    }

}
