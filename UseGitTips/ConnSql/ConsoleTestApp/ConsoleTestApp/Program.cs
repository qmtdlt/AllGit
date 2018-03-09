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
        public AnimalTypeAttribute(Animal pet)
        {
            thePet = pet;
        }
        protected Animal thePet
        {
            get { return thePet; }
            set { thePet = value; }
        }
    }
    class AnimalTypeTestClass
    {
        [AnimalType(Animal.Dog)]
        public void DogMethod() { }
        [AnimalType(Animal.Cat)]
        public void CagMethod() { }
        [AnimalType(Animal.Bird)]
        public void BirdMethod() { }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AnimalTypeTestClass testClass = new AnimalTypeTestClass();
            Type type = testClass.GetType();
            foreach (Method mInfo in type.GetMethods())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    if (attr.GetType() == typeof(AnimalTypeAttribute))
                    {
                        Console.WriteLine("Method {0} has a pet {1} attribute.",mInfo.name,((AnimalTypeAttribute)attr).Pet);
                    }
                }
            }

            foreach (MethodvInfo mInfo in type.GetMethods())
            {
                // Iterate through all the Attributes for each method. 
                foreach (Attribute attr in
                    Attribute.GetCustomAttributes(mInfo))
                {
                    // Check for the AnimalType attribute. 
                    if (attr.GetType() == typeof(AnimalTypeAttribute))
                        Console.WriteLine(
                            "Method {0} has a pet {1} attribute.",
                            mInfo.Name, ((AnimalTypeAttribute)attr).Pet);
                }

            }

        }
    }
}
