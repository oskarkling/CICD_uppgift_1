namespace CICD_uppgift_1
{

    // Injection of Console.Readline()
    public class TestConsoleReadlineRetriever : ConsoleReadlineRetriever
    {
        public string Input { get; set; }
        public override string GetStringInput()
        {
            return Input;
        }
    }
}