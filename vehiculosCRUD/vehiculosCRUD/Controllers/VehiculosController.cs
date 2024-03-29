﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vehiculosCRUD.Models;

namespace vehiculosCRUD.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly db_vehiculosContext _context;

        public VehiculosController(db_vehiculosContext context)
        {
            _context = context;
        }

        // GET: Vehiculoes
        public async Task<IActionResult> Index()
        {
            PropietarioController.SubirJSONaBaseDeDatos();
            var db_vehiculosContext = _context.Vehiculos.Include(v => v.Marca).Include(v => v.Propietario);
            return View(await db_vehiculosContext.ToListAsync());
        }

        // GET: Vehiculoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.Propietario)
                .FirstOrDefaultAsync(m => m.Patente == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // GET: Vehiculoes/Create
        public IActionResult Create()
        {
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Nombre");
            ViewData["IdPropietario"] = CrearSelectListPropietarios(_context.Propietarios);
            return View();
        }

        // POST: Vehiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Patente,IdPropietario,IdMarca,Modelo,CantPuertas")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Nombre", vehiculo.IdMarca);
            ViewData["IdPropietario"] = CrearSelectListPropietarios(_context.Propietarios);
            return View(vehiculo);
        }

        public IEnumerable<SelectListItem> CrearSelectListPropietarios(IEnumerable<Propietario> list) {
            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (Propietario prop in list) {
                SelectListItem selectItem = new SelectListItem
                {
                    Text = prop.Nombre + " " + prop.Apellido,
                    Value = prop.Id.ToString(),
                    Selected = false
                };
                selectList.Add(selectItem);
            }
            

            return selectList;

        }

        public IEnumerable<SelectListItem> CrearSelectListPropietarios(IEnumerable<Propietario> list, int id)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (Propietario prop in list)
            {
                SelectListItem selectItem;
                if (prop.Id == id)
                {
                    selectItem = new SelectListItem
                    {
                        Text = prop.Nombre + " " + prop.Apellido,
                        Value = prop.Id.ToString(),
                        Selected = true

                    };

                }
                else {
                    selectItem = new SelectListItem
                    {
                        Text = prop.Nombre + " " + prop.Apellido,
                        Value = prop.Id.ToString(),
                        Selected = false

                    };
                }
                selectList.Add(selectItem);

            }


            return selectList;

        }

        // GET: Vehiculos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Nombre", vehiculo.IdMarca);
            ViewData["IdPropietario"] = CrearSelectListPropietarios(_context.Propietarios, vehiculo.IdPropietario);
            return View(vehiculo);
        }

        // POST: Vehiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Patente,IdPropietario,IdMarca,Modelo,CantPuertas")] Vehiculo vehiculo)
        {
            if (id != vehiculo.Patente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.Patente))
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
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Id", vehiculo.IdMarca);
            ViewData["IdPropietario"] = new SelectList(_context.Propietarios, "Id", "Id", vehiculo.IdPropietario);
            return View(vehiculo);
        }

        // GET: Vehiculoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.Propietario)
                .FirstOrDefaultAsync(m => m.Patente == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(string id)
        {
            return _context.Vehiculos.Any(e => e.Patente == id);
        }
    }
}
