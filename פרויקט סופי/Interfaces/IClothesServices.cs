
using שיעור_3.Models;
using System.Collections.Generic;
using System.Linq;


namespace שיעור_3.Interfaces{
public interface IClothesServices{
        public List<MyClothes> GetAll(int index);

    public MyClothes Get(int id);

    public void Add(MyClothes clothes);

    public void Delete(int id);

    public void Update(MyClothes clothes);

    public int Count { get;}
}}