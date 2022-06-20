using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Core.Tasks;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
            var strRequestModel = GetObjectInfo(requestModel, "Request Model");
            var strResult = GetObjectInfo(result, "Result");

            TestOutput.WriteLine(strRequestModel);
            TestOutput.WriteLine(strResult);
        }

        string GetObjectInfo(object obj, string title)
        {
            if (obj == null) return string.Empty;

            var strBuilder = new StringBuilder();
            strBuilder.AppendLine(title);
            strBuilder.AppendLine($"Type -> {obj.GetType().Name}");
            Type objType = obj.GetType();
            var resultProp = objType.GetProperties()
                .FirstOrDefault(x => x.Name.Equals(nameof(FlowResult.Result)));
            var result = resultProp?.GetValue(obj);

            if (result != null)
            {
                var serialized = JsonSerializer.Serialize(obj,
                    options: new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                strBuilder.AppendLine("Object result props:");
                strBuilder.AppendLine(serialized);
            }

            return strBuilder.ToString();
        }
    }
}
