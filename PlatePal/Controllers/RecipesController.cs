namespace PlatePal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _recipesService;

        public RecipesController(RecipesService recipesService)
        {
            _recipesService = recipesService;
        }
        [HttpGet]
        public ActionResult<List<Recipe>> GetAll()
        {
            try
            {
                List<Recipe> recipes = _recipesService.GetAll();
                return Ok(recipes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}