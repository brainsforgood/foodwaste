using Microsoft.AspNetCore.Mvc;
using Products;
using Repositories;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private IRepository repository;
    public ProductController(IRepository repo) => repository = repo;

    [HttpGet()]
    public IEnumerable<Product> Get() => repository.Products;
 
    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        return Ok(repository[id]);
    }
    

}