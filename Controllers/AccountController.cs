using BestStoreMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BestStoreMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Register(RegisterDto registerDto)
        {

            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid) 
            {
                return View(registerDto);
            }

            //cria uma nova conta e autentica o usuario
            var user = new ApplicationUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.Email, // UserName vai ser usado para autenticar o usuario
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                Address = registerDto.Address,
                CreatedAt = DateTime.Now,
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                //sucesso na criação de usuario
                await userManager.AddToRoleAsync(user, "cliente");

                //entrar no novo usuario
                await signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            //se o result falhar, mostra os erros
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerDto);
        }


        public async Task<IActionResult> Logout()
        {
            if(signInManager.IsSignedIn(User))
            {
                await signInManager.SignOutAsync(); 
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(loginDto loginDto)
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            var result = await signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password,
                loginDto.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Login falhou. Verifique suas credenciais.";
            }

            return View(loginDto);
        }
    }
}
