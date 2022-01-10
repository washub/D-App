using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dapp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private DataContext _context;
        public DataController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetValues(){
            return Ok(await _context.Values.ToListAsync());
        }
         
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id){
            var value = await _context.Values.FirstOrDefaultAsync(x=> x.Id == id);
            return Ok(value);
        }
    }
}