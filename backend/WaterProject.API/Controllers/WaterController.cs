using Microsoft.AspNetCore.Mvc;
using WaterProject.API.Data;

namespace WaterProject.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WaterController : ControllerBase
{
    private WaterDBContext _waterContext;
    public WaterController(WaterDBContext temp)
    {
        _waterContext = temp;
    }
    
    [HttpGet("AllProjects")]
    public IEnumerable<Project> GetProjects()
    {
        return _waterContext.Projects.ToList();
    }

    [HttpGet("FunctionalProjects")]
    public IEnumerable<Project> GetFunctionalProjects()
    {
        var something = _waterContext.Projects.Where(p => p.ProjectFunctionalityStatus == "Functional").ToList();
        return something;
    }

}