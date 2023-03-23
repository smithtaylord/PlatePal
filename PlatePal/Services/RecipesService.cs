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

        internal string DeleteRecipe(int id, string userId)
        {
            Recipe recipe = _repo.GetById(id);
            if (recipe == null) throw new Exception($"No recipe at id:{id}");
            if (recipe.CreatorId != userId) throw new Exception("You are not allowed to delete this recipe");
            _repo.DeleteRecipe(id);
            return $"{recipe.Title} recipe has been removed";

        }

        internal Recipe EditRecipe(int recipeId, Recipe recipeData, string userId)
        {
            Recipe original = _repo.GetById(recipeId);
            if (original.CreatorId != userId) throw new Exception("You did not create this recipe, so you cannot delete it");
            original.Instructions = recipeData.Instructions != null ? recipeData.Instructions : original.Instructions;
            int rowsAffected = _repo.EditRecipe(original);
            if (rowsAffected == 0) throw new Exception("Could not modify for some reason");
            if (rowsAffected > 1) throw new Exception("Something went very very wrong and you edited more than one row.");
            return original;
        }
    }
}