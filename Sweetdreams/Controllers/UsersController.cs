using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Sweetdreams.Data;
using Sweetdreams.Models;
using Sweetdreams.Models.Identity;
using System.Collections;

namespace Sweetdreams.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _roleManager = roleManager;
        }
        // GET: UsersController
        public async Task<IActionResult> Index()
        {
            return _dbContext.Users != null ?
                        View(await _dbContext.Users.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Roles'  is null.");
        }

        // GET: UsersController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _dbContext.Users == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _dbContext.Users == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,PhoneNumber")] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dbuser = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);
                    if (!ApplicationUserExists(dbuser.Id))
                    {
                        return NotFound();
                    }
                    dbuser.UserName = user.UserName;
                    dbuser.PhoneNumber = user.PhoneNumber;
                    dbuser.Email = user.Email;
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: ApplicationRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _dbContext.Users == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: ApplicationRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_dbContext.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            var applicationRole = await _dbContext.Users.FindAsync(id);
            if (applicationRole != null)
            {
                _dbContext.Users.Remove(applicationRole);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> AddRole(string id)
        {
            if (id == null || _dbContext.Users == null)
            {
                return NotFound();
            }


            var user = await _dbContext.Users.FindAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var viewModel = new AddRoleViewModel(roles, user);

            if (user == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("AddRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string id, [Bind("Name")] ApplicationRole role)
        {
            if (id == null || _dbContext.Users == null)
            {
                return NotFound();
            }


            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (!ApplicationRoleExists(role.Name))
            {
                role.NormalizedName = role.Name.ToUpper();
                _dbContext.Add(role);
                await _dbContext.SaveChangesAsync();
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var viewModel = new AddRoleViewModel(roles, user);
                return View(viewModel);
            } else
            {
                throw new InvalidOperationException();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> RemoveRole(string parameters)
        {
            var id = parameters.Split(",")[0];
            var roleName = parameters.Split(",")[1];

            if (id == null || _dbContext.Users == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FindAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                var viewModel = new AddRoleViewModel(roles, user);
                if (user == null)
                {
                    return NotFound();
                }

                return RedirectToAction("Index");
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private bool ApplicationUserExists(string id)
        {
            return (_dbContext.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool ApplicationRoleExists(string roleName)
        {
            return (_dbContext.Roles?.Any(e => e.Name == roleName)).GetValueOrDefault();
        }
    }
}
