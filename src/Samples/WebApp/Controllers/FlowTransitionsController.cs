using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Magenta.Workflow;
using Magenta.Workflow.UseCases.InitFlowTransition;

namespace WebApp.Controllers
{
    public class FlowTransitionsController : BaseController
    {
        public FlowTransitionsController(IFlowManager flowManager, IFlowReportManager flowReportManager)
            : base(flowManager, flowReportManager)
        {
        }

        public async Task<IActionResult> Index(Guid flowStateId)
        {
            var transitions = await FlowReportManager
                .GetSourceTransitionsAsync(flowStateId);
            return View("Index", transitions.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid flowStateId)
        {
            var state = await FlowReportManager.GetStateByIdAsync(flowStateId);
            var states = await FlowReportManager.GetStatesByTypeIdAsync(state.Result.TypeId);
            ViewBag.States = states.Result;
            return View("Create", new InitFlowTransitionRequest()
            {
                SourceId = flowStateId,
                TypeId = state.Result.TypeId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(InitFlowTransitionRequest request)
        {
            var result = await FlowManager.InitFlowTransitionAsync(request);
            HandleFlowResult(result);
            return RedirectToAction(nameof(Create), new {flowStateId = request.SourceId});
        }
    }
}
