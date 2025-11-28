using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceProject.Models;

namespace ECommerceProject.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // Middleware kiểm tra đăng nhập
        private bool IsAdmin() => HttpContext.Session.GetString("AdminRole") == "Admin";

        // 1. Danh sách sản phẩm (CÓ PHÂN TRANG)
        public async Task<IActionResult> Index(int page = 1)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Admin");

            int pageSize = 10; // Số sản phẩm trên 1 trang admin (nên để nhiều hơn trang chủ chút)

            // Query dữ liệu (sắp xếp mới nhất lên đầu)
            var query = _context.Products.Include(p => p.Category).OrderByDescending(p => p.ProductId);

            // Tính toán phân trang
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Lấy dữ liệu trang hiện tại
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Đóng gói vào ViewModel
            var viewModel = new ProductListViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        // 2. Trang Thêm mới (GET)
        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Admin");
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // 3. Xử lý Thêm mới (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "images");
                if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

                string filename = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(uploadDir, filename);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                product.ImageUrl = filename;
            }
            else
            {
                product.ImageUrl = "default.jpg";
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // 4. Xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Admin");
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                if (product.ImageUrl != "default.jpg")
                {
                    string oldPath = Path.Combine(_environment.WebRootPath, "images", product.ImageUrl);
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // 5. Trang Sửa sản phẩm (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Admin");
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // 6. Xử lý Sửa sản phẩm (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile imageFile)
        {
            if (id != product.ProductId) return NotFound();

            var existingProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);

            if (imageFile != null)
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "images");
                if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

                string filename = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(uploadDir, filename);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                product.ImageUrl = filename;
            }
            else
            {
                product.ImageUrl = existingProduct.ImageUrl;
            }

            _context.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}