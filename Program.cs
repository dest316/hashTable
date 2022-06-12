using System;
using Laba7__HashTable_;

namespace TestEnviroment
{
    class Program
    {
        static void Main(string[] args)
        {
            HashTable hashTable = new HashTable();
            for (int i = 0; i < 40; i++)
            {
                hashTable.Add(new User { password = "qwerty", nickname = "test_user", phoneNumber = $"+7908455551{i}"});
            }
            hashTable.Delete(new User { nickname = "test_name", password = "qwerty", phoneNumber = "+7908455545" });
            Console.WriteLine(hashTable.Search(new User { password = "qwerty", nickname = "test_user", phoneNumber = "+79084555513" }));
            Console.WriteLine(hashTable.Search(new User { password = "qwerty", nickname = "test_user", phoneNumber = "+79084555523" }));
            for (int i = 0; i < 30; i++)
            {
                hashTable.Delete(new User { password = "qwerty", nickname = "test_user", phoneNumber = $"+7908455551{i}" });
            }     
            hashTable.Print();
        }
        
    }
}
