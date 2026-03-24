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
    public IActionResult GetProjects(int pageHowMany = 10, int pageNum = 1, [FromQuery] List<string>? projectTypes = null)
    {
        var query = _waterContext.Projects.AsQueryable();

        if (projectTypes != null && projectTypes.Any())
        {
            query = query.Where(p => projectTypes.Contains(p.ProjectType));
        }
        
        var totalNumProjects = query.Count();
        
        var something = query
            .Skip((pageNum-1) * pageHowMany)
            .Take(pageHowMany)
            .ToList();

        var someObject = new
        {
            Projects = something,
            TotalNumProjects = totalNumProjects
        };
        
        return Ok(someObject);
    }

    [HttpGet("FunctionalProjects")]
    public IEnumerable<Project> GetFunctionalProjects()
    {
        var something = _waterContext.Projects.Where(p => p.ProjectFunctionalityStatus == "Functional").ToList();
        return something;
    }
    
    [HttpGet("GetProjectTypes")]
    public IActionResult GetProjectTypes()
    {
        var projectTypes = _waterContext.Projects
            .Select(p => p.ProjectType)
            .Distinct()
            .ToList();
        
        return Ok(projectTypes);
    }

}