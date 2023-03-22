namespace PlatePal.Services
{
    public class RecipesService
    {
        private readonly RecipesRepository _repo;

        public RecipesService(RecipesRepository repo)
        {
            _repo = repo;
        }

        internal List<Recipe> GetAll()
        {
            List<Recipe> recipes = _repo.GetAll();
            return recipes;
        }

        internal Recipe GetById(int id)
        {
            Recipe recipe = _repo.GetById(id);
            if (recipe == null) throw new Exception("no recipe id found");

            return recipe;
        }
        internal Recipe CreateRecipe(Recipe recipeData)
        {
            Recipe recipe = _repo.CreateRecipe(recipeData);
            return recipe;
        }
    }
}