namespace Magenta.Workflow.Configuration
{
    public class MagentaConfigBuilder
    {
        public bool InMemoryState  { get; internal set;}
        
        public MagentaConfigBuilder UseInMemory()
        {
            InMemoryState = true;

            return this;
        }
    }
}
