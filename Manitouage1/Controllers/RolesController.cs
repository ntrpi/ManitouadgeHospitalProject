// Created by Sandra Kupfer 2021/04

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Manitouage1.Models;

namespace Manitouage1.Controllers
{
    // https://www.yogihosting.com/aspnet-core-identity-roles/#all-roles
    // 2021/04/05
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> roleManager;

        public RolesController( )
        {
            roleManager = new RoleManager<IdentityRole>( 
                    new RoleStore<IdentityRole>( new ManitouageDbContext() )
                );
            string[] roleNames = { "Admin", "Staff", "Patient", "User" };
            foreach( string name in roleNames ) {
                if( !roleManager.RoleExists( name ) ) {
                    roleManager.Create( new IdentityRole( name ) );
                }
            }
        }

        public ViewResult Index() => View( roleManager.Roles );

        private void Errors( IdentityResult result )
        {
            foreach( var error in result.Errors )
                ModelState.AddModelError( "", error );
        }

        public ActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create( [Required] string name )
        {
            if( ModelState.IsValid ) {
                IdentityResult result = await roleManager.CreateAsync( new IdentityRole( name ) );
                if( result.Succeeded )
                    return RedirectToAction( "Index" );
                else
                    Errors( result );
            }
            return View( name );
        }

        // This is a cheat for now.
        // TODO: this should be post.
        [HttpGet]
        public async Task<ActionResult> Delete( string id )
        {
            IdentityRole role = await roleManager.FindByIdAsync( id );
            if( role != null ) {
                IdentityResult result = await roleManager.DeleteAsync( role );
                if( result.Succeeded )
                    return RedirectToAction( "Index" );
                else
                    Errors( result );
            } else
                ModelState.AddModelError( "", "No role found" );
            return View( "Index", roleManager.Roles );
        }
    }
}