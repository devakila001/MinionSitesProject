using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteMinion.Contexts;
using WebsiteMinion.Models;

namespace SiteChecker.UI.Controllers
{
    public class WebsiteInfoController : Controller
    {
        private readonly WebsiteDbContext _context;

        public WebsiteInfoController(WebsiteDbContext context)
        {
            _context = context;
        }

        // GET: WebsiteInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Websiteinfos.ToListAsync());
        }

        // GET: WebsiteInfo/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteInfo = await _context.Websiteinfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (websiteInfo == null)
            {
                return NotFound();
            }

            return View(websiteInfo);
        }

        // GET: WebsiteInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebsiteInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WebsiteUrl,RegisteredAt,MonitoringEnabled,LastCheckedAt,MonitoringIntervalSeconds,Id,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt")] WebsiteInfo websiteInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(websiteInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(websiteInfo);
        }

        // GET: WebsiteInfo/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteInfo = await _context.Websiteinfos.FindAsync(id);
            if (websiteInfo == null)
            {
                return NotFound();
            }
            return View(websiteInfo);
        }

        // POST: WebsiteInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("WebsiteUrl,RegisteredAt,MonitoringEnabled,LastCheckedAt,MonitoringIntervalSeconds,Id,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt")] WebsiteInfo websiteInfo)
        {
            if (id != websiteInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(websiteInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebsiteInfoExists(websiteInfo.Id))
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
            return View(websiteInfo);
        }

        // GET: WebsiteInfo/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteInfo = await _context.Websiteinfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (websiteInfo == null)
            {
                return NotFound();
            }

            return View(websiteInfo);
        }

        // POST: WebsiteInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var websiteInfo = await _context.Websiteinfos.FindAsync(id);
            if (websiteInfo != null)
            {
                _context.Websiteinfos.Remove(websiteInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebsiteInfoExists(long id)
        {
            return _context.Websiteinfos.Any(e => e.Id == id);
        }
    }
}
