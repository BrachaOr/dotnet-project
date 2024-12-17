
using שיעור_3.Models;
using System.Collections.Generic;
using System.Linq;


namespace שיעור_3.Interfaces{
public interface IUserServices{
     List<MyUser> GetAll();

    MyUser Get(int id);

     int Add(MyUser user);

     void Delete(int id);

     void Update(MyUser user);

     int Count { get;}
    int ExistUser(string name, string password);
}}