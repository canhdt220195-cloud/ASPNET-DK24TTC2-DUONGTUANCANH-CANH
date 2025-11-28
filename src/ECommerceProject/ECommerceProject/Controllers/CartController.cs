using Microsoft.AspNetCore.Mvc;
using ECommerceProject.Models;
using ECommerceProject.Helpers;
using Microsoft.AspNetCore.Http;

namespace ECommerceProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var check = cart.FirstOrDefault(x => x.ProductId == id);

            if (check == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = 1
                });
            }
            else
            {
                check.Quantity++;
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            var item = cart.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            if (cart == null || cart.Count == 0)
            {
                return RedirectToAction("Index");
            }

            var customerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
            var user = _context.Customers.Find(customerId);
            ViewBag.CurrentUser = user;

            return View(cart);
        }

        // --- CẬP NHẬT: Nhận thêm tham số 'phone' và cập nhật thông tin khách hàng ---
        [HttpPost]
        public async Task<IActionResult> Checkout(string address, string phone)
        {
            var customerIdStr = HttpContext.Session.GetString("CustomerId");
            if (customerIdStr == null) return RedirectToAction("Login", "Account");

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            if (cart == null || cart.Count == 0) return RedirectToAction("Index");

            int customerId = int.Parse(customerIdStr);
            var customer = _context.Customers.Find(customerId);

            // Cập nhật địa chỉ và SĐT mới nhất vào hồ sơ khách hàng
            if (customer != null)
            {
                customer.Address = address;
                customer.Phone = phone;
                _context.Update(customer);
            }

            var newCart = new Cart
            {
                CustomerId = customerId,
                DateCreated = DateTime.Now,
                OrderStatus = 0 // Mặc định: Chờ xác nhận
            };

            _context.Carts.Add(newCart);
            await _context.SaveChangesAsync();

            foreach (var item in cart)
            {
                var cartDetail = new CartDetail
                {
                    CartId = newCart.CartId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    PriceAtTime = item.Price
                };
                _context.CartDetails.Add(cartDetail);
            }
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Cart");

            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}