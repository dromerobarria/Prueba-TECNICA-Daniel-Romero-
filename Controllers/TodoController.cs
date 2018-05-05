using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

#region TodoController
namespace TodoApi.Controllers
{

    public class Solucion
    {
        /* Función que determina si el multiplo de 101 con N operadores (Es recursiva)
            numeros: Arreglo de numeros a evaluar
            value: valor resultante
            index: indice del arreglo
        */
        public static string expression(int[] numeros, int value, int index)
        {


            /* Verficar el modulo de 101*/
            value %= 101;
            if (index == numeros.Length)
                return value == 0 ? "" : null;

            int index_proximo = index + 1;
            int numero = numeros[index];
            string resultado;


            resultado = expression(numeros, value * numero, index_proximo);

            /* Verficar que el resulado no sea nulo y aplico operador*/

            if (resultado != null)
                return "+" + numeros[index] + resultado;

            resultado = expression(numeros, value - numero, index_proximo);

            if (resultado != null)
                return "*" + numeros[index] + resultado;

            resultado = expression(numeros, value + numero, index_proximo);

            if (resultado != null)
                return "-" + numeros[index] + resultado;

            return null;
        }

        public string StartNumbers()
        {
            int[] numeros = new int[] { 5, 55, 3, 45, 33, 25 };
            return (numeros[0] + expression(numeros, numeros[0], 1));
        }
    }


    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        #endregion

        public TodoController(TodoContext context)
        {
            _context = context;

            Solucion myClass = new Solucion();

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { resultado = myClass.StartNumbers()});
                _context.SaveChanges();
            }
        }



        #region snippet_GetAll
        [HttpGet]
        public List<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }
        #endregion

    }
}