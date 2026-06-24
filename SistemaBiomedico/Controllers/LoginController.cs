using SistemaBiomedico.Data;
using SistemaBiomedico.Models;
using SistemaBiomedico.ViewModels;
////////////////////////
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
////////////////////////

namespace SistemaBiomedico.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class LoginController : Controller
    {
        private readonly AppDbContext _dbconext;
        public LoginController(AppDbContext dbcontext)
        {
            _dbconext = dbcontext;
        }
    
        [HttpGet]
        public IActionResult Registro()
        {
            ViewBag.Roles = _dbconext.Roles.ToList();
            return View();
        }
        [HttpPost]
       
        public async Task<IActionResult> Registro(UserVM model)
        {
            if (model.Pass != model.RepPass)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                ViewBag.Roles = _dbconext.Roles.ToList();
                return View();
            }
            Usuario usuario = new Usuario()
            {
                NameUser = model.Name,
                Email = model.Email,
                Password = model.Pass,
                RolId = model.IdRol
            };
            await _dbconext.Usuarios.AddAsync(usuario);
            await _dbconext.SaveChangesAsync();
            if (usuario.idUsuario != 0) return RedirectToAction("Login", "Login");
            ViewData["Mensaje"] = "El usuario no pudo crearse";
            ViewBag.Roles = _dbconext.Roles.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            Usuario? usuario_encontrado = await _dbconext.Usuarios.Include(u => u.Roles).Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefaultAsync();
            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron usuarios";
                return View();
            }
            //Claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.NameUser),
                new Claim(ClaimTypes.Email, usuario_encontrado.Email),
                new Claim(ClaimTypes.Role, usuario_encontrado.Roles.Nombre),
                new Claim("IdUsuario", usuario_encontrado.idUsuario.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var propiedades = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                propiedades
                );
            //Sesiones
            HttpContext.Session.SetString("Usuario", usuario_encontrado.NameUser);
            HttpContext.Session.SetString("Rol", usuario_encontrado.Roles.Nombre);
            HttpContext.Session.SetInt32("IdUsuario", usuario_encontrado.idUsuario);
            switch (usuario_encontrado.Roles.Nombre)
            {
                case "Administrador":
                    return RedirectToAction("Administrador", "Login");
                case "Tecnico":
                    return RedirectToAction("Tecnico", "Login");
                case "Operativo":
                    return RedirectToAction("Operativo", "Login");
                default:
                    return RedirectToAction("Login", "Login");
            }
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Administrador()
        {
            return View();
        }
        [Authorize(Roles = "Tecnico")]
        [HttpGet]
        public IActionResult Tecnico()
        {
            return View();
        }
        [Authorize(Roles = "Operativo")]
        [HttpGet]
        public IActionResult Operativo()
        {
            return View();
        }
    }
}