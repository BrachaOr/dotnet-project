using שיעור_3.Models;
using שיעור_3.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using שיעור_3.Services;


namespace שיעור_3.Services{
public class ClothesService :IClothesServices
    {
        List<MyClothes> arr { get; }
        
        private string fileName="clothes.json";
        int nextId = 3;
        public ClothesService()

        {
            this.fileName = Path.Combine("data","clothes.json");
            using(var jsonFile = File.OpenText(fileName)){
                arr=JsonSerializer.Deserialize<List<MyClothes>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions{
                    PropertyNameCaseInsensitive = true
                });
            }
        }
          private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(arr));
    }
       public List<MyClothes> GetAll(int index)
        {
            if(index!=-1)
                return arr.FindAll(c=>c.userId==index);
            return arr;

        }

        public MyClothes Get(int id)
        {
            return arr.FirstOrDefault(p => p.Id == id);
        }

        public void Add(MyClothes clothes)
        {
            clothes.Id = nextId++;
            arr.Add(clothes);
        }

        public void Delete(int id)
        {
            var clothes = Get(id);
            if(clothes is null)
                return;

            arr.Remove(clothes);
        }

        public void Update(MyClothes clothes)
        {
            var index = arr.FindIndex(p => p.Id == clothes.Id);
            if(index == -1)
                return;

            arr[index] = clothes;
        }

        public int Count { get =>  arr.Count();}
    }
}