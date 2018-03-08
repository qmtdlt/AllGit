using System;
namespace ConsoleTestApp
{
    public enum Animal
    {
        Dog = 1,
        Cat,
        Bird,
    }
    public class AnimalTypeAttribute : Attribute
    {
        //构造函数，接受已给 Animal
        public AnimalTypeAttribute(Animal pet)
        {
            thePet = pet;
        }
        //成员变量
        protected Animal thePet;
        //属性pet
        public Animal Pet
        {
            get { return thePet;}
            set { thePet = value; }
        }
    }
    class AniMalTypeTestClass
    {
        [AnimalType(Animal.Dog)]
        public void DogMethod() { }
        [AnimalType(Animal.Cat)]
        public void CatMethod() { }
        [AnimalType(Animal.Bird)]
        public void BirdMethod() { }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AniMalTypeTestClass testClass = new AniMalTypeTestClass();
            Type type = testClass.GetType();
            foreach (var item in type.GetMethods())
            {
                foreach (var attr in Attribute.GetCustomAttributes(item))
                {
                    if(attr.GetType() == typeof(AnimalTypeAttribute))
                    {
                        Console.WriteLine("Method {0} has a pet {1} attribute.",item.Name,((AnimalTypeAttribute)attr).Pet);
                    }
                }
            }
        }
    }
}
