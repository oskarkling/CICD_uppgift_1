using System;
namespace CICD_uppgift_1
{
    // This class is for injection of console.readling for testing input
    public class ConsoleReadlineRetriever
    {
        public virtual string GetStringInput()
        {
            return Console.ReadLine();
        }
    }
}