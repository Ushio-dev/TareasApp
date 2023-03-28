using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TareasApp.Models;
using TareasApp.Models.viewmodel;

namespace TareasApp.Controllers
{
    public class TareasController : Controller
    {
        private readonly TareaContext _tareaContext;
        public TareasController(TareaContext tareaContext) {
            _tareaContext = tareaContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _tareaContext.Tareas.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TareaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tarea = new Tarea()
                {
                    TareaName = model.TareaName,
                };

                _tareaContext.Add(tarea);
                await _tareaContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["Tareas"] = SelectList(_tareaContext.Tareas, "TareaName", viewModel.TareaName);  esto es para los selects
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var oTarea = await _tareaContext.Tareas.FirstOrDefaultAsync(busc => busc.Id == id);

            TareaViewModel2 vmTarea = new TareaViewModel2
            {
                TareaRequest = oTarea
            };

            if (oTarea == null)
            {
                return NotFound();
            }


			return View(vmTarea);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TareaViewModel2 model)
        {
            if (ModelState.IsValid)
            {
                    _tareaContext.Tareas.Update(model.TareaRequest);
                    await _tareaContext.SaveChangesAsync();
                    return RedirectToAction("Index");

            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var oTarea = await _tareaContext.Tareas.FindAsync(id);

            if (oTarea != null) 
            {
                _tareaContext.Tareas.Remove(oTarea);

                await _tareaContext.SaveChangesAsync();

				return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
