using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogsModel;
using Microsoft.EntityFrameworkCore;

namespace BlogOracle.Controllers
{
    [Route("api/[controller]")]
    public class BlogsController : Controller
    {
        private Model _context;

        public BlogsController(Model context)
        {
            _context = context;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Console.WriteLine($"{GetHashCode()} - {_context.GetHashCode()}");
            return Ok(_context.Blogs.Include(blog => blog.Posts).FirstOrDefault(blog => blog.Id == id));
        }
    }
}
