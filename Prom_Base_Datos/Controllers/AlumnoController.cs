using Prom_Base_Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prom_Base_Datos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateORUpdateAlumno()
        {
            //agregamos el modelo para enviar a la vista
            Alumno model = new Alumno();
            return View(model);
        }

        //ahora aca vamos a recibir el modelo
        [HttpPost]
        public ActionResult CreateORUpdateAlumno(Alumno model)
        {
            //agregamos el modelo para enviar a la vista
            if (ModelState.IsValid)
            {
                model.promedio = (model.nota1 + model.nota2 + model.nota3) / 3;
                model.estado = model.promedio > 6 ? "Aprobado" : "Reprobado";

                //agregmos a base de datos
                using (var context = new Contexto())
                {
                    context.Alumnos.Add(model);
                    context.SaveChanges();
                }
                return View("Correcto");
            }

            else
            {
                return View(model);
            }

        }
        public ActionResult MostrarDatos()
        {
            //para capturar datos desde la base
            using (var context = new Contexto())
            {
                var data = context.Alumnos.ToList();
                ViewBag.Datos = data;
                return View();
            }

            
        }
    }
}