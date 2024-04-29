using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace shopManager
{
    class Node
    {
        //public Node Next;
        //public Product Data;
    }
    public class StackDataList
    {
        private Node Top;
        private int id = 1111110;
        public bool IsEmpty()
        {
            if (Top == null)
                return true;
            else return false;
        }

        public void PushNewProduct(string name, string category, string quantity, string cost, string profit)
        {
            string filePath = @"D:\products.txt";
            FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine($"{id} , {name} , {category} , {quantity} , {cost} , {profit}");
            sw.Close();
            fs.Close();
            //Node prodNode = new Node();
            //prodNode.Data = new Product(name, category, quantity, id, cost, profit);
            //prodNode.Next = Top;
            //Top = prodNode;
            //id++;
        }
        public void GetAllProducts()
        {
            //List<Product> products = new List<Product>();

            //Node current = Top;
            //while (current != null)
            //{
            //    products.Add(current.Data);
            //    current = current.Next;
            //}

            //return products;
        }

        public void GetSpecificProductById(int id)
        {


            //if (Top == null) return null;
            //Node p = Top;

            //if (p.Data.ID == id) return p.Data;

            //while (p.Next != null)
            //{
            //    if (p.Next.Data.ID == id)
            //        return p.Next.Data;

            //    p = p.Next;
            //}

            //return null;
        }

        public void RemovedSpesProduct(int id)
        {
            //Node p = Top;
            //if (p.Data.ID == id)
            //{
            //    Top = Top.Next;
            //    return;
            //}
            //while (p.Next.Data.ID != id)
            //    p = p.Next;
            //p.Next = p.Next.Next;
        }
        public void Update(int newQuantity, int id)
        {
            //Node Update = Top;
            //while (Update != null)
            //{
            //    if (Update.Data.ID == id)
            //    {
            //        int oldQuantity = Update.Data.Quantity;
            //        Update.Data = new Product(Update.Data.Name, Update.Data.Category, oldQuantity - newQuantity, id, Update.Data.Cost, Update.Data.Profit);
            //        return;
            //    }

            //    Update = Update.Next;

            //}
        }
    }
}
