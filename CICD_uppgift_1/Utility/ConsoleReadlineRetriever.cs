using System;
namespace CICD_uppgift_1
{
    public class ConsoleReadlineRetriever
    {
        public virtual string GetStringInput()
        {
            return Console.ReadLine();
        }
    }
}