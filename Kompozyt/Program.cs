using System;
using System.Collections.Generic;

namespace GameSystem
{
    abstract class Module
    {
        public abstract string Operate();

        public virtual void Add(Module module)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(Module module)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsComposite()
        {
            return false;
        }
    }

    class SingleModule : Module
    {
        private string _name;

        public SingleModule(string name)
        {
            _name = name;
        }

        public override string Operate()
        {
            return _name;
        }
    }

    class CompositeModule : Module
    {
        private List<Module> _subModules = new List<Module>();
        private string _name;

        public CompositeModule(string name)
        {
            _name = name;
        }

        public override void Add(Module module)
        {
            _subModules.Add(module);
        }

        public override void Remove(Module module)
        {
            _subModules.Remove(module);
        }

        public override bool IsComposite()
        {
            return true;
        }

        public override string Operate()
        {
            string result = $"{_name}(";
            for (int i = 0; i < _subModules.Count; i++)
            {
                result += _subModules[i].Operate();
                if (i < _subModules.Count - 1)
                {
                    result += ", ";
                }
            }
            return result + ")";
        }
    }

    class Client
    {
        public void Execute(Module module)
        {
            Console.WriteLine($"Module operation: {module.Operate()}");
        }

        public void AddModule(Module composite, Module subModule)
        {
            if (composite.IsComposite())
            {
                composite.Add(subModule);
                Console.WriteLine($"Added module: {subModule.Operate()} to {composite.Operate()}");
            }
            else
            {
                Console.WriteLine($"Cannot add module to {composite.Operate()}, as it is not composite.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Module weapon = new SingleModule("Laser Cannon");
            Module armor = new SingleModule("Titanium Armor");
            Module shield = new SingleModule("Energy Shield");

            CompositeModule defenseSystem = new CompositeModule("Defense System");
            defenseSystem.Add(armor);
            defenseSystem.Add(shield);

            CompositeModule spaceship = new CompositeModule("Spaceship");
            spaceship.Add(weapon);
            spaceship.Add(defenseSystem);

            Console.WriteLine("=== Testowanie pojedynczego modułu ===");
            client.Execute(weapon);

            Console.WriteLine("\n=== Testowanie złożonego modułu ===");
            client.Execute(spaceship);

            Console.WriteLine("\n=== Dodawanie modułów ===");
            Module engine = new SingleModule("Quantum Engine");
            client.AddModule(spaceship, engine);
            client.Execute(spaceship);
        }
    }
}
