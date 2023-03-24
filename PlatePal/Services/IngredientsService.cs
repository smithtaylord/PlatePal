namespace PlatePal.Services
{
    public class IngredientsService
    {
        private readonly IngredientsRepository _repo;
        private readonly RecipesService _recipesService;
        public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
        {
            _repo = repo;
            _recipesService = recipesService;
        }

        internal Ingredient CreateIngredient(Ingredient ingredientData, string userId)
        {
            Recipe recipe = _recipesService.GetById(ingredientData.RecipeId);
            if (recipe == null) throw new Exception($"No recipe with id: {ingredientData.Id}");
            if (recipe.CreatorId != userId) throw new Exception("This recipe does not belong to you, so you cannot att ingredients to it");
            Ingredient ingredient = _repo.CreateIngredient(ingredientData);
            return ingredient;

        }


        internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
        {
            List<Ingredient> ingredients = _repo.GetIngredientsByRecipe(recipeId);
            return ingredients;
        }
        internal string DeleteIngredient(int id, string userId)
        {
            Ingredient ingredient = _repo.GetIngredientById(id);
            Recipe recipe = _recipesService.GetById(ingredient.RecipeId);
            if (ingredient == null) throw new Exception($"No ingredient with id: {id}");
            if (recipe.CreatorId != userId) throw new Exception("This recipe does not belong to you");
            _repo.DeleteIngredient(id);
            return $"{ingredient.Name} has been removed";
        }
    }
}