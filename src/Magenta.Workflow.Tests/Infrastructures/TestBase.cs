using System.Text;
using Magenta.Workflow.Tests.Mock;
using Xunit.Abstractions;

namespace Magenta.Workflow.Tests.Infrastructures
{
    public class TestBase
    {
        public TestBase(ITestOutputHelper testOutput)
        {
            TestOutput = testOutput ?? throw new System.ArgumentNullException(nameof(ITestOutput));
        }

        public ITestOutputHelper TestOutput { get; set; }


        internal void LogTestInfo(object requestModel = null, object result = null)
        {
            TestOutput.WriteLine($"StateManager: {MockState.StateManager?.GetType()?.GUID}");

            var strRequestModel = GetObjectInfo(requestModel, "Request Model");
            var strResult = GetObjectInfo(result, "Result");

            TestOutput.WriteLine(strRequestModel);
            TestOutput.WriteLine(strResult);
        }

        string GetObjectInfo(object obj, string title)
        {
            if (obj == null) return string.Empty;

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine(title);
            strBuilder.AppendLine($"Type -> {obj.GetType().Name}");
            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
                strBuilder.AppendLine($"{prop.Name}:{prop.GetValue(obj)}");
            return strBuilder.ToString();
        }
    }
}
