using System;
using CMS_miniAPI.Hubs;
using CMS_miniAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;


namespace CMS_miniAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CMSController : ControllerBase
	{
		private readonly CMSContext _context;
		private readonly IHubContext<SignalHub> _hubContext;

		public CMSController(CMSContext context, IHubContext<SignalHub> hubContext)
		{
			_context = context;
			_hubContext = hubContext;
		}

		/**
		 * BaseBeacon 标签表
		 */

		// GET: 获取单个标签
		[HttpGet("{id}")]
		public async Task<ActionResult<BaseBeacon>> GetBeaconbyId(long id)
		{
			if (_context.Beacons == null) return NotFound();
			var beacon = await _context.Beacons.FindAsync(id);
			if (beacon == null)
				return NotFound();
			return beacon;
		}

		// GET: 获取全部标签
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BaseBeacon>>> GetBeaconList()
		{
			if (_context.Beacons == null) return NotFound();
			var beacons = await _context.Beacons.ToListAsync();
			if (beacons == null)
				return NotFound();
			return beacons;
		}

		// PUT: 更新单个标签
		[HttpPut("{id}")]
		public async Task<IActionResult> PutBeaconbyId(long id, BaseBeacon beacon)
		{
			if (id != beacon.Id) return BadRequest();
			_context.Entry(beacon).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
				await _hubContext.Clients.All.SendAsync("updatecar", "Update");
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BeaconExists(id))
					return NotFound();
				else
					throw;
			}
			return NoContent();
		}

		// POST: 新增单个标签
		[HttpPost]
		public async Task<ActionResult<BaseBeacon>> PostBeacon(BaseBeacon beacon)
		{
			if (_context.Beacons == null)
				return Problem("标签表BaseBeacon为空");
			_context.Beacons.Add(beacon);
			await _context.SaveChangesAsync();
			await _hubContext.Clients.All.SendAsync("updatecar", "Add");
			return CreatedAtAction(nameof(GetBeaconbyId), new { id = beacon.Id }, beacon);
		}

		private bool BeaconExists(long id) {
			return (_context.Beacons?.Any(e => e.Id == id)).GetValueOrDefault();
		}

		// DELETE: 删除单个标签
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBeaconbyId(long id)
		{
			if (_context.Beacons == null) return NotFound();
			var beacon = await _context.Beacons.FindAsync(id);
			if (beacon == null) return NotFound();

			_context.Beacons.Remove(beacon);
			await _context.SaveChangesAsync();
			await _hubContext.Clients.All.SendAsync("updatecar", "Delete");

			return NoContent();
		}

		/**
		 * Orgnization 组织表
		 */

		// 获取组织列表
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BaseOrganization>>> GetOrgnizationList()
		{
			if (_context.Organizations == null) return NotFound();
			var organizations = await _context.Organizations.Where(t => t.ParentId == null).ToListAsync();
			if (organizations == null)
				return NotFound();
			return organizations;
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<BaseOrganization>> GetOrgnizationbyId(long id)
		{
			if (_context.Organizations == null) return NotFound();
			var orgnization = await _context.Organizations.FindAsync(id);
			if (orgnization == null) return NotFound();
			return orgnization;
		}
		// 增加组织表，同时发送给客户端更新信号
		[HttpPost]
		public async Task<ActionResult<BaseOrganization>> AddOrgnization(BaseOrganization org)
		{
            if (_context.Organizations == null)
                return Problem("组织表Orgnizations为空！");
            _context.Organizations.Add(org);
            await _context.SaveChangesAsync();
			// 发送更新消息
			await _hubContext.Clients.All.SendAsync("updatefactory", "true");
            return CreatedAtAction(nameof(GetOrgnizationbyId), new { id = org.Id }, org);
        }
        // 更新组织信息，同时发送客户端更新信号
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrgnizationbyId(long id, BaseOrganization org)
        {
            if (id != org.Id) return BadRequest();
            _context.Entry(org).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
				await _hubContext.Clients.All.SendAsync("updatefactory");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeaconExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }
    }
}

