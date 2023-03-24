namespace PlatePal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _recipesService;
        private readonly IngredientsService _ingredientsService;
        private readonly Auth0Provider _auth;

        public RecipesController(RecipesService recipesService, Auth0Provider auth, IngredientsService ingredientsService)
        {
            _recipesService = recipesService;
            _auth = auth;
            _ingredientsService = ingredientsService;
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
        [HttpGet("{id}")]
        public ActionResult<Recipe> GetById(int id)
        {
            try
            {
                Recipe recipe = _recipesService.GetById(id);
                return Ok(recipe);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/ingredients")]
        public ActionResult<List<Ingredient>> GetIngredientsByRecipe(int id)
        {
            try
            {
                List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipe(id);
                return Ok(ingredients);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        [Authorize]
        async public Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                recipeData.CreatorId = userInfo.Id;
                Recipe recipe = _recipesService.CreateRecipe(recipeData);
                recipe.Creator = userInfo;
                return Ok(recipe);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        async public Task<ActionResult<Recipe>> EditRecipe(int id, [FromBody] Recipe recipeData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                Recipe recipe = _recipesService.EditRecipe(id, recipeData, userInfo.Id);
                return Ok(recipe);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        async public Task<ActionResult<string>> DeleteRecipe(int id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                string message = _recipesService.DeleteRecipe(id, userInfo.Id);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}