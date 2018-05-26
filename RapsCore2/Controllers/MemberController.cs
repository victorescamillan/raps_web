using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RapsCore2.Models;
using RapsCore2.Helpers;
using RapsCore2.Models.MemberViewModel;
using Microsoft.AspNetCore.Http;
using RapsCore2.Models.AccountViewModels;
using System.IO;

namespace RapsCore2.Controllers
{
    public class MemberController : Controller
    {
        private readonly DB_A1A56D_RapsFinalContext _context;

        public MemberController(DB_A1A56D_RapsFinalContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("member_id") != null)
            {
                var member_id = HttpContext.Session.GetInt32("member_id");

                ViewData["name"] = HttpContext.Session.GetString("name");
                ViewData["role"] = HttpContext.Session.GetString("role");

                var dashboard = _context.LoadStoredProc("dbo.GetMemberDashboard")
                .WithSqlParam("member_id", member_id)
                .ExecuteStoredProc<GetMemberDashboardViewModel>()
                .FirstOrDefault();

                ViewBag.Downlines = dashboard.downlines;
                ViewBag.Level1 = dashboard.level1;
                ViewBag.Last_Comm = dashboard.last_comm;
                ViewBag.Pending_Comm = dashboard.pending_comm;
                ViewBag.Member_Id = member_id;

                var commission = _context.LoadStoredProc("dbo.GetEncashmentByMember")
                  .WithSqlParam("member_id", member_id)
                  .ExecuteStoredProc<EncashmentRequestViewModel>()
                  .AsQueryable();
                return View(commission);
            }
            else
            {
                //HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }
        }
        // GET: Members
        public IActionResult List()
        {
            var members = _context.LoadStoredProc("dbo.GetMembers")
                 .ExecuteStoredProc<GetMembersViewModel>()
                 .AsQueryable();
            return View(members);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("member_id") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var member = await _context.Member
                    .SingleOrDefaultAsync(m => m.Id == id);
                if (member == null)
                {
                    return NotFound();
                }

                return View(member);
            }
            else
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }
           
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                id = HttpContext.Session.GetInt32("member_id");
            }

            var member = await _context.Member.SingleOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction("Profile","Member");
            }
            return View(member);
        }
        public async Task<IActionResult> Edit_Login(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                id = HttpContext.Session.GetInt32("member_id");
            }

            var member = await _context.Member.SingleOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Login(int id, [Bind("Id,Username,Password")] Member member)
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
                    data.Username = member.Username;
                    data.Password = member.Password;

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
                return RedirectToAction("Profile", "Member");
            }
         
            return View(member);
        }
        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .SingleOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Member.SingleOrDefaultAsync(m => m.Id == id);
            _context.Member.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.Id == id);
        }
        public IActionResult Genealogy()
        {
            if (HttpContext.Session.GetInt32("member_id") != null)
            {
                var member_id = HttpContext.Session.GetInt32("member_id");
                ViewData["name"] = HttpContext.Session.GetString("name");
                ViewData["role"] = HttpContext.Session.GetString("role");
                var genealogy = _context.LoadStoredProc("dbo.GetGenealogy")
                   .WithSqlParam("member_id", member_id)
                   .ExecuteStoredProc<GenealogyViewModel>()
                   .AsQueryable();

                var leve1 = genealogy.Where(v => v.level == 1).ToList();
                var leve2 = genealogy.Where(v => v.level == 2).ToList();
                var leve3 = genealogy.Where(v => v.level == 3).ToList();
                var leve4 = genealogy.Where(v => v.level == 4).ToList();
                var leve5 = genealogy.Where(v => v.level == 5).ToList();

                return View(genealogy);
            }
            else
            {
                //HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }
          
        }
        
        public IActionResult Commission()
        {
            if (HttpContext.Session.GetInt32("member_id") != null)
            {

                var member_id = HttpContext.Session.GetInt32("member_id");

                ViewData["name"] = HttpContext.Session.GetString("name");
                ViewData["role"] = HttpContext.Session.GetString("role");
                var commission = _context.LoadStoredProc("dbo.GetEncashmentByMember")
                   .WithSqlParam("member_id", member_id)
                   .ExecuteStoredProc<EncashmentRequestViewModel>()
                   .AsQueryable();
                return View(commission);
                
            }
            else
            {
                //HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }
        }
        public IActionResult Commission_Details()
        {
            if (HttpContext.Session.GetInt32("member_id") != null)
            {

                var member_id = HttpContext.Session.GetInt32("member_id");

                var result = from c in _context.Commission
                             join p in _context.Program on c.EncashId equals p.Id
                             join m in _context.Member on c.MemberId equals m.Id
                             
                             select new
                             {
                                 Downline = m.Fname + " " + m.Lname,
                                 Prog =p.Name, 
                                 Level = c.Level,
                                 Amount = c.Amount,
                                 Date_Requested = c.DateRequested,
                             };
                var comm = new List<CommissionDetailsViewModel>();
                foreach(var item in result)
                {
                    comm.Add(new CommissionDetailsViewModel
                    {
                        Downline = item.Downline,
                        Prog = item.Prog,
                        Level = item.Level,
                        Amount = item.Amount,
                        Date_Requested = item.Date_Requested.GetValueOrDefault()
                    });
                }
                return View(comm);
            }
            else
            {
                //HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Purchase()
        {
            if (HttpContext.Session.GetInt32("member_id") != null)
            {

                var member_id = HttpContext.Session.GetInt32("member_id");
                ViewData["name"] = HttpContext.Session.GetString("name");
                ViewData["role"] = HttpContext.Session.GetString("role");
                var purchase = _context.LoadStoredProc("dbo.GetPurchaseHistory")
                   .WithSqlParam("member_id", member_id)
                   .ExecuteStoredProc<GetPurchaseHistoryViewModel>()
                   .AsQueryable();
                return View(purchase);
            }
            else
            {
                //HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }
        }
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetInt32("member_id") != null)
            {
                
                var member_id = HttpContext.Session.GetInt32("member_id");
                var detail = _context.Member.Where(d => d.Id == member_id).FirstOrDefault();
                ViewData["name"] = HttpContext.Session.GetString("name");
                ViewData["role"] = HttpContext.Session.GetString("role");

                var rank = _context.Rank.FirstOrDefault(d => d.Id == detail.RankId);
                ViewBag.Rank = rank.Name;
                return View(detail);
            }
            else
            {
                //HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }
        }
        public IActionResult StockistProgram()
        {
            return View();
        }
        [Route("api/Member/GetMembers")]
        public IActionResult GetMembers()
        {
            var members = from m in _context.Member
                          join s in _context.Member on m.Id equals s.Id into sp from s in sp.DefaultIfEmpty()
                          join r in _context.Rank on m.RankId equals r.Id
                          join u in _context.UserRole on m.RoleId equals u.Id
                          
                          where u.Role != "ADMIN"
                          orderby m.Id ascending
                          select new
                          {
                              id = m.Id,
                              name = m.Fname + " " + m.Lname,
                              role = u.Role,
                              rank = r.Name,
                              sponsor_id = m.SponsorId,
                              sponsor_name = s.Fname + " " + s.Lname,
                              address = m.Address + ", " + m.City,
                              province = m.Province,
                              contact = m.ContactNo,
                              email = m.Email,
                              bank_account = m.BankAccount,
                              tin = m.TinNo,
                              username = m.Username,
                              date_join = m.DateJoin,
                              is_maintain = m.IsMaintenance,
                              is_active = m.IsActive,
                          };

            return new ObjectResult(members);
        }
        //[HttpPost]
        //public async Task<IActionResult> UploadFile(IFormFile files)
        //{
        //    if (files == null || files.Length == 0)
        //        return Content("file not selected");

        //    var path = Path.Combine(
        //                Directory.GetCurrentDirectory(), "wwwroot",
        //                files.FileName);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    return RedirectToAction("Files");
        //}

        //public async Task<IActionResult> Download(string filename)
        //{
        //    if (filename == null)
        //        return Content("filename not present");

        //    var path = Path.Combine(
        //                   Directory.GetCurrentDirectory(),
        //                   "wwwroot", filename);

        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(path, FileMode.Open))
        //    {
        //        await stream.CopyToAsync(memory);
        //    }
        //    memory.Position = 0;
        //    return File(memory, GetContentType(path), Path.GetFileName(path));
        //}
    }
}
