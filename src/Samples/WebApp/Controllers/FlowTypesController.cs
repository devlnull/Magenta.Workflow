using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Magenta.Workflow;
using Magenta.Workflow.UseCases.InitFlowType;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class FlowTypesController : BaseController
    {
        public FlowTypesController(IFlowManager flowManager,
            IFlowReportManager flowReportManager) : base(flowManager, flowReportManager)
        {
        }

        public async Task<IActionResult> Index()
        {
            var result = await FlowReportManager.GetTypesAsync();
            return View("Index", result.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create", new InitFlowTypeTextModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(InitFlowTypeTextModel flowTypeTextModel)
        {
            var flowTypeModel = new InitFlowTypeModel()
            {
                Name = flowTypeTextModel.Name,
                EntityPayloadType = Type.GetType(flowTypeTextModel.EntityPayloadType),
                EntityType = Type.GetType(flowTypeTextModel.EntityType),
            };
            var result = await FlowManager.InitFlowTypeAsync(flowTypeModel);
            
            HandleFlowResult(result);
            return View("Create", new InitFlowTypeTextModel());
        }
    }
}
