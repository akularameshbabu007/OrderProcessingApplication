using System;
using System.Collections.Generic;
using static OrderProcessingApplication.Program;

namespace OrderProcessingApplication
{
    public abstract class Product
    {
        public string ItemName { get; set; }
        public List<string> Operations { get; set; }
        public abstract void GetSlip();
    }
    public abstract class PhysicalProduct : Product
    {
        public override void GetSlip()
        {
            Operations.Add("Generated a packing slip for shipping.");
            Console.WriteLine("Generated a packing slip for shipping.");
        }
        public virtual void AddCommission()
        {
            Operations.Add("Commission payment to the agent");
            Console.WriteLine("Commission payment to the agent");
        }
    }
    public abstract class NonPhysicalProduct : Product
    {
        public override void GetSlip()
        {
            Operations.Add("Generated a packing slip.");
            Console.WriteLine("Generated a packing slip.");
        }
        public virtual void DropMail()
        {
            Operations.Add("Mail Sent");
            Console.WriteLine("Mail Sent");
        }

    }

    class Video : NonPhysicalProduct
    {
        public Video(string videoName)
        {
            Operations = new List<string>();
            ItemName = videoName;

            GetSlip();
        }
        public override void GetSlip()
        {
            base.GetSlip();
            if (ItemName.ToLower().Equals("learning to ski"))
            {
                Operations.Add("'First Aid' video added to the packing slip");
                Console.WriteLine("'First Aid' video added to the packing slip");
            }
        }
    }
    class Membership : NonPhysicalProduct
    {
        public Membership()
        {
            Operations = new List<string>();
            base.GetSlip();
            Operations.Add("Activate that membership");
            Console.WriteLine("Activate that membership");
            base.DropMail();
        }
    }
    class Upgrade : NonPhysicalProduct
    {
        public Upgrade()
        {
            Operations = new List<string>();
            base.GetSlip();
            Operations.Add("Apply the upgrade");
            Console.WriteLine("Apply the upgrade");
            base.DropMail();
        }
    }
    class Book : PhysicalProduct
    {
        public Book(string itemName)
        {
            ItemName = itemName;
            Operations = new List<string>();
            base.GetSlip();
            base.AddCommission();
            GetSlip();
        }
        public override void GetSlip()
        {
            Operations.Add("Create a duplicate packing slip for the royalty department.");
            Console.WriteLine("Create a duplicate packing slip for the royalty department.");
        }
    }

    class Other : PhysicalProduct
    {
        public Other(string name)
        {
            ItemName = name;
            Operations = new List<string>();
            base.GetSlip();
            base.AddCommission();
        }
    }

    class Program
    {
        public enum ProductTypes
        {
            Video,
            Membership,
            Upgrade,
            Book,
            Other
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Product type and name (if applicable) seperated by space");
            var input = Console.ReadLine().Split(' ');
            var output = OrderProcessor.ConvertInputToType(input);
            Console.WriteLine("Item Name : {0} Operations : {1}", output.ItemName, string.Join(' ', output.Operations));
            Console.ReadLine();
        }
    }

    public class OrderProcessor
    {
        public static Product ConvertInputToType(string[] input)
        {
            ProductTypes type;
            try
            {
                type = (ProductTypes)Enum.Parse(typeof(ProductTypes), input[0], ignoreCase: true);
            }
            catch (Exception e)
            {
                type = ProductTypes.Other;
            }
            Product product;
            string name = input.Length > 1 ? string.Join(' ', input, 1, input.Length - 1) : string.Empty;
            switch (type)
            {
                case ProductTypes.Membership:
                    {
                        product = new Membership();
                        break;
                    }
                case ProductTypes.Upgrade:
                    {
                        product = new Upgrade();
                        break;
                    }
                case ProductTypes.Video:
                    {
                        product = new Video(name);
                        break;
                    }
                case ProductTypes.Book:
                    {
                        product = new Book(name);
                        break;
                    }
                case ProductTypes.Other:
                default:
                    {
                        product = new Other(name);
                        break;
                    }
            }
            return product;
        }
    }
}
