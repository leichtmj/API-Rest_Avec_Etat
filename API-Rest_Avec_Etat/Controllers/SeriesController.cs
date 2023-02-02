using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Rest_Avec_Etat.Models.EntityFramework;

namespace API_Rest_Avec_Etat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly SeriesDbContext _context;

        public SeriesController(SeriesDbContext context)
        {
            _context = context;
        }

        // GET: api/Series
        /// <summary>
        /// Récupère toutes les series
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Serie>>> GetSeries()
        {
            return await _context.Series.ToListAsync();
        }

        // GET: api/Series/5
        /// <summary>
        /// Récupère une serie via son id
        /// </summary>
        /// <param name="id">L'id de la serie</param>
        /// <response code ="200">quand la serie est trouvée</response>
        /// <response code ="404">quand la serie n'est pas trouvée</response>
        /// <returns>Http response</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Serie>> GetSerie(int id)
        {
            var serie = await _context.Series.FindAsync(id);

            if (serie == null)
            {
                return NotFound();
            }

            return serie;
        }

        // PUT: api/Series/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Permet la modification d'une serie
        /// </summary>
        /// <param name="id">L'id de la serie que l'on souhaite modifier</param>
        /// <param name="devise">La nouvelle serie modifiée</param>
        /// <response code ="404">quand la serie n'est pas trouvée</response>
        /// <response code ="400">quand la serie est invalide</response>
        /// <response code ="204">quand la modification a été appliquée</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PutSerie(int id, Serie serie)
        {
            if (id != serie.Serieid)
            {
                return BadRequest();
            }

            _context.Entry(serie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Series
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Ajoute une serie
        /// </summary>
        /// <param name="serie"></param>
        /// <response code ="201">quand la serie est ok</response>
        /// <returns>Retourne la serie créée</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Serie>> PostSerie(Serie serie)
        {
            _context.Series.Add(serie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSerie", new { id = serie.Serieid }, serie);
        }

        // DELETE: api/Series/5
        /// <summary>
        /// Supprime une serie à partir de son ID
        /// </summary>
        /// <param name="id">L'id de la serie à supprimer</param>
        /// <response code ="400">quand la serie est invalide</response>
        /// <returns>La serie supprimée</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSerie(int id)
        {
            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }

            _context.Series.Remove(serie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SerieExists(int id)
        {
            return _context.Series.Any(e => e.Serieid == id);
        }
    }
}
