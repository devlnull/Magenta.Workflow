using System;
using System.Linq;
using Magenta.Workflow;
using Magenta.Workflow.Core.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected IFlowReportManager FlowReportManager { get; }
        protected IFlowManager FlowManager { get; }

        public BaseController(IFlowManager flowManager,
            IFlowReportManager flowReportManager)
        {
            FlowManager = flowManager ?? throw new ArgumentNullException(nameof(flowManager));
            FlowReportManager = flowReportManager ?? throw new ArgumentNullException(nameof(flowReportManager));
        }

        protected void HandleFlowResult<T>(FlowResult<T> flowResult)
        {
            if (flowResult != null && flowResult.Warned)
                TempData["warning"] = flowResult.Warns;
            else if (flowResult != null && flowResult.Succeeded == false)
                TempData["error"] = flowResult.Errors;
            else if (flowResult != null && flowResult.Succeeded)
                TempData["success"] = true;
        }
    }
}
