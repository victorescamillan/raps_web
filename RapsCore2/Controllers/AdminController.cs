using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RapsCore2.Models;
using Microsoft.EntityFrameworkCore;
using RapsCore2.Models.AccountViewModels;
using System.Diagnostics;
using RapsCore2.Models.AdminViewModel;

namespace RapsCore2.Controllers
{
    public class AdminController : Controller
    {
        private readonly DB_A1A56D_RapsFinalContext _context;

        public AdminController(DB_A1A56D_RapsFinalContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                ViewBag.Name = HttpContext.Session.GetString("name");
                ViewBag.Role = HttpContext.Session.GetString("role");
                
                var members = _context.Member.ToList();
                    

                if(members.Count() > 0)
                {
                    ViewBag.MemberCount = members.Count();
                    ViewBag.JuniorMember = members.Where(x => x.RoleId == 4).ToList().Count();
                    ViewBag.SeniorMember = members.Where(x => x.RoleId == 6).ToList().Count();
                    ViewBag.MegaMember = members.Where(x => x.RoleId == 3).ToList().Count();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
          
        }

        public IActionResult Members()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Role = HttpContext.Session.GetString("role");
            var members = _context.Member.Where(m => m.Id > 1000001).ToList();

            var x = members.Where(data => data.Id.Equals(1003057)).First();
            return View(members);
        }
        public IActionResult Deactivate(int? id)
        {
            var member = (from c in _context.Member
                          where c.Id == id
                          select c).First();
            if(member != null)
            {
                member.IsActive = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Members", "Admin");
        }
        public IActionResult Activate(int? id)
        {
            var member = (from c in _context.Member
                          where c.Id == id
                          select c).First();
            if (member != null)
            {
                member.IsActive = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Members", "Admin");
        }
        public IActionResult MemberDetails(int? id)
        {
            ViewBag.Role = HttpContext.Session.GetString("role");
            if (ViewBag.Role != "ADMIN")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            var member_id = 0;
            if (id.HasValue)
            {
                HttpContext.Session.SetInt32("member_id", id.GetValueOrDefault());
                member_id = id.GetValueOrDefault();
            }
            else
            {
                member_id = HttpContext.Session.GetInt32("member_id").GetValueOrDefault();
            }
            var members = _context.Member.Where(m => m.Id == member_id).FirstOrDefault();
            var query = (from m in _context.Member
                       join r in _context.Rank
                       on m.RankId equals r.Id
                       where m.Id == member_id
                       select new
                       {
                           rank = r.Name
                       }).First();
            if(query != null)
            {
                ViewBag.Rank = query.rank;
            }

            return View(members);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Lname,Fname,Mi,Birthdate,Beneficiary,Address,City,Province,ZipCode,ContactNo,Gender,Email,BankAccount,TinNo")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _context.Member.Where(d => d.Id == member.Id).FirstOrDefaultAsync();
                    data.Lname = member.Lname;
                    data.Fname = member.Fname;
                    data.Mi = member.Mi;
                    data.Birthdate = member.Birthdate;
                    data.Beneficiary = member.Beneficiary;
                    data.Address = member.Address;
                    data.City = member.City;
                    data.Province = member.Province;
                    data.ZipCode = member.ZipCode;
                    data.ContactNo = member.ContactNo;
                    data.Gender = member.Gender;
                    data.Email = member.Email;
                    data.BankAccount = member.BankAccount;
                    data.TinNo = member.TinNo;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("MemberDetails", "Admin");
            }
            return View(member);
        }
        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.Id == id);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            try
            {
                if (ModelState.IsValid)
                {
                    var result = (from m in _context.Member
                                  join r in _context.UserRole
                                  on m.RoleId equals r.Id
                                  where m.Username == model.Username && m.Password == model.Password
                                  select new
                                  {
                                      user_id = m.Id,
                                      role = r.Role,
                                      name = m.Fname + " " + m.Lname
                                  }).First();

                    if (result != null)
                    {
                        if(result.role == "ADMIN" || result.role == "STAFF")
                        {
                            HttpContext.Session.SetInt32("user_id", result.user_id);
                            HttpContext.Session.SetString("role", result.role);
                            HttpContext.Session.SetString("name", result.name);
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else
                        {
                            return View(model);
                        }
                    }
                    else
                    {
                        return View(model);
                    }
                }
            }
            catch
            {
                return View(model);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public IActionResult RequestEncashment()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Role = HttpContext.Session.GetString("role");
            try
            {
                var collection = from c in _context.EncashmentRequest
                                 join m in _context.Member
                                 on c.MemberId equals m.Id
                                 join r in _context.RequestType
                                 on c.RequestTypeId equals r.Id
                                 where c.IsApproved == false
                                 orderby c.Id ascending

                                 select new EncashmentViewModel
                                 {
                                     member_id = m.Id,
                                     member_name = m.Fname + " " + m.Lname,
                                     request_type = r.Type,
                                     request_date = c.DateRequested,
                                     approved_date = c.DateApproved,
                                     gross = c.Gross.GetValueOrDefault(),
                                     tax = c.Tax.GetValueOrDefault(),
                                     net = c.Net.GetValueOrDefault()
                                 };

                if (collection != null || collection.Any())
                {
                    return View(collection);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("General Exception: " + ex);
                return View();
            }
        }
        public IActionResult ApprovedEncashment()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Role = HttpContext.Session.GetString("role");
            try
            {
                var collection = from c in _context.EncashmentRequest
                                 join m in _context.Member
                                 on c.MemberId equals m.Id
                                 join r in _context.RequestType
                                 on c.RequestTypeId equals r.Id
                                 where c.IsApproved == true
                                 orderby c.Id ascending

                                 select new EncashmentViewModel
                                 {
                                     member_id = m.Id,
                                     member_name = m.Fname + " " + m.Lname,
                                     request_type = r.Type,
                                     request_date = c.DateRequested,
                                     approved_date = c.DateApproved,
                                     gross = c.Gross.GetValueOrDefault(),
                                     tax = c.Tax.GetValueOrDefault(),
                                     net = c.Net.GetValueOrDefault()
                                 };

                if (collection != null || collection.Any())
                {
                    return View(collection);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("General Exception: " + ex);
                return View();
            }
           
        }
        public IActionResult NewMemberCodes()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Role = HttpContext.Session.GetString("role");
            try
            {
                if (ViewBag.Role != "ADMIN")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                var collection = from c in _context.EntryCode
                                 join m in _context.Member
                                 on c.MemberId equals m.Id into op from y in op.DefaultIfEmpty()
                                 orderby c.Id ascending
                                 select new CodesViewModel
                                 {
                                     product_code = c.ProductCode,
                                     pin_code = c.PinCode,
                                     status = c.IsUsed == true ? "Not Available" : "Available",
                                     //member_id = y.Id == 0 ? 0 : y.Id,
                                     member = y.Fname + " " + y.Lname
                                 };
                if (collection != null || collection.Any())
                    return View(collection);
                else
                    return View();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("General Exception: " + ex);
                return View();
            }
        }
        public IActionResult RepeatPurchaseCodes()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Role = HttpContext.Session.GetString("role");
            try
            {
                if (ViewBag.Role != "ADMIN")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                var collection = from c in _context.RepeatPurchaseCode
                                 join m in _context.Member
                                 on c.MemberId equals m.Id into op
                                 from y in op.DefaultIfEmpty()
                                     //on c.MemberId equals m.Id
                                 orderby c.Id ascending
                                 select new CodesViewModel
                                 {
                                     product_code = c.ProductCode,
                                     pin_code = c.PinCode,
                                     status = c.IsUsed == true ? "Not Available" : "Available",
                                     //member_id = m.Id,
                                     member = y.Fname + " " + y.Lname
                                 };

                if (collection != null || collection.Any())
                    return View(collection);
                else
                    return View();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("General Exception: " + ex);
                return View();
            }
        }
        public IActionResult JuniorList()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Role = HttpContext.Session.GetString("role");
            var collection = from s in _context.Stockist
                             join m in _context.Member
                             on s.MemberId equals m.Id
                             join r in _context.UserRole
                             on s.RoleId equals r.Id
                             where s.RoleId == 4
                             orderby s.Id

                             select new StockistViewModel
                             {
                                 member_id = m.Id,
                                 name = m.Fname + " " + m.Lname,
                                 role = r.Role,
                                 date = s.Date.GetValueOrDefault()
                             };
            if(collection != null || collection.Any())
            {
                return View(collection);
            }
            else
            {
                return View();
            }
        }
        public IActionResult SeniorList()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Role = HttpContext.Session.GetString("role");
            var collection = from s in _context.Stockist
                             join m in _context.Member
                             on s.MemberId equals m.Id
                             join r in _context.UserRole
                             on s.RoleId equals r.Id
                             where s.RoleId == 6
                             orderby s.Id

                             select new StockistViewModel
                             {
                                 member_id = m.Id,
                                 name = m.Fname + " " + m.Lname,
                                 role = r.Role,
                                 date = s.Date.GetValueOrDefault()
                             };
            if (collection != null || collection.Any())
            {
                return View(collection);
            }
            else
            {
                return View();
            }
        }
        public IActionResult MegaList()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Role = HttpContext.Session.GetString("role");
            var collection = from s in _context.Stockist
                             join m in _context.Member
                             on s.MemberId equals m.Id
                             join r in _context.UserRole
                             on s.RoleId equals r.Id
                             where s.RoleId == 3
                             orderby s.Id

                             select new StockistViewModel
                             {
                                 member_id = m.Id,
                                 name = m.Fname + " " + m.Lname,
                                 role = r.Role,
                                 date = s.Date.GetValueOrDefault()
                             };
            if (collection != null || collection.Any())
            {
                return View(collection);
            }
            else
            {
                return View();
            }
        }
    }
}