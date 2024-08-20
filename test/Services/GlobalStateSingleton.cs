namespace test.Services
{
    public class GlobalStateSingleton
    {
        public GlobalStateSingleton()
        {
            LastCsvOffset = 0;
        }

        public int LastCsvOffset { get; set; }
    }
}
