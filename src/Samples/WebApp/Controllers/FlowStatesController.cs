using System;
using System.Threading.Tasks;
using Magenta.Workflow;
using Magenta.Workflow.UseCases.InitFlowState;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class FlowStatesController : BaseController
    {
        public FlowStatesController(IFlowManager flowManager, IFlowReportManager flowReportManager) 
            : base(flowManager, flowReportManager)
        {
        }

        public async Task<IActionResult> Index(Guid flowTypeId)
        {
            var states = await FlowReportManager.GetStatesByTypeIdAsync(flowTypeId);
            return View("Index", states.Result);
        }

        [HttpGet]
        public IActionResult Create(Guid flowTypeId)
        {
            return View("Create", new InitFlowStateRequest()
            {
                TypeId = flowTypeId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(InitFlowStateRequest request)
        {
            var result = await FlowManager.InitFlowStateAsync(request);
            HandleFlowResult(result);
            return View("Create", new InitFlowStateRequest()
            {
                TypeId = request.TypeId
            });
        }
    }
}
