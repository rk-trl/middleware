namespace Middleware
{
    public class Print : IPrint
    {
        public Print() { }
        void IPrint.Print()
        {
            Console.WriteLine("Printing");
        }
    }
}
