namespace PlatePal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService _ingredientsService;
        private readonly Auth0Provider _auth;

        public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth)
        {
            _ingredientsService = ingredientsService;
            _auth = auth;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Ingredient>> CreateInstruction([FromBody] Ingredient ingredientsData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                Ingredient ingredients = _ingredientsService.CreateIngredient(ingredientsData, userInfo.Id);
                return Ok(ingredients);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        async public Task<ActionResult<string>> DeleteIngredient(int id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                string message = _ingredientsService.DeleteIngredient(id, userInfo.Id);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}