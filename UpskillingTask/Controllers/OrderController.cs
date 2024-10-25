using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UpskillingTask.DBContext;
using UpskillingTask.Models;

namespace UpskillingTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(AddOrderRequestDTO input)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order =  _context.Orders.Add(new Order
            {
                OrderDate = DateTime.Now,
                CustomerId = input.CustomerId,
                TotalAmount = input.Products.Sum(x => x.Quantity * x.Price)
            });
                await _context.SaveChangesAsync();
                var orderItems = input.Products.Select(x => new OrderItem
            {
                ProductId=x.Id,
                Quantity=x.Quantity,
                OrderId=order.Entity.Id
            }).ToList();
             _context.OrderItems.AddRange(orderItems);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(input);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(long id)
        {
            var Order = await _context.Orders.FirstOrDefaultAsync(x=>x.Id==id);

            if (Order == null)
            {
                return NotFound();
            }

            return Order;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var Order = await _context.Orders.FirstOrDefaultAsync(x=>x.Id==id);
            if (Order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Order Order)
        {
           _context.Entry(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool OrderExists(long id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
