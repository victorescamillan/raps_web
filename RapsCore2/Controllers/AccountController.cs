using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RapsCore2.Models;
using RapsCore2.Models.AccountViewModels;
using Microsoft.EntityFrameworkCore;
using RapsCore2.Helpers;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;
using RapsCore2.Models.MemberViewModel;
using PusherServer;
using System.Net;

namespace RapsCore2.Controllers
{

    public class AccountController : Controller
    {
        private DB_A1A56D_RapsFinalContext _context;
        public AccountController(DB_A1A56D_RapsFinalContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> HelloWorld()
        {
            var options = new PusherOptions { Encrypted = true };

            var pusher = new Pusher(
              "445256",
              "5881f5e8a9d5239e3638",
              "cb1a170bf6bb208005d2",
              options);

            var result = await pusher.TriggerAsync(
              "my-channel",
              "my-event",
              new { message = "hello world" });

            return new StatusCodeResult((int)HttpStatusCode.OK);
        }
        [HttpGet]
        //[AllowAnonymous]
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
                    var result = _context.LoadStoredProc("dbo.GetMemberByLogin")
                        .WithSqlParam("username", model.Username)
                        .WithSqlParam("password", model.Password)
                        .ExecuteStoredProc<MemberLoginViewModel>()
                        .Single();


                    if (result != null)
                    {
                        HttpContext.Session.SetInt32("member_id", result.member_id);
                        HttpContext.Session.SetString("role", result.role);
                        HttpContext.Session.SetString("name", result.lname + " " + result.fname);
                        if (result.role == "ADMIN")
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Dashboard", "Member");
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
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetInt32("member_id") != null)
            {
                HttpContext.Session.Clear();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register(int? id)
        {
            var member_id = HttpContext.Session.GetInt32("member_id");

            if (member_id == null)
            {
                member_id = id == null ? 0 : id;
            }
            var member = await _context.Member.SingleOrDefaultAsync(m => m.Id == member_id);
            if (member != null)
            {
                ViewBag.Sponsor_Id = member.Id;
                ViewBag.Sponsor_Name = member.Fname + " " + member.Lname;
            }

            return View();
        }
        [HttpPost]
        public IActionResult Register([Bind("Id,SponsorId,Lname,Fname,Mi,Birthdate,Beneficiary,Address,City,Province,ZipCode,ContactNo,Gender,Email,BankAccount,Username,Password,ConfirmPassword,TinNo,product_code,pin_code")] SaveMemberViewModel member)
        {
            if (ModelState.IsValid)
            {
                var result = _context.LoadStoredProc("dbo.SaveMember")
                    .WithSqlParam("id", 0)
                    .WithSqlParam("lname", member.Lname)
                    .WithSqlParam("fname", member.Fname)
                    .WithSqlParam("mi", member.Mi)
                    .WithSqlParam("birthdate", member.Birthdate)
                    .WithSqlParam("beneficiary", member.Beneficiary)
                    .WithSqlParam("address", member.Address)
                    .WithSqlParam("city", member.City)
                    .WithSqlParam("province", member.Province)
                    .WithSqlParam("zip_code", member.ZipCode == null ? string.Empty : member.ZipCode)
                    .WithSqlParam("contact_no", member.ContactNo == null ? string.Empty : member.ContactNo)
                    .WithSqlParam("gender", member.Gender)
                    .WithSqlParam("email", member.Email == null ? string.Empty : member.Email)
                    .WithSqlParam("bank_account", member.BankAccount == null ? string.Empty : member.BankAccount)
                    .WithSqlParam("sponsor_id", member.SponsorId)
                    .WithSqlParam("date_join", DateTime.Now)
                    .WithSqlParam("tin_no", member.TinNo == null ? string.Empty : member.TinNo)

                    .WithSqlParam("username", member.Username)
                    .WithSqlParam("password", member.Password)

                    .WithSqlParam("purchase_type", 1)
                    .WithSqlParam("product_id", 1)
                    .WithSqlParam("product_code", member.product_code)
                    .WithSqlParam("pin_code", member.pin_code)
                    .WithSqlParam("qty", 1)
                    .WithSqlParam("amount", 1800)
                    .WithSqlParam("discount", 0)
                    .WithSqlParam("program_id", 1)
                    .WithSqlParam("comm", 0)

                    .ExecuteStoredProc<SaveMemberViewModel_Result>()
                    .Single();

                var sponsor = _context.Member.Where(m => m.Id == member.SponsorId).FirstOrDefault();
                if (sponsor != null)
                {
                    ViewBag.Sponsor_Id = sponsor.Id;
                    ViewBag.Sponsor_Name = sponsor.Fname + " " + sponsor.Lname;
                }

                ViewBag.Result = result.message;
                ViewBag.Member_Id = result.member_id;
                if (result.message == "SUCCESS")
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return View(member);
                }

            }
            return View(member);

        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
