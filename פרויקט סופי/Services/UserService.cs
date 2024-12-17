using שיעור_3.Models;
using שיעור_3.Interfaces;
using System.Collections.Generic;
using System.Linq;
//להוסיף
using System.Text.Json;

namespace שיעור_3.Services{
public class UserService :IUserServices
    {
        List<MyUser> arr { get; }
        int nextId = 3;

        //להוסיף
        private string fileName="users.json";
        public UserService()
        {
            this.fileName = Path.Combine("data","users.json");
            using(var jsonFile = File.OpenText(fileName)){
                arr=JsonSerializer.Deserialize<List<MyUser>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions{
                    PropertyNameCaseInsensitive = true
                });
            }
            
            // arr = new List<MyUser>
            // {
            //     new MyUser { Id = 1, Name = "Classic Italian"},
            //     new MyUser { Id = 2, Name = "Classic Italian"},
            //     new MyUser { Id = 3, Name = "Classic Italian"},
            //     new MyUser { Id = 4, Name = "Classic Italian"},
            // };
        }
  

private void saveToFile()
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(arr));
        }

        public List<MyUser> GetAll()
        {
            return arr;
        }

        public MyUser Get(int id)
        {
            return arr.FirstOrDefault(p => p.Id == id);
        }

      public int Add(MyUser user)
    {
        if (arr.Count == 0)

        {
            user.Id = 1;
        }
        else
        {
            user.Id = arr.Max(p => p.Id) + 1;

        }

        arr.Add(user);
        saveToFile();
        return user.Id;
    }

        public void Delete(int id)
        {
            var user = Get(id);
            if(user is null)
                return;

            arr.Remove(user);
        }

        public void Update(MyUser user)
        {
            var index = arr.FindIndex(p => p.Id == user.Id);
            if(index == -1)
                return;

            arr[index] = user;
            saveToFile();
        }

        public int Count { get =>  arr.Count();}

        public int ExistUser(string name,string password ){
             MyUser existUser=arr.FirstOrDefault(u => u.Name.Equals(name) && u.Password.Equals(password));
             if(existUser!=null)
                 return existUser.Id;
             return -1;

        }
    }
}